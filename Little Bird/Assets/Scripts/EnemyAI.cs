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
    // CHANGED: Generalized to 'target' instead of 'player'
    protected Transform target; 
    protected Animator animator;

    // CHANGED: Made Start virtual so child classes like Bull can override it
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        
        // Default target is the player, so other enemies still work perfectly
        target = FindFirstObjectByType<FirstPersonController>().transform;

        agent.updateRotation = false;

        GoToNextPatrolPoint();
    }

    void Update()
    {
        if (!agent.enabled) return;
    
        // CHANGED: Calculate distance to the generic target
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        switch (currentState)
        {
            case EnemyState.Patrol:
                Patrol();
                if (distanceToTarget <= chaseRange)
                    currentState = EnemyState.Chase;
                break;

            case EnemyState.Chase:
                Chase();
                if (distanceToTarget <= attackRange)
                    currentState = EnemyState.Attack;
                else if (distanceToTarget > chaseRange)
                    currentState = EnemyState.Patrol;
                break;

            case EnemyState.Attack:
                Attack();
                if (distanceToTarget > attackRange)
                    currentState = EnemyState.Chase;
                break;
        }

        if (animator != null)
            animator.SetFloat("Speed", agent.velocity.magnitude);

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
        // CHANGED: Chase the target
        agent.SetDestination(target.position);
        if (animator != null)
            animator.SetBool("IsAttacking", false);
    }

    void Attack()
    {
        agent.SetDestination(transform.position);

        // CHANGED: Face the target
        Vector3 direction = (target.position - transform.position).normalized;
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
        // CHANGED: Deal damage to whatever the target is
        Health targetHealth = target.GetComponent<Health>();
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damage);
            
            if (target.GetComponent<FirstPersonController>() != null)
            {
                CameraShake.instance.Shake(0.2f, 0.15f);
            }
        }
    }

    public virtual void Die()
    {
        Debug.Log("Die called on " + gameObject.name);

        if (LevelManager.instance != null)
        {
            LevelManager.instance.AddKill();
        }
        
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