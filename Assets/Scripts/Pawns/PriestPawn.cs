using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriestPawn : Pawn
{
    protected float distance = 10f;

    // Start is called before the first frame update
    protected override void Start()
    {
        // Calls the parent's class start method
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        // Calls the parent's class update method
        base.Update();
    }

    public override void Action()
    {
        if (coolDown <= 0)
        {         
            // Needs to be updated once the directional joystick has been implemented into the code

        //    transform.position = new Vector2(transform.position.x - distance, transform.position.y);
        //    transform.position = new Vector2(transform.position.x, transform.position.y - distance);
        }
    }
}
