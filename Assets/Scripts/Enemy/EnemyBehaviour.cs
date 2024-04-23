using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    // References
    public EnemyData enemyStats;
    public EnemyPooler enemyPool;
    
    private Transform playerTransform;
    private PlayerHealth playerHealth;
    private EnemyController enemyController;
    private EnemyCoroutineController enemyCoroutineController;
    private EnemyProjectile enemyProjectile;
    private NavMeshAgent enemyAgent;
    private Animator enemyAnimator;
    private EnemyHealth enemyHealth;


    void Awake()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponentInChildren<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
        enemyProjectile = GetComponent<EnemyProjectile>();
        enemyCoroutineController = GetComponent<EnemyCoroutineController>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
            playerHealth = player.GetComponent<PlayerHealth>();
        }

        enemyController = new EnemyController(enemyStats, enemyAnimator, enemyAgent, enemyProjectile, playerTransform, transform, playerHealth, enemyHealth, enemyCoroutineController, enemyPool);
    }

    void Update()
    {
        enemyController.Update();
    }

}
