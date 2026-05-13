using UnityEngine;

public class Bull : EnemyAI
{
    void Awake()
    {
        damage = 8f;            // lighter hits
        attackInterval = 0.5f;  // but very fast
        chaseRange = 12f;
        attackRange = 6.3f;
    }

    protected override void DealDamage()
    {
        base.DealDamage();
        Debug.Log("Bull attacks!");
        CameraShake.instance.Shake(0.1f, 0.1f);
    }
}