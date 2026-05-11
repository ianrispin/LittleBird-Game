using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    FirstPersonController player;
    NavMeshAgent agent;

    [Header("Combat")]
    public float damage = 10f;
    public float damageInterval = 1f;  // seconds between each hit
    public float attackRange = 1.5f;

    private float damageTimer = 0f;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        player = FindFirstObjectByType<FirstPersonController>();
    }

    void Update()
    {
        agent.SetDestination(player.transform.position);

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // Only deal damage when close enough
        if (distanceToPlayer <= attackRange)
        {
            damageTimer += Time.deltaTime;

            if (damageTimer >= damageInterval)
            {
                damageTimer = 0f;

                // Deal damage to player
                Health playerHealth = player.GetComponent<Health>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                }

                // Trigger camera shake
                CameraShake.instance.Shake(0.2f, 0.15f);
            }
        }
        else
        {
            damageTimer = 0f;
        }
    }
}