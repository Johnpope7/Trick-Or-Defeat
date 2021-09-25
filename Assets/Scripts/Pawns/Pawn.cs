/*
 * This is the pawn superclass for all the pawns in the game
 * aside from the common attributes below these pawns take advantage of the UnityEvents class
 * 
 * Just create a class that inherits from this one by replacing "MonoBehaviour" with "Pawn"
 * CAUTION: Do the above INSIDE your CHILD CLASS the syntax is ChildClass : ParentCLass
 * just like you see below, Pawn is inheriting from MonoBehavior so it can be attached
 * to GameObjects within the Unity Engine.
 * 
 * The Action() Method below is meant to be overriden within a child class to create effects.
 * For the Ranger, the Action is shooting arrows. For the Shield, it might be a charge or 
 * something that can slow enemies around him.
 * 
 * Whatever it may be, create an override function within the child class with the name Action()
 * to take advantage of this functionality. If you ever wish to add an event and call it yourself,
 * you must first declare it like it is below the header "Pawn Events" in this script. Then,
 * simply .Invoke() it when you wish for the event to trigger.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pawn : MonoBehaviour
{
    [Header("General Pawn Attributes")]
    [SerializeField, Range(0, 1000),Tooltip("the movement speed of the pawn")]
    protected float speed;
    [SerializeField, Tooltip("The Rigidbody 2D on this pawn")]
    protected Rigidbody2D rb;

    [Header("Pawn Attack Settings")]
    [SerializeField, Range(0, 1), Tooltip("the amount of time it takes an action to refresh")]
    protected float coolDownTime;
    [SerializeField, Tooltip("this is the cool down countdown, time is subtract from this to tell when an action can be performed again.")]
    protected float coolDown;
    [SerializeField, Tooltip("the Layer Mask that the enemies reside in")]
    protected LayerMask enemyLayer; 
    [SerializeField, Range(0, 100), Tooltip("the amount of damage a pawn's attack does")]
    protected float damage;
    
    [Header("Pawn Events")]
    [Tooltip("Calls the class's special action. Usually used on controllers to allow the player to attack.")]
    public UnityEvent OnAction;

    [Header("AI Settings")]
    [SerializeField, Range(0, 100), Tooltip("the range of an attack")]
    protected float attackRange;
    protected string typeId;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        //get the rigidbody of this game object
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //lower cooldown as time passes, but not below 0 or above its cooldown time
        coolDown = Mathf.Clamp(coolDown - Time.deltaTime, 0f, coolDownTime);
    }
    /// <summary>
    /// Moves the pawn in the given direction using their rigidbody.
    /// Takes in a Vector2D.
    /// </summary>
    /// <param name="movement"></param>
    public void Move(Vector2 movement)
    {
        //move the rigidbody by Vector 2 multiplied by speed.
        rb.velocity = new Vector2(movement.x * speed * Time.fixedDeltaTime, movement.y * speed * Time.fixedDeltaTime);
    }

    /// <summary>
    /// An action that pawns can perform. This can be anything from a melee attack, to a heal.
    /// </summary>
    public virtual void Action() 
    {
        //override in children
    }

    /// <summary>
    /// Returns the movement speed of the pawn
    /// </summary>
    /// <returns>float</returns>
    public float GetSpeed() 
    {
        return speed;
    }

    /// <summary>
    /// Returns the attack range of the pawn
    /// </summary>
    /// <returns>float</returns>
    public float GetAttackRange() 
    {
        return attackRange;
    }

    public string GetTypeId() 
    {
        return typeId;
    }
}
