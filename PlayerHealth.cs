using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public float healthRegenDelay = 3f;
    public int healthRegenAmount = 10;
    private bool isDead;
    private float damageTimer;

    private void Start()
    {
        currentHealth = maxHealth;
        
    }

    private void Update()
    {
        // Start the damage timer when the player takes damage
        if (!isDead)
        {
            if (damageTimer <= 0f)
            {
                StartCoroutine(RegenerateHealth());
            }
            else
            {
                damageTimer -= Time.deltaTime;
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (isDead)
            return;

        currentHealth -= damageAmount;

        // Reset the damage timer when the player takes damage
        damageTimer = healthRegenDelay;

        // Check if the player's health has reached zero
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Perform any necessary actions when the player dies
        isDead = true;
        Debug.Log("Player has died.");
        SceneManager.LoadScene(sceneName: "GameOver");
    }

    private IEnumerator RegenerateHealth()
    {
        while (currentHealth < maxHealth)
        {
            yield return new WaitForSeconds(1f);

            int regenAmount = 5;
            int healthDifference = maxHealth - currentHealth;
            int actualRegenAmount = Mathf.Min(regenAmount, healthDifference);

            currentHealth += actualRegenAmount;

            // Check if the player's health has reached or exceeded the maximum
            if (currentHealth >= maxHealth)
            {
                currentHealth = maxHealth;
                yield break; // Exit the coroutine since the health is fully regenerated
            }
        }
    }
}

