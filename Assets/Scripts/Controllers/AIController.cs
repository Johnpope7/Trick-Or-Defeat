using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{
    [Header("Controlled Enemies Array")]
    public static GameObject[] enemies;

    [Header("Target Settings")]
    [SerializeField]
    protected GameObject target;
    [SerializeField]
    protected Transform targetTf;
    [SerializeField]
    protected float distanceToTarget;
    protected Vector2 movement;
    [SerializeField]
    protected LayerMask playerLayer;

    [Header("AI State")]
    [SerializeField]
    protected AIState aiState = AIState.Chase;
    [SerializeField]
    protected EnemyType enemyType;
    [SerializeField, Range(0, 10)]
    protected float timer = 3f;
    protected enum AIState { Chase, Attack, Idle }
    protected enum EnemyType { Melee, Ranged }


    // Start is called before the first frame update
    protected override void Start()
    {
        //DO NOT call base, we dont want a single pawn on this we need an array of them

        foreach (var enemy in enemies) 
        {
            string type = pawn.GetTypeId();
            SetEnemyType(type);
            if (enemyType == EnemyType.Ranged) 
            {
                enemy.GetComponent<ERangedPawn>().target = target;
            }
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        foreach (var enemy in enemies)
        {

            if (enemyType == EnemyType.Melee)
            {
                switch (aiState)
                {
                    case AIState.Chase:
                        Chase(target, targetTf);
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
                        Chase(target, targetTf);
                        StartCoroutine(StateChangeTimer());
                        break;
                    case AIState.Attack:
                        pawn.OnAction.Invoke();
                        ChangeState(AIState.Chase);
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
    }
    protected void Chase(GameObject target, Transform targetTf)
    {
        movement = (targetTf.position - transform.position) * pawn.GetSpeed() * Time.deltaTime;
        pawn.Move(movement);
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
