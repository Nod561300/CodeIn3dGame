using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Image healthFillImage;
    public Gradient healthGradient;

    private void Start()
    {
        // Get the PlayerHealth component from the player object
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

        // Set the initial health fill amount and color
        UpdateHealthFill();
    }

    private void Update()
    {
        // Update the health fill amount and color continuously
        UpdateHealthFill();
    }

    private void UpdateHealthFill()
    {
        // Calculate the fill amount based on the player's current health
        float fillAmount = (float)playerHealth.currentHealth / playerHealth.maxHealth;

        // Update the fill amount of the health fill image
        healthFillImage.fillAmount = fillAmount;

        // Update the color of the health fill image based on the gradient
        healthFillImage.color = healthGradient.Evaluate(fillAmount);
    }
}
