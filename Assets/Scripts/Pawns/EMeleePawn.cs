using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMeleePawn : Pawn
{
    [HideInInspector]
    public GameObject target; //the target of the enemy pawn
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void Action()
    {
        if (coolDown <= 0)
        {
            //play attack animation
            //wait until it is finished and deal damage
            target.GetComponent<Health>().Damage(damage); 
            //the above may need to be wrapped in a coroutine once animations are implemented
        }
    }
}
