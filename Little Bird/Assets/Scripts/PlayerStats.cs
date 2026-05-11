using UnityEngine;
using System.Collections;
using StarterAssets;

public class PlayerStats : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100f;
    private float currentHealth;

    private FirstPersonController fpsController;

    void Start()
    {
        currentHealth = maxHealth;
        fpsController = GetComponent<FirstPersonController>();
    }

    // --- Carrot: speed boost ---
    public void BoostSpeed(float amount, float duration)
    {
        StartCoroutine(SpeedBoost(amount, duration));
    }

    IEnumerator SpeedBoost(float amount, float duration)
    {
        fpsController.MoveSpeed += amount;
        fpsController.SprintSpeed += amount;
        Debug.Log("Speed boosted!");
        yield return new WaitForSeconds(duration);
        fpsController.MoveSpeed -= amount;
        fpsController.SprintSpeed -= amount;
        Debug.Log("Speed back to normal");
    }

    // --- Eggplant: health boost ---
    public void BoostHealth(float amount)
{
    PlayerHealth health = GetComponent<PlayerHealth>();
    if (health != null)
    {
        health.currentHealth = Mathf.Min(health.currentHealth + amount, health.maxHealth);
        Debug.Log("Health restored!");
    }
}
    // --- Broccoli: jump boost ---
    public void BoostJump(float amount, float duration)
    {
        StartCoroutine(JumpBoost(amount, duration));
    }

    IEnumerator JumpBoost(float amount, float duration)
    {
        fpsController.JumpHeight += amount;
        Debug.Log("Jump boosted!");
        yield return new WaitForSeconds(duration);
        fpsController.JumpHeight -= amount;
        Debug.Log("Jump back to normal");
    }

    public float GetHealth() { return currentHealth; }
}