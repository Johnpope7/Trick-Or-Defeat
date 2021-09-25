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
    protected float coolDownTime;
    [SerializeField]
    protected float coolDown;
    [SerializeField]
    protected LayerMask enemyLayer;
    [SerializeField, Range(0, 100)]
    protected float damage;

    [Header("Pawn Events")]
    public UnityEvent OnAction;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        
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
