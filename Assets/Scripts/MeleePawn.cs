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
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        //lower cooldown as time passes, but not below 0 or above its cooldown time
        coolDown = Mathf.Clamp(coolDown - Time.deltaTime, 0f, coolDownTime);
    }

    public override void Action() 
    {
        if (coolDown <= 0)
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRadius, enemyLayer);
            for (int i = 0; i < enemiesToDamage.Length; i++) 
            {
                enemiesToDamage[i].GetComponent<Health>().Damage(damage);
                Debug.Log("Hit Enemy:{0%}", enemiesToDamage[i]);
            }
            coolDown = coolDownTime;
        }
        Debug.Log("Action Complete!");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRadius);
    }
}
