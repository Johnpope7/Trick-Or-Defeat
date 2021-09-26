using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
    //the pawn this controller is controlling
    [SerializeField]
    protected Pawn pawn;
    //this is the name of the pawn the controller is using
    [SerializeField]
    protected string pawnName;
    //this is the prefab for the pawn the controller is using
    [SerializeField]
    protected Pawn pawnPrefab;
    //the movement direction of the player
    protected Vector2 movement;
    //the animator that is on the controllers pawn
    protected Animator anim;
    //the direction the player is aiming
    protected Vector2 aim;

    // Start is called before the first frame update
    new protected virtual void Start()
    {
        if (pawn == null)
        {
            SpawnPawn();
        }
    }

        // Update is called once per frame
        new protected virtual void Update()
    {

        //movement
        //set movement x and y values to appropriate axis
        movement.x = Input.GetAxis("MoveHorizontal");
        movement.y = Input.GetAxis("MoveVertical");
        //pass it to the pawn's Move function
        pawn.Move(movement);
        //aiming
        //set aim x and y values to appropriate axis
        aim.x = Input.GetAxis("AimHorizontal");
        aim.y = Input.GetAxis("AimVertical");
        //pass these values to the pawns aim function
        pawn.Aim(aim);
        /*
         * CONTROLLER INPUT
         */

        //Action Input
        //we're using mouse 0 for now because the Action input we made in
        //the input manager wasn't recognized by name for whatever reason
        if (Input.GetAxis("Action") > 0)
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

    protected virtual void SpawnPawn() 
    {
            pawn = Instantiate(pawnPrefab, new Vector3(0, 0, 0), Quaternion.identity) as Pawn;
            pawn.name = pawnName;
    }
}
