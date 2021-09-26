using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        nextSpawnTime = Time.time + LevelManager.instance.enemySpawnDelay;
        LevelManager.instance.enemySpawners.Add(gameObject);
        base.Start();
    }

    protected override void Update()
    {
        //if the number of current enemies is less than the number of max enemies
        if (LevelManager.instance.currentEnemies < LevelManager.instance.maxEnemies) 
        {
            //and if the time is greater than or equal to the next set spawn time
            if (Time.time >= nextSpawnTime)
            {
                //declare a random int between 0 and the max number of enemy prefabs in their list
                int random = Random.Range(0, LevelManager.instance.enemyPrefabs.Count);
                //instantiate an enemy using our random int at this objects position
                GameObject enemy = Instantiate(LevelManager.instance.enemyPrefabs[random], tf.position, tf.rotation);
                //name it something meaningful, in this case, the name of the prefab it chose
                //and the number it is in the current enemies count
                enemy.name = LevelManager.instance.enemyPrefabs[random] + "_" + LevelManager.instance.currentEnemies;
                //add it to the GameManager's list of enemies
                LevelManager.instance.enemies.Add(enemy);
                //set the next spawn time equal to now plus enemy spawn delay
                nextSpawnTime = Time.time + LevelManager.instance.enemySpawnDelay;
                //increment the number of current enemies
                LevelManager.instance.currentEnemies++;
            }
        }

        base.Update();
    }

    protected override void OnDestroy()
    {
        LevelManager.instance.enemySpawners.Remove(gameObject);
        base.OnDestroy();
    }
}
