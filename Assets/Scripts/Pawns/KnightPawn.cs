using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightPawn : Pawn
{
    // Setting a header for this code
    [Header("Knight Setting & Attributes")]
    [SerializeField, Tooltip("the position of the attack game object shell.")]
    protected Transform attackPos;
    [SerializeField, Range(0, 100), Tooltip("the radius of the attack circle")]
    protected float attackRadius;

    // Start is called before the first frame update
    protected override void Start()
    {
        // Call the parent's class start method
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        coolDown = Mathf.Clamp(coolDown - Time.deltaTime, 0f, coolDownTime);
    }

    // Creating a method that detects the collision and should deflect
    public override void Action()
    {
        //if our action's cool down is less than or equal to zero
        if (coolDown <= 0)
        {
            //create a circle and return all the colliders within the area into an array
            Collider2D[] enemiesToPush = Physics2D.OverlapCircleAll(attackPos.position, attackRadius, enemyLayer);
            //for every collider in that array
            for (int i = 0; i < enemiesToPush.Length; i++)
            {
                Vector2 difference = enemiesToPush[i].transform.position - transform.position;
                //draw a circle and put all the colliders within it in an array
                enemiesToPush[i].GetComponent<Rigidbody2D>().velocity = new Vector2(enemiesToPush[i].transform.position.x + difference.x,
                                                                                    enemiesToPush[i].transform.position.y + difference.y);
                Debug.Log("Pushed Enemy: " + enemiesToPush[i].name);
            }
            coolDown = coolDownTime;
        }
        Debug.Log("Action Complete!");
    }

}
