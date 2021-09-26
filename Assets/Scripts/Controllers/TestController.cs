/*
 * This is the test controller we have for our test prefabs so far.
 * Just attach this to the pawn you want to control and plug it in through
 * the inspector to move!
 * 
 * The pawn takes advantage of UnityEvents to attack
 * the plus side is this also simplifies the control scheme
 * by decupling the controller inputs from the pawn 
 * 
 * the controller doesnt care what is in OnAction,
 * it just invokes it and moves on
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : Controller
{
    //the pawn this controller is controlling
    protected Pawn pawn;
    //the movement direction of the player
    private Vector2 movement;
    //the animator that is on the controllers pawn
    protected Animator anim;

    // Start is called before the first frame update
    protected override void Start()
    {
        pawn = GetComponent<Pawn>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        
        //movement
        //set movement x and y values to appropriate axis
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        //pass it to the pawn's Move function
        pawn.Move(movement);

        /*
         * CONTROLLER INPUT
         */

        //Action Input
        //we're using mouse 0 for now because the Action input we made in
        //the input manager wasn't recognized by name for whatever reason
        if (Input.GetKeyDown("mouse 0")) 
        {
            Debug.Log("Using Action!");
            //this is the action event on the pawn component
            //you can set up what you want to happen in the inspector
            //and this will just do it!
            pawn.OnAction.Invoke();
        }
    }

    /// <summary>
    /// Gets the pawn that this controller is controlling.
    /// </summary>
    /// <returns>returns a Pawn component</returns>
    public Pawn GetPawn()
    {
        Pawn pawn = this.gameObject.GetComponent<Pawn>();
        return pawn;
    }

    /// <summary>
    /// Sets the pawn of this controller to the one passed in.
    /// </summary>
    /// <param name="newPawn"></param>
    public void SetPawn(Pawn newPawn)
    {
        pawn = newPawn;
    }
}
