using UnityEngine;
using Microlight.MicroBar;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    public float currentHealth;

    [Header("Player Health Bar")]
    public MicroBar healthBar;

    [Header("Enemy Health Bar")]
    public GameObject healthBarPrefab;
    public Vector3 barOffset = new Vector3(0, 0.8f, 0);
    private MicroBar enemyBar;

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.Initialize(maxHealth);
        }
        else if (healthBarPrefab != null)
        {
            GameObject canvasGO = new GameObject("EnemyBarCanvas");
            Canvas canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.WorldSpace;
            canvasGO.transform.SetParent(transform);
            canvasGO.transform.localPosition = barOffset;
            canvasGO.transform.localScale = new Vector3(0.005f, 0.005f, 0.005f);

            GameObject bar = Instantiate(healthBarPrefab, canvasGO.transform);
            bar.transform.localPosition = Vector3.zero;
            enemyBar = bar.GetComponent<MicroBar>();
            enemyBar.Initialize(maxHealth);
        }
    }

    void Update()
    {
        if (enemyBar != null)
        {
            enemyBar.transform.parent.LookAt(Camera.main.transform);
            enemyBar.transform.parent.Rotate(0, 180, 0);
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log(gameObject.name + " health: " + currentHealth);

        if (healthBar != null)
            healthBar.UpdateBar(currentHealth);

        if (enemyBar != null)
            enemyBar.UpdateBar(currentHealth);

        if (currentHealth <= 0)
            Die();
    }

    public void RestoreHealth(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);

        if (healthBar != null)
            healthBar.UpdateBar(currentHealth);

        if (enemyBar != null)
            enemyBar.UpdateBar(currentHealth);
    }

    void Die()
    {
        Destroy(gameObject);
    }
}