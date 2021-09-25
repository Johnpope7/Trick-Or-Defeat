using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleePawn : Pawn
{
    [Header("Melee Setting & Attributes")]
    [SerializeField, Tooltip("the position of the attack game object shell.")]
    protected Transform attackPos;
    [SerializeField, Range(0,100), Tooltip("the radius of the attack circle")]
    protected float attackRadius;

    // Start is called before the first frame update
    protected override void Start()
    {
        //call the parent class's start method
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        //call the parent class's update method
        base.Update();
    }

    /// <summary>
    /// An action that pawns can perform. This can be anything from a melee attack, to a heal.
    /// </summary>
    public override void Action() 
    {
        //if our action's cool down is less than or equal to zero
        if (coolDown <= 0)
        {
            //create a circle and return all the colliders within the area into an array
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRadius, enemyLayer);
            //for every collider in that array
            for (int i = 0; i < enemiesToDamage.Length; i++) 
            {
                enemiesToDamage[i].GetComponent<Health>().Damage(damage);
                Debug.Log("Hit Enemy: " + enemiesToDamage[i].name);
            }
            coolDown = coolDownTime;
        }
        Debug.Log("Action Complete!");
    }

    /// <summary>
    /// Gizmo for visually displaying the attack range
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRadius);
    }
}
