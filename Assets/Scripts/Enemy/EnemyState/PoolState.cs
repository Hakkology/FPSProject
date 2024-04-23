using System;
using UnityEngine;
using UnityEngine.AI;

public class PoolState : IEnemyState
{
    private EnemyData enemyData;
    private Animator enemyAnimator;
    private EnemyController enemyController;
    private NavMeshAgent enemyAgent;
    private EnemyHealth enemyHealth;
    private EnemyPooler enemyPool;

    public PoolState(EnemyController controller, EnemyData data, Animator animator, NavMeshAgent agent, EnemyHealth health, EnemyPooler pool)
    {
        enemyController = controller;
        enemyData = data;
        enemyAnimator = animator;
        enemyAgent = agent;
        enemyHealth = health;
        enemyPool = pool;
    }

    public void Init()
    {
        Debug.Log($"{enemyData.enemyName} is entering PoolState.");

        if (enemyAgent != null && enemyAgent.gameObject != null) {
            enemyAnimator.Rebind();
            enemyAnimator.Update(0f);
            enemyHealth.ResetHealth();
            enemyAgent.ResetPath();
            enemyAgent.enabled = false;

            enemyPool.DespawnEnemy(enemyAgent.gameObject);
        }
    }

    public void Update()
    {
        HandleTransition();
    }

    private void HandleTransition()
    {
        
    }

    public void Cancel()
    {
        enemyAgent.enabled = true;
        Debug.Log($"{enemyData.enemyName} is exiting PoolState");
    }
}
