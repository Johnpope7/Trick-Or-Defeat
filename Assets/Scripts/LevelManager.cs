using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Level Data")]
    public static LevelManager instance;
    public float levelTime; //level play time
    public int score; //player team score

    [Header("AI Settings")]
    public GameObject target; //target of the enemies (the priest)
    public int maxEnemies; //the max number of enemies allowed in the level
    public int currentEnemies; //the current number of enemies in the levl
    public float enemySpawnDelay; //the delay between enemy spawns
    public GameObject enemySpawnPrefab; //the enemy spawner object
    public List<GameObject> enemySpawners; //a list of enemy spawners
    public GameObject[] spawnerWaypoints; //a list of spawner spawn locations
    public List<GameObject> enemyPrefabs; //a list of enemy prefabs
    public List<GameObject> enemies; //a list of enemies in the game
    [SerializeField]
    private GameObject AIController; //the ai currently in the game
    [SerializeField]
    private GameObject AIControllerPefab; //the ai prefab

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
            Debug.Log("LevelManager instance made, bow before it.");
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(this.gameObject); // delete the new level manager attempting to store itself, there can only be one.
            Debug.Log("Warning: A second game manager was detected and destrtoyed"); // display message in the console to inform of its demise
        }

        spawnerWaypoints = FindSpawnLocations();
        SpawnSpawners(spawnerWaypoints);
    }

    // Update is called once per frame
    void Update()
    {
        if (AIController == null)
        {
            AIController = Instantiate(AIControllerPefab, new Vector3(0,0,0), Quaternion.identity);
            AIController.name = "AI";
        }
    }

    private GameObject[] FindSpawnLocations() 
    {
        GameObject[] spawnLocations = GameObject.FindGameObjectsWithTag("SpawnPos");

        return spawnLocations;
    }

    private void SpawnSpawners(GameObject[] locations) 
    {
        for (int i = 0; i < locations.Length; i++)
        {
            GameObject spawner = Instantiate(enemySpawnPrefab, spawnerWaypoints[i].transform.position, Quaternion.identity);
            spawner.name = "ESpawner" + i;
        }
    }
}
