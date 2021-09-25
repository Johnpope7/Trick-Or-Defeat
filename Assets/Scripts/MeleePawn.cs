using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleePawn : Pawn
{
    [Header("Melee Setting & Attributes")]
    [SerializeField]
    protected Transform attackPos;
    [SerializeField, Range(0,100)]
    protected float attackRadius;
    // Start is called before the first frame update
    protected override void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    public override void Action() 
    {
        if (coolDown <= 0)
        {
            Collider2D[] enemeiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRadius, enemyLayer);

            coolDown = coolDownTime;
        }
        else 
        {
            coolDown -= Time.deltaTime;
        }
    }
}
