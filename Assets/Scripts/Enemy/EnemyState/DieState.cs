using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class DieState : IEnemyState
{
    private EnemyData enemyData;
    private Animator enemyAnimator;
    private NavMeshAgent enemyAgent;
    private EnemyCoroutineController enemyCoroutineController;
    private EnemyPooler enemyPooler;  

    private float destructionTimer = 4.5f;

    public DieState(EnemyData data, Animator animator, NavMeshAgent agent, EnemyCoroutineController coroutineController, EnemyPooler pooler)
    {
        enemyData = data;
        enemyAnimator = animator;
        enemyAgent = agent;
        enemyCoroutineController = coroutineController;
        enemyPooler = pooler;  
    }

    public void Init()
    {
        Debug.Log($"{enemyData.enemyName} enemy is entering Death state.");
        enemyAgent.isStopped = true;
        enemyAgent.velocity = Vector3.zero;
        enemyAnimator.SetBool("Die", true);
        enemyCoroutineController.ExecuteCoroutine(DespawnAfterDelay(destructionTimer));
    }
    
    private IEnumerator DespawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        enemyPooler.DespawnEnemy(enemyAgent.gameObject); 
    }

    public void Update()
    {
        enemyAnimator.SetFloat("Speed", 0);
    }

    public void Cancel()
    {
        
    }
}
