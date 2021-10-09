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
    protected Vector2 movement; //the movement direction of the pawn
    [SerializeField]
    protected LayerMask playerLayer; //the layer mask the players are on

    [Header("Enemy Type"), SerializeField]
    protected EnemyType enemyType; //the type of enemy the AI controller is interfacing with (Melee or Ranged)
    protected enum EnemyType { Melee, Ranged } //an enumeration of enemy types
    [SerializeField, Range(0, 10)]
    protected float timer = 3f; //timer for the melee state change coroutine
    


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
                switch (pawn.aiState)
                {
                    case Pawn.AIState.Chase:
                        movement = (targetTf.position - pawn.transform.position) * pawn.GetSpeed() * Time.deltaTime;
                        pawn.Move(movement);
                        if (Vector3.Distance(target.transform.position, pawn.transform.position) <= pawn.GetAttackRange())
                        {
                            ChangeState(Pawn.AIState.Attack, pawn);
                        }
                        break;
                    case Pawn.AIState.Attack:
                        pawn.OnAction.Invoke();
                        if (Vector3.Distance(target.transform.position, pawn.transform.position) > pawn.GetAttackRange())
                        {
                            ChangeState(Pawn.AIState.Chase, pawn); 
                        }
                        break;
                    case Pawn.AIState.Idle:
                        //do nothing
                        break;
                }
            }
            else if (enemyType == EnemyType.Ranged)
            {
                switch (pawn.aiState)
                {
                    case Pawn.AIState.Chase:
                        movement = (targetTf.position - pawn.transform.position) * pawn.GetSpeed() * Time.deltaTime;
                        pawn.Move(movement);
                        if (Vector3.Distance(target.transform.position, pawn.transform.position) <= pawn.GetAttackRange())
                        {
                            ChangeState(Pawn.AIState.Attack, pawn);
                        }
                        break;
                    case Pawn.AIState.Attack:
                        pawn.OnAction.Invoke();
                        if (Vector3.Distance(target.transform.position, pawn.transform.position) > pawn.GetAttackRange())
                        {
                            ChangeState(Pawn.AIState.Chase, pawn);
                        }
                        break;
                    case Pawn.AIState.Idle:
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
        else if (type == "Ranged")
        {
            enemyType = EnemyType.Ranged;
        }
    }
    protected void ChangeState(Pawn.AIState newState, Pawn pawn)
    {
        //change state
        pawn.aiState = newState;
        Debug.Log("Game object: " + gameObject.name + ", is changing state to " + newState);
    }

    public void SetTarget(GameObject newTarget, Transform newTargetTf)
    {
        target = newTarget;
        targetTf = newTargetTf;
    }

    IEnumerator StateChangeTimer(Pawn.AIState aiState, Pawn pawn) 
    {
        yield return new WaitForSeconds(timer);
        ChangeState(aiState, pawn);
    }
}
