using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : Controller
{
    private Pawn pawn;
    private Vector2 movement;
    // Start is called before the first frame update
    protected override void Start()
    {
        pawn = GetComponent<Pawn>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        //set movement x and y values to appropriate axis
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        pawn.Move(movement);
    }
}