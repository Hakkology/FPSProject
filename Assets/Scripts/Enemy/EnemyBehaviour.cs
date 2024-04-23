using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour, IExperience
{
    // References
    public EnemyData enemyStats;

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
        playerHealth = playerTransform.gameObject.GetComponent<PlayerHealth>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
            playerHealth = player.GetComponent<PlayerHealth>();
        }

        enemyController = new EnemyController(enemyStats, enemyAnimator, enemyAgent, enemyProjectile, playerTransform, transform, playerHealth, enemyHealth, enemyCoroutineController);
    }

    void Update()
    {
        enemyController.Update();
    }

    public void GiveExperience()
    {
        throw new System.NotImplementedException();
    }
}
