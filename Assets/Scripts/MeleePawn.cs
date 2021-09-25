using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleePawn : Pawn
{
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
            coolDown = coolDownTime;
        }
        else 
        {
            coolDown -= Time.deltaTime;
        }
    }
}
