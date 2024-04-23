using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class FleeState : IEnemyState
{
    private EnemyData enemyData;
    private EnemyController enemyController;
    private NavMeshAgent enemyAgent;
    private Animator enemyAnimator;
    private Transform playerTransform;
    private EnemyHealth enemyHealth;
    private EnemyCoroutineController enemyCoroutineController;
    private Transform enemyTransform;

    private float lateralMoveFrequency = 1f;
    private float lastLateralMove = 0f;
    private float lateralDirection = 1f;

    private float recoverTimer;
    private bool isRecovering = false;

    private Coroutine recoveryCoroutine;

    public FleeState(EnemyController controller, EnemyData data, NavMeshAgent agent, Animator animator, Transform transform, Transform pTransform, EnemyHealth health, EnemyCoroutineController coroutineController)
    {
        enemyController = controller;
        enemyData = data;
        enemyAgent = agent;
        enemyAnimator = animator;
        enemyTransform = transform;
        enemyHealth = health;
        enemyCoroutineController = coroutineController;
        playerTransform = pTransform;

    }

    public void Init()
    {
        enemyAgent.speed = enemyData.enemyRunSpeed;
        enemyAnimator.SetFloat("Speed", 1, .1f, Time.deltaTime);
        recoverTimer = 4.0f;
        isRecovering = false;
        Debug.Log($"{enemyData.enemyName} is entering FleeState");
    }

    public void Update()
    {
        HandleDeath();

        if (!isRecovering)
        {
            HandleFleeing();
            HandleTransition();
        }
    }

    public void Cancel()
    {
        if (recoveryCoroutine != null) {
            enemyCoroutineController.StopCoroutine(recoveryCoroutine);
            recoveryCoroutine = null;
        }
        enemyAnimator.SetBool("isCowering", false);
        isRecovering = false;
        Debug.Log($"{enemyData.enemyName} is exiting FleeState");
    }

    private void HandleTransition()
    {
        if (Vector3.Distance(playerTransform.position, enemyTransform.position) > enemyData.enemyFleeDistance && !isRecovering)
        {
            Debug.Log($"{enemyData.enemyName} has escaped to a safe distance.");

            enemyAnimator.SetFloat("Speed", 0, .1f, Time.deltaTime);
            enemyAgent.isStopped = true;

            isRecovering = true;
            recoveryCoroutine = enemyCoroutineController.StartCoroutine(RecoveryCoroutine());
        }
    }

    private IEnumerator RecoveryCoroutine()
    {
        enemyAnimator.SetBool("isCowering", true);
        yield return new WaitForSeconds(recoverTimer);

        if (isRecovering) {
            enemyAnimator.SetBool("isCowering", false);
            enemyController.ChangeState(EnemyState.Chase);
        }
    }

    private void HandleDeath()
    {
        if (enemyHealth.CurrentHealth <= 0)
        {
            enemyAnimator.SetBool("isCowering", false);
            enemyCoroutineController.StopCoroutine(recoveryCoroutine);
            enemyController.ChangeState(EnemyState.Die);
        }
    }

    private void HandleFleeing()
    {
        Vector3 fleeDirection = (enemyTransform.position - playerTransform.position).normalized;
        Vector3 fleeTarget = enemyTransform.position + fleeDirection * enemyData.enemyFleeDistance;

        if (Time.time > lastLateralMove + lateralMoveFrequency)
        {
            lateralDirection *= -1;
            lastLateralMove = Time.time;
        }

        fleeTarget += enemyTransform.right * lateralDirection * 2;
        enemyAgent.SetDestination(fleeTarget);
    }
}
