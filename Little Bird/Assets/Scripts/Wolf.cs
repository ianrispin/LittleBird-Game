using UnityEngine;

public class Wolf : EnemyAI
{
    void Awake()
    {
        damage = 20f;           // hits hard
        attackInterval = 2f;    // but slowly
        chaseRange = 15f;       // good vision
        attackRange = 2.1f;
    }

    protected override void DealDamage()
    {
        base.DealDamage();
        Debug.Log("Wolf attacks!");
        // shake harder than fox
        CameraShake.instance.Shake(0.4f, 0.3f);
    }
}