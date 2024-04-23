using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Idle,
    Patrol,
    Chase,
    Attack,
    Die,
    Flee
}

public class EnemyController
{
    private EnemyState currentState;
    
    private IEnemyState idleState;
    private IEnemyState patrolState;
    private IEnemyState chaseState;
    private IEnemyState attackState;
    private IEnemyState dieState;
    private IEnemyState fleeState;
    
    private IEnemyState currentEnemyState;

    public EnemyController(EnemyData data, Animator animator, NavMeshAgent agent, EnemyProjectile projectile, Transform playerTransform, Transform transform, PlayerHealth playerHealth, EnemyHealth health, EnemyCoroutineController coroutineController)
    {
        idleState = new IdleState(this, data, animator, transform, playerTransform);
        patrolState = new PatrolState(this, data, agent, animator, transform, playerTransform);
        chaseState = new ChaseState(this, data, agent, animator, transform, playerTransform, health);
        attackState = new AttackState(this, coroutineController, data, agent, animator, projectile, transform, playerTransform, health, playerHealth);
        dieState = new DieState(data, animator, agent, coroutineController);
        fleeState = new FleeState(this, data, agent, animator, transform, playerTransform, health, coroutineController);

        currentState = EnemyState.Idle;
        currentEnemyState = idleState;
        currentEnemyState.Init();
    }

    public void ChangeState(EnemyState newState)
    {
        if (currentState == newState) return;

        currentEnemyState.Cancel();

        currentState = newState;
        switch (currentState)
        {
            case EnemyState.Idle:
                currentEnemyState = idleState;
                break;
            case EnemyState.Patrol:
                currentEnemyState = patrolState;
                break;
            case EnemyState.Chase:
                currentEnemyState = chaseState;
                break;
            case EnemyState.Attack:
                currentEnemyState = attackState;
                break;
            case EnemyState.Die:
                currentEnemyState = dieState;
                break;
            case EnemyState.Flee:
                currentEnemyState = fleeState;
                break;
        }

        currentEnemyState.Init();
    }

    public void Update()
    {
        currentEnemyState.Update();
    }
}