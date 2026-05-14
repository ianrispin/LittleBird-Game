using UnityEngine;
using UnityEngine.AI;
using StarterAssets;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState { Patrol, Chase, Attack }
    public EnemyState currentState = EnemyState.Patrol;

    [Header("Detection")]
    public float chaseRange = 10f;
    public float attackRange = 2.1f;

    [Header("Patrol")]
    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;

    [Header("Attack")]
    public float damage = 10f;
    public float attackInterval = 1f;
    private float attackTimer = 0f;

    protected NavMeshAgent agent;
    protected Transform player;
    protected Animator animator;

    protected void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        player = FindFirstObjectByType<FirstPersonController>().transform;

        // Fix backwards movement
        agent.updateRotation = false;

        GoToNextPatrolPoint();
    }

    void Update()
    {
        // Stop updating if dead
    if (!agent.enabled) return;
    
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

        // Update speed parameter for walk/gallop transitions
        if (animator != null)
            animator.SetFloat("Speed", agent.velocity.magnitude);

        // Manually rotate based on velocity
        if (agent.velocity.sqrMagnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
        }
    }

    protected void Patrol()
    {
        if (patrolPoints.Length == 0) return;
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GoToNextPatrolPoint();

        if (animator != null)
            animator.SetBool("IsAttacking", false);
    }

    protected void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;
        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    void Chase()
    {
        agent.SetDestination(player.position);
        if (animator != null)
            animator.SetBool("IsAttacking", false);
    }

    void Attack()
    {
        agent.SetDestination(transform.position);

        // Face player while attacking
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);

        if (animator != null)
            animator.SetBool("IsAttacking", true);

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

    public virtual void Die()
{
    Debug.Log("Die called on " + gameObject.name);
    
    if (animator != null)
    {
        Debug.Log("Setting Death trigger");
        animator.SetTrigger("Death");
    }
    else
    {
        Debug.Log("Animator is null!");
    }
    
    agent.enabled = false;
    
    Collider col = GetComponent<Collider>();
    if (col != null)
        col.enabled = false;

    Destroy(gameObject, 3f);
}
}