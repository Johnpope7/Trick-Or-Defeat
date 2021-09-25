using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pawn : MonoBehaviour
{
    [Header("General Pawn Attributes")]
    [SerializeField, Range(0, 1000)]
    protected float speed;
    [SerializeField]
    protected Rigidbody2D rb;

    [Header("Pawn Attack Settings")]
    [SerializeField, Range(0, 1)]
    protected float coolDownTime; //the amount of time it takes an action to refresh
    [SerializeField]
    protected float coolDown; //holds our cool down countdown, we subtract from this to tell when our action is refreshed
    [SerializeField]
    protected LayerMask enemyLayer; //the Layer Mask taht the enemies reside
    [SerializeField, Range(0, 100)]
    protected float damage;

    [Header("Pawn Events")]
    public UnityEvent OnAction;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
    /// <summary>
    /// Moves the pawn in the given direction using their rigidbody
    /// </summary>
    /// <param name="movement"></param>
    public void Move(Vector2 movement)
    {
        //move the rigidbody by Vector 2 multiplied by speed.
        rb.velocity = new Vector2(movement.x * speed * Time.fixedDeltaTime, movement.y * speed * Time.fixedDeltaTime);
    }

    public virtual void Action() 
    {
        //override in children
    }
}
