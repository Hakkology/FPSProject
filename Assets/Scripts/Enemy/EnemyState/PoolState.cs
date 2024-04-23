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

    public PoolState(EnemyController controller, EnemyData data, Animator animator, NavMeshAgent agent, EnemyHealth health)
    {
        enemyController = controller;
        enemyData = data;
        enemyAnimator = animator;
        enemyAgent = agent;
        enemyHealth = health;
    }

    public void Init()
    {
        enemyAnimator.Rebind();
        enemyAnimator.Update(0f);
        enemyHealth.ResetHealth();
        enemyAgent.isStopped = true;
        enemyAgent.ResetPath();
        enemyAgent.enabled = false;

        Debug.Log($"{enemyData.enemyName} is entering PoolState");
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
