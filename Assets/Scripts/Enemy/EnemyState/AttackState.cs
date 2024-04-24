using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : IEnemyState
{
    private EnemyData enemyData;
    private EnemyController enemyController;
    private EnemyCoroutineController enemyCoroutineController;
    private NavMeshAgent enemyAgent;
    private Animator enemyAnimator;
    private EnemyHealth enemyHealth;
    private EnemyProjectile enemyProjectile;
    private PlayerHealth playerHealth;
    private Transform enemyTransform;
    private Transform playerTransform;

    private float checkTimer = 0f;

    public AttackState(EnemyController controller, EnemyCoroutineController coroutineController, EnemyData data, NavMeshAgent agent, Animator animator, EnemyProjectile projectile, Transform transform, Transform playerTransform, EnemyHealth health, PlayerHealth pHealth)
    {
        enemyController = controller;
        enemyCoroutineController = coroutineController;
        enemyData = data;
        enemyAgent = agent;
        enemyProjectile = projectile;
        enemyAnimator = animator;
        enemyTransform = transform;
        enemyHealth = health;
        playerHealth = pHealth;
        this.playerTransform = playerTransform;
    }

    public void Init()
    {
        enemyAgent.isStopped = true;
        enemyAgent.speed = enemyData.enemyRunSpeed;
        
        Attack();
    }

    public void Update()
    {
        enemyAnimator.SetFloat("Speed", 0, .1f, Time.deltaTime);
        HandleHealthTransition();
    }
    public void Cancel()
    {
        enemyAnimator.ResetTrigger("Attack");
        enemyCoroutineController.StopAllCoroutinesSafe();
    }

    private void Attack()
    {
        enemyCoroutineController.ExecuteCoroutine(HandleAttack());
    }

    private IEnumerator HandleAttack()
    {
        enemyAnimator.SetTrigger("Attack");
        if (enemyData.attackType == AttackType.Melee)
        {
            playerHealth.TakeDamage(enemyData.enemyAttackDamage);
            Debug.Log($"Melee attack on player for {enemyData.enemyAttackDamage} damage.");
        }
        else if (enemyData.attackType == AttackType.Ranged)
        {
            LaunchProjectile();
            Debug.Log($"Ranged attack on player for {enemyData.enemyAttackDamage} damage.");
        }
        yield return new WaitForSeconds(enemyData.enemyAttackCooldown);
        enemyController.ChangeState(EnemyState.Chase);
    }
    private void LaunchProjectile()
    {
        if (enemyData.enemyProjectilePrefab != null)
        {
            GameObject projectileObject = GameObject.Instantiate(enemyData.enemyProjectilePrefab, enemyTransform.position, Quaternion.identity);
            EnemyProjectile projectile = projectileObject.GetComponent<EnemyProjectile>();

            if (projectile != null)
            {
                projectile.enemyData = this.enemyData;
                projectile.Initialize(playerTransform); 
            }

            // Projeyi 3 saniye sonra yok et
            GameObject.Destroy(projectileObject, 3.0f);
        }
    }

    void HandleHealthTransition(){

        if (enemyHealth.CurrentHealth <= 0)
        {
            enemyController.ChangeState(EnemyState.Die);
            return;
        }
        if (enemyHealth.CurrentHealth <= enemyData.enemyFleeThreshold)
        {
            enemyController.ChangeState(EnemyState.Flee);
            return;
        }
    }

}