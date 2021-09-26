using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{
    [Header("Target Settings")]
    [SerializeField]
    protected GameObject target;
    [SerializeField]
    protected Transform targetTf; //the transform of the target player
    protected float distanceToTarget; //distance from a specific pawn to a target player
    protected Vector2 movement; //the movement direction of the pawn
    [SerializeField]
    protected LayerMask playerLayer; //the layer mask the players are on

    [Header("AI State")]
    [SerializeField]
    protected AIState aiState = AIState.Chase; //states for the AI, initial state is chase
    [SerializeField]
    protected EnemyType enemyType; //the type of enemy the AI controller is interfacing with (Melee or Ranged)
    [SerializeField, Range(0, 10)]
    protected float timer = 3f; //timer for the melee state change coroutine
    protected enum AIState { Chase, Attack, Idle } //an enumeration of AI States
    protected enum EnemyType { Melee, Ranged } //an enumeration of enemy types


    // Start is called before the first frame update
    protected override void Start()
    {
        target = LevelManager.instance.target;
        targetTf = target.transform;
        foreach (var enemy in LevelManager.instance.enemies) 
        {
            string type = enemy.GetComponent<Pawn>().GetTypeId();
            SetEnemyType(type);
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (target == null) 
        {
            target = LevelManager.instance.target;
        }
        if (targetTf == null) 
        {
            targetTf = target.transform;
        }
        foreach (var enemy in LevelManager.instance.enemies)
        {
           Pawn pawn =  enemy.GetComponent<Pawn>();

            if (enemyType == EnemyType.Melee)
            {
                switch (aiState)
                {
                    case AIState.Chase:
                        movement = (targetTf.position - pawn.transform.position) * pawn.GetSpeed() * Time.deltaTime;
                        pawn.Move(movement);
                        if (distanceToTarget <= pawn.GetAttackRange())
                        {
                            ChangeState(AIState.Attack);
                        }
                        break;
                    case AIState.Attack:
                        pawn.OnAction.Invoke();
                        if (distanceToTarget > pawn.GetAttackRange())
                        {
                            ChangeState(AIState.Chase); 
                        }
                        break;
                    case AIState.Idle:
                        //do nothing
                        break;
                }
            }
            else if (enemyType == EnemyType.Ranged)
            {
                switch (aiState)
                {
                    case AIState.Chase:
                        movement = (targetTf.position - pawn.transform.position) * pawn.GetSpeed() * Time.deltaTime;
                        pawn.Move(movement);
                        StartCoroutine(StateChangeTimer());
                        break;
                    case AIState.Attack:
                        pawn.OnAction.Invoke();
                        if (distanceToTarget > pawn.GetAttackRange())
                        {
                            ChangeState(AIState.Chase);
                        }
                        break;
                    case AIState.Idle:
                        //do nothing
                        break;
                }
            }
        }
    }

    protected void SetEnemyType(string type) 
    {
        if (type == "Melee")
        {
            enemyType = EnemyType.Melee;
        }
        else 
        {
            enemyType = EnemyType.Ranged;
        }
    }
    protected void ChangeState(AIState newState)
    {
        //change state
        aiState = newState;
        Debug.Log("Game object: " + gameObject.name + ", is changing state to " + newState);
    }

    public void SetTarget(GameObject newTarget, Transform newTargetTf)
    {
        target = newTarget;
        targetTf = newTargetTf;
    }

    IEnumerator StateChangeTimer() 
    {
        yield return new WaitForSeconds(timer);
        ChangeState(AIState.Attack);
    }
}
