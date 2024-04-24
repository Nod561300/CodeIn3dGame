using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public int damageAmount = 10;
    public bool isEnemyBullet = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isEnemyBullet && other.CompareTag("Player"))
        {
            // Check if the collided object is the player
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Decrease the player's health
                playerHealth.TakeDamage(damageAmount);
            }

            // Destroy the bullet
            Destroy(gameObject);
        }
    }
}
