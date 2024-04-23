using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour, IExperience
{
    // References
    public EnemyData enemyStats;
    public Transform playerTransform;
    private EnemyController enemyController;
    private NavMeshAgent enemyAgent;
    private Animator enemyAnimator;
    private Rigidbody enemyRigidbody;
    private EnemyHealth enemyHealth;
    private EnemyCoroutineController enemyCoroutineController;

    void Awake()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
        enemyRigidbody = GetComponent<Rigidbody>();
        enemyHealth = GetComponent<EnemyHealth>();
        enemyCoroutineController = GetComponent<EnemyCoroutineController>();

        enemyController = new EnemyController(enemyStats, enemyAnimator, enemyAgent, playerTransform, transform, enemyHealth, enemyCoroutineController);
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
