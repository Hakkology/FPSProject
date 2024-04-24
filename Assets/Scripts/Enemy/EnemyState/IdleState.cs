using UnityEngine;
using UnityEngine.AI;

public class IdleState : IEnemyState
{
    private EnemyData enemyData;
    private Animator enemyAnimator;
    private EnemyController enemyController;
    private NavMeshAgent enemyAgent;
    private Transform enemyTransform;
    private Transform playerTransform;

    private float transitionTimer;
    private float sightCheckInterval  = 0.4f; 
    private float sightCheckTimer  = 0f;
    public IdleState(EnemyController controller, EnemyData data, Animator animator, NavMeshAgent agent, Transform transform, Transform pTransform)
    {
        enemyController = controller;
        enemyData = data;
        enemyAnimator = animator;
        enemyTransform = transform;
        enemyAgent = agent;
        playerTransform = pTransform;
    }

    public void Init()
    {
        enemyAgent.enabled = true;
        enemyAgent.ResetPath();
        transitionTimer = Random.Range(3f, 5f);
        Debug.Log($"{enemyData.enemyName} is entering idle state.");
    }

    public void Update()
    {
        enemyAnimator.SetFloat("Speed", 0, .1f, Time.deltaTime);
        HandleSight();
        HandleTransition();
    }

    public void Cancel()
    {
        Debug.Log($"{enemyData.enemyName} is exiting idle state.");
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
                        enemyController.ChangeState(EnemyState.Chase);
                    }
                }
            }
            sightCheckTimer = sightCheckInterval; 
        }
    }
    private void HandleTransition()
    {
        if (transitionTimer > 0)
        {
            transitionTimer -= Time.deltaTime;
            if (transitionTimer <= 0)
            {
                enemyController.ChangeState(EnemyState.Patrol);
            }
        }
    }
}