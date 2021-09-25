using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    #region Variables
    public GameObject instigator; //stores the object that fires this projectile
    private Rigidbody2D prb; //stores the projectile rigidbody 
    private float projectileDamage; //the damage value of the projectile



    #endregion
    private void Awake()
    {
        prb = GetComponent<Rigidbody2D>();
    }
    #region CustomMethods
    private void OnTriggerEnter(Collider _other)
    {
        Debug.Log("Collision Detected");
        GameObject enemyObject = _other.gameObject;
        Health enemyHealth = enemyObject.GetComponent<Health>();
        if (enemyHealth != null) //if the enemy has health, make it take damage
        {
            enemyHealth.Damage(projectileDamage);
            Debug.Log("Enemy Damaged");
        }
        prb.velocity = new Vector3(0f, 0f, 0f); //stops the projectile on impact
        Destroy(gameObject); //destroys the object after impact.
        Debug.Log("Projectile destroyed");
    }


 

    #region GettersAndSetters
    public float GetProjectileDamage() //the getter for the damage
    {
        return projectileDamage;
    }
    public float SetProjectileDamage(float dmg) //the setter of the damage
    {
        projectileDamage = dmg;
        return projectileDamage;
    }

    #endregion

    #endregion
}
