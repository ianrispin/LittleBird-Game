using UnityEngine;

public class Wolf : EnemyAI
{
    void Awake()
    {
        damage = 20f;           // hits hard
        attackInterval = 1f;    // but slowly
        chaseRange = 15f;       // good vision
        attackRange = 4.1f;
    }

    protected override void DealDamage()
    {
        base.DealDamage();
        Debug.Log("Wolf attacks!");
        // shake harder than fox
        CameraShake.instance.Shake(0.4f, 0.3f);
    }

    void OnCollisionEnter(Collision collision)
{
    Debug.Log("Wolf hit by: " + collision.gameObject.name);
}

void OnTriggerEnter(Collider other)
{
    Debug.Log("Wolf triggered by: " + other.gameObject.name);
}
}