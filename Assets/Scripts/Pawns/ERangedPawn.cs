using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ERangedPawn : Pawn
{
    [Header("Action Variables")]
    [SerializeField]
    protected GameObject projectilePrefab; // stores game object for the projectile instantiation
    protected Rigidbody2D prb; //stores the projectile rigid body
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
        if (coolDown <= 0)
        {
            //get the direction the target is in and multiply it by shotForce
            Vector2 shotDir = Vector2.Lerp(LevelManager.instance.target.transform.position, transform.position, shotForce);
            //spawn an instance of the projectile at the firing zone
            GameObject projectileInstance = Instantiate(projectilePrefab, firingZone.position, firingZone.rotation);
            Projectile projectile = projectileInstance.GetComponent<Projectile>();
            //get the instigator
            projectile.instigator = this.gameObject;
            //get the bulletDamage variable
            projectile.SetProjectileDamage(damage);
            //get the shell rigid body to apply force
            prb = projectile.GetComponent<Rigidbody2D>();
            //apply the shotforce variable to the rigid body to make the bullet move
            prb.AddForce(shotDir);
            //destroy the bullet after a desired time
            Destroy(projectileInstance, projectileLifeSpan);
            coolDown = coolDownTime;
        }
    }
}
