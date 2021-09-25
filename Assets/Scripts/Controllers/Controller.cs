using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    protected Pawn pawn;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
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
