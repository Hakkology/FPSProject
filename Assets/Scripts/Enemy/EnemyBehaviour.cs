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

    void Awake()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
        enemyRigidbody = GetComponent<Rigidbody>();
        enemyHealth = GetComponent<EnemyHealth>();

        enemyController = new EnemyController(enemyStats, enemyAnimator, enemyAgent, playerTransform, transform, enemyHealth);
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
