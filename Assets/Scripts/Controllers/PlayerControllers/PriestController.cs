using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriestController : PlayerController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        //movement
        //set movement x and y values to appropriate axis
        movement.x = Input.GetAxis("Move1Horizontal");
        movement.y = Input.GetAxis("Move1Vertical");
        //pass it to the pawn's Move function
        pawn.Move(movement);
        //aiming
        //set aim x and y values to appropriate axis
        aim.x = Input.GetAxis("Aim1Horizontal");
        aim.y = Input.GetAxis("Aim1Vertical");
        //pass these values to the pawns aim function
        pawn.Aim(aim);
        /*
         * CONTROLLER INPUT
         */

        //Action Input
        //we're using mouse 0 for now because the Action input we made in
        //the input manager wasn't recognized by name for whatever reason
        if (Input.GetAxis("Action1") > 0)
        {

            Debug.Log("Using Action!");
            //this is the action event on the pawn component
            //you can set up what you want to happen in the inspector
            //and this will just do it!
            pawn.OnAction.Invoke();

        }
    }
}
