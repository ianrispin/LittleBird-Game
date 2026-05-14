using UnityEngine;

public class Bull : EnemyAI
{
    void Awake()
    {
        damage = 8f;            // lighter hits
        attackInterval = 0.5f;  // but very fast
        chaseRange = 100f;
        attackRange = 6.3f;
    }

    // Override Start to change the target
    protected override void Start()
    {
        // Run the base setup (grabs the NavMeshAgent, Animator, etc.)
        base.Start();

        GameObject nest = GameObject.Find("Nest");
        if (nest != null)
        {
            target = nest.transform;
        }
        else
        {
            Debug.LogWarning("Bull could not find the Nest object in the scene!");
        }
    }

    protected override void DealDamage()
    {
        base.DealDamage();
        Debug.Log("Bull attacks the nest!");
        
        if (target != null && target.GetComponent<StarterAssets.FirstPersonController>() != null)
        {
            CameraShake.instance.Shake(0.1f, 0.1f);
        }
    }
}