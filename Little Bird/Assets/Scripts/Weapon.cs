using StarterAssets;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] float damage = 25f;
    StarterAssetsInputs starterAssetsInputs;

    void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
    }

    void Update()
    {
        if (starterAssetsInputs.shoot)
        {
            RaycastHit hit;
            if (Physics.Raycast(
                Camera.main.transform.position,
                Camera.main.transform.forward,
                out hit,
                Mathf.Infinity))
            {
                Debug.Log("Hit: " + hit.collider.gameObject.name);

                if (!hit.collider.CompareTag("Nest"))
                {
                    Health health = hit.collider.GetComponent<Health>();
                    if (health == null)
                        health = hit.collider.GetComponentInParent<Health>();

                    if (health != null)
                        health.TakeDamage(damage);
                }
            }
            starterAssetsInputs.ShootInput(false);
        }
    }
}