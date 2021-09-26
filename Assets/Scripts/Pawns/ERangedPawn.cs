using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ERangedPawn : Pawn
{
    
    [Header("Action Variables")]
    public GameObject target; //the target of the enemy pawn
    public GameObject projectilePrefab; // stores game object for the projectile instantiation
    private Rigidbody2D prb; //stores the projectile rigid body
    [SerializeField]
    private Transform firingZone; //the spot from which the projectile comes from
    public float projectileLifeSpan;//the lifespan of a projectile, how long itll last on the screen
    [SerializeField]
    private float shotForce = 200f; //speed of the projectile shot

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void Action()
    {
        Vector2 shotDir = transform.position - target.transform.position;
        GameObject projectileInstance = Instantiate(projectilePrefab, firingZone.position, firingZone.rotation);
        Projectile projectile = projectileInstance.GetComponent<Projectile>();
        //get the instigator
        projectile.instigator = this.gameObject;
        //get the bulletDamage variable
        projectile.SetProjectileDamage(damage);
        //get the shell rigid body to apply force
        prb = projectile.GetComponent<Rigidbody2D>();
        //apply the shotforce variable to the rigid body to make the bullet move
        prb.AddForce(shotDir * shotForce);
        //destroy the bullet after a desired time
        Destroy(projectileInstance, projectileLifeSpan);
    }
}
