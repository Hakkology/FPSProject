using System;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : IEnemyState
{
    private EnemyData enemyData;
    private Animator enemyAnimator;
    private EnemyController enemyController;
    private NavMeshAgent enemyAgent;
    private Transform playerTransform;
    private Transform enemyTransform;
    private EnemyHealth enemyHealth;

    private Vector3 lastPlayerPosition;
    private float destinationUpdateThreshold = 1.0f;
    private float checkInterval = 0.1f; 
    private float checkTimer = 0f;

    public ChaseState(EnemyController controller, EnemyData data, NavMeshAgent agent, Animator animator, Transform transform, Transform pTransform, EnemyHealth health)
    {
        enemyController = controller;
        enemyData = data;
        enemyAgent = agent;
        enemyAnimator = animator;
        enemyTransform = transform;
        enemyHealth = health;
        playerTransform = pTransform;
    }

    public void Init()
    {
        enemyAgent.isStopped = false;
        enemyAgent.speed = enemyData.enemyRunSpeed;
        lastPlayerPosition = playerTransform.position;
        enemyAgent.SetDestination(playerTransform.position);
        Debug.Log($"{enemyData.enemyName} is entering ChaseState");
    }

    public void Update()
    {
        enemyAnimator.SetFloat("Speed", 1.0f, .1f, Time.deltaTime);
        checkTimer -= Time.deltaTime;
        if (checkTimer <= 0)
        {
            HandleChase();
            HandleTransition();
            checkTimer = checkInterval;
        }
    }
    public void Cancel()
    {
        Debug.Log($"{enemyData.enemyName} is exiting ChaseState");
    }
    private void HandleTransition()
    {
        if (enemyHealth.CurrentHealth <= 0)
        {
            enemyController.ChangeState(EnemyState.Die);
        }
        else if (enemyHealth.CurrentHealth <= enemyData.enemyFleeThreshold)
        {
            enemyController.ChangeState(EnemyState.Flee);
        }
    }

    private void HandleChase()
    {
        float distance = Vector3.Distance(playerTransform.position, enemyTransform.position);
        Debug.DrawLine(enemyTransform.position, playerTransform.position, Color.red);  

        if (distance <= enemyData.enemyAttackRange)
        {
            enemyController.ChangeState(EnemyState.Attack);
        }
        else
        {
            if (Vector3.Distance(playerTransform.position, lastPlayerPosition) > destinationUpdateThreshold)
            {
                enemyAgent.SetDestination(playerTransform.position);
                lastPlayerPosition = playerTransform.position;
                Debug.Log("Updated destination to " + playerTransform.position); 
            }
        }
    }
}
