using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public int score;
    public int maxEnemies;
    public float levelTime;
    public GameObject target;

    void Awake() 
    {
        target = GameObject.FindGameObjectWithTag("Priest");
    }
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) // if instance is empty
        {
            instance = this; // store THIS instance of the class in the instance variable
        }
        else
        {
            Destroy(this.gameObject); // delete the new level manager attempting to store itself, there can only be one.
            Debug.Log("Warning: A second game manager was detected and destrtoyed"); // display message in the console to inform of its demise
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
