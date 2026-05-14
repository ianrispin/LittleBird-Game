using UnityEngine;

public class Nest : MonoBehaviour
{
    public void TakeDamage(float amount)
    {
        Health health = GetComponent<Health>();
        if (health != null)
            health.TakeDamage(amount);
    }
}