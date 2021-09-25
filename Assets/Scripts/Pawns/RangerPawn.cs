using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerPawn : Pawn
{
    #region Variables
    [Header("Action Variables")]
    public GameObject projectilePrefab; // stores game object for the projectile instantiation
    private Rigidbody2D prb; //stores the projectile rigid body
    [SerializeField]
    private Transform firingZone; //the spot from which the projectile comes from
    public float projectileLifeSpan;//the lifespan of a projectile, how long itll last on the screen
    [SerializeField]
    private float shotForce = 20000f; //speed of the projectile shot

    #endregion

    #region Builtin Methods
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        coolDown = Mathf.Clamp(coolDown - Time.deltaTime, 0f, coolDownTime); //Cooldown for the action
    }
    #endregion

    #region Custom Methods
    public override void Action()
    {
        //create the vector 3 variable that is equal to our firing zones forward vector multiplied by shot force
        Vector2 shotDir = firingZone.up * shotForce;
        Debug.Log("shotDir is," + shotDir);
        //spawn the bullet
        GameObject projectileInstance = Instantiate(projectilePrefab, firingZone.position, firingZone.rotation);
        Debug.Log("projectile Instantiated");
        Projectile projectile = projectileInstance.GetComponent<Projectile>();
        //get the instigator
        projectile.instigator = this.gameObject;
        Debug.Log("gameObject assigned");
        //get the bulletDamage variable
        projectile.SetProjectileDamage(damage);
        Debug.Log("projectileDamage set," + projectile.GetProjectileDamage());
        //get the shell rigid body to apply force
        prb = projectile.GetComponent<Rigidbody2D>();
        Debug.Log("Rigidbody set");
        //apply the shotforce variable to the rigid body to make the bullet move
        prb.AddForce(shotDir);
        Debug.Log("Added force to the power of " + shotDir);
        //destroy the bullet after a desired time
        Destroy(projectileInstance, projectileLifeSpan);
        Debug.Log("Projectile destroyed");
    }
    #endregion

    #region Getters and Setters
    public float p_shotForce  //gets the speed of the bullet
    {
        get { return shotForce; }
    }
    #endregion
}
