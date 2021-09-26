using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointAdder : MonoBehaviour
{
    private void Awake()
    {
        LevelManager.instance.spawnerWaypoints.Add(this.gameObject);
    }
}
