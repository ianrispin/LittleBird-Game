using UnityEngine;
using UnityEngine.AI;
using StarterAssets;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState { Patrol, Chase, Attack }
    public EnemyState currentState = EnemyState.Patrol;

    [Header("Detection")]
    public float chaseRange = 10f;
    public float attackRange = 1.5f;

    [Header("Patrol")]
    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;

    [Header("Attack")]
    public float damage = 10f;
    public float attackInterval = 1f;
    private float attackTimer = 0f;

    protected NavMeshAgent agent;
    protected Transform player;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindFirstObjectByType<FirstPersonController>().transform;
        GoToNextPatrolPoint();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case EnemyState.Patrol:
                Patrol();
                if (distanceToPlayer <= chaseRange)
                    currentState = EnemyState.Chase;
                break;

            case EnemyState.Chase:
                Chase();
                if (distanceToPlayer <= attackRange)
                    currentState = EnemyState.Attack;
                else if (distanceToPlayer > chaseRange)
                    currentState = EnemyState.Patrol;
                break;

            case EnemyState.Attack:
                Attack();
                if (distanceToPlayer > attackRange)
                    currentState = EnemyState.Chase;
                break;
        }
    }

    void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GoToNextPatrolPoint();
    }

    void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;
        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    void Chase()
    {
        agent.SetDestination(player.position);
    }

    void Attack()
    {
        agent.SetDestination(transform.position); // stop moving
        transform.LookAt(player);

        attackTimer += Time.deltaTime;
        if (attackTimer >= attackInterval)
        {
            attackTimer = 0f;
            DealDamage();
        }
    }

    protected virtual void DealDamage()
    {
        Health playerHealth = player.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
            CameraShake.instance.Shake(0.2f, 0.15f);
        }
    }
}