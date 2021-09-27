using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartTruer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LevelManager.instance.isGameStart = true;
    }
}
