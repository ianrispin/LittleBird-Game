using UnityEngine;

public enum VeggieType { Carrot, Eggplant, Mushroom }

public class Collectible : MonoBehaviour
{
    public VeggieType veggieType;
    public float effectAmount = 5f;
    public float effectDuration = 5f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Print collection message
            Debug.Log(veggieType.ToString() + " collected!");

            PlayerStats stats = other.GetComponent<PlayerStats>();
            if (stats != null)
            {
                switch (veggieType)
                {
                    case VeggieType.Carrot:
                        stats.BoostSpeed(effectAmount, effectDuration);
                        break;
                    case VeggieType.Eggplant:
                        stats.BoostHealth(effectAmount);
                        break;
                    case VeggieType.Mushroom:
                        stats.BoostJump(effectAmount, effectDuration);
                        break;
                }
            }

            Destroy(gameObject);
        }
    }
}