using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pawn : MonoBehaviour
{
    [Header("General Pawn Attributes")]
    [SerializeField, Range(0, 1000)]
    private float speed;
    [SerializeField]
    private Rigidbody2D rb;

    [Header("Pawn Attack Settings")]
    [SerializeField, Range(0, 10)]
    private float coolDownTime;
    private float coolDown;

    [Header("Pawn Events")]
    public UnityEvent Action;
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
}
