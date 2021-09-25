/*
 * This is the super class for the controllers, they don't need much more than this.
 * the getters and setters are because the pawns are technically private
 * use them if you need to get to a controllers pawn outside of its class.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    //the pawn this controller is controlling
    protected Pawn pawn;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        pawn = GetComponent<Pawn>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
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
