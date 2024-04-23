using UnityEngine;
using UnityEngine.AI;

public class PatrolState : IEnemyState
{
    private EnemyData enemyData;
    private Animator enemyAnimator;
    private EnemyController enemyController;
    private NavMeshAgent enemyAgent;
    private Transform playerTransform;
    private Transform enemyTransform;

    private Vector3 patrolTarget;
    private bool targetReached;
    private float sightCheckInterval = 0.4f;
    private float sightCheckTimer = 0f;

    public PatrolState(EnemyController controller, EnemyData data, NavMeshAgent agent, Animator animator, Transform transform, Transform pTransform)
    {
        enemyController = controller;
        enemyData = data;
        enemyAgent = agent;
        enemyAnimator = animator;
        enemyTransform = transform;
        playerTransform = pTransform;
    }

    public void Init()
    {
        enemyAgent.speed = enemyData.enemyWalkSpeed;
        targetReached = false;
        SelectNewPatrolTarget();
        Debug.Log($"{enemyData.enemyName} is entering PatrolState");
    }

    public void Update()
    {
        enemyAnimator.SetFloat("Speed", 0.5f, .1f, Time.deltaTime);
        HandleSight();
        HandlePatrol();
    }

    public void Cancel()
    {
        Debug.Log($"{enemyData.enemyName} is exiting PatrolState");
    }
    private void HandlePatrol()
    {
        if (!enemyAgent.pathPending && enemyAgent.remainingDistance < .5f && !targetReached)
        {
            targetReached = true; 
            enemyAgent.isStopped = true; 
            enemyController.ChangeState(EnemyState.Idle);
        }
    }

    private void SelectNewPatrolTarget()
    {
        bool validTargetFound = false;
        int attemptCount = 0;
        while (!validTargetFound && attemptCount < 10) 
        {
            Vector3 randomDirection = Random.insideUnitSphere * enemyData.enemyPatrolRange;
            randomDirection += enemyTransform.position;
            NavMeshHit navHit;

            if (NavMesh.SamplePosition(randomDirection, out navHit, enemyData.enemyPatrolRange, -1))
            {
                Vector3 targetDirection = navHit.position - enemyTransform.position;
                RaycastHit hit;

                if (!Physics.Raycast(enemyTransform.position, targetDirection.normalized, out hit, targetDirection.magnitude, LayerMask.GetMask("Obstruction")))
                {

                    patrolTarget = navHit.position;
                    enemyAgent.SetDestination(patrolTarget);
                    enemyAnimator.SetFloat("Speed", 0.5f, .1f, Time.deltaTime);
                    targetReached = false;
                    enemyAgent.isStopped = false;
                    validTargetFound = true; 
                }
            }
            attemptCount++; 
        }

        if (attemptCount == 10)
        {
            enemyController.ChangeState(EnemyState.Idle);
        }
    }

    public void HandleSight()
    {
        sightCheckTimer -= Time.deltaTime;
        if (sightCheckTimer <= 0)
        {
            Vector3 directionToPlayer = playerTransform.position - enemyTransform.position;
            float distance = directionToPlayer.magnitude;
            
            if (distance < enemyData.enemySightRange)
            {
                RaycastHit hit;
                if (Physics.Raycast(enemyTransform.position, directionToPlayer.normalized, out hit, enemyData.enemySightRange))
                {
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        Debug.Log($"{enemyData.enemyName} has clear sight of the player and is initiating chase.");
                        enemyController.ChangeState(EnemyState.Chase);
                    }
                }
            }
            sightCheckTimer = sightCheckInterval; 
        }
    }
}