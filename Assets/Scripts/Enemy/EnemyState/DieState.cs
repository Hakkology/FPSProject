using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class DieState : IEnemyState
{
    private EnemyData enemyData;
    private EnemyController enemyController;
    private Animator enemyAnimator;
    private EnemyHealth enemyHealth;
    private NavMeshAgent enemyAgent;
    private EnemyCoroutineController enemyCoroutineController;
    private EnemyPooler enemyPooler;  

    private float destructionTimer = 4.5f;

    public DieState(EnemyController controller, EnemyData data, Animator animator, NavMeshAgent agent, EnemyHealth health, EnemyCoroutineController coroutineController, EnemyPooler pooler)
    {
        enemyController = controller;
        enemyData = data;
        enemyAnimator = animator;
        enemyAgent = agent;
        enemyHealth = health;
        enemyCoroutineController = coroutineController;
        enemyPooler = pooler;  
    }

    public void Init()
    {
        Debug.Log($"{enemyData.enemyName} enemy is entering Death state.");
        enemyHealth.GiveExperience(enemyData.enemyExperiencePoints);
        enemyAgent.isStopped = true;
        enemyAgent.velocity = Vector3.zero;
        enemyAnimator.SetTrigger("Death");
        enemyCoroutineController.ExecuteCoroutine(DespawnAfterDelay(destructionTimer));
    }
    
    private IEnumerator DespawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        enemyController.ChangeState(EnemyState.Pool);
    }

    public void Update()
    {
        enemyAnimator.SetFloat("Speed", 0);
    }

    public void Cancel()
    {
        enemyAnimator.ResetTrigger("Death");
        enemyCoroutineController.StopAllCoroutines();
    }
}
