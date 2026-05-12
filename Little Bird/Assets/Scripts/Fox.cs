using UnityEngine;

public class Fox : EnemyAI
{
    void Awake()
    {
        damage = 8f;            // lighter hits
        attackInterval = 0.5f;  // but very fast
        chaseRange = 12f;
        attackRange = 2.1f;
    }

    protected override void DealDamage()
    {
        base.DealDamage();
        Debug.Log("Fox attacks!");
        CameraShake.instance.Shake(0.1f, 0.1f);
    }
}