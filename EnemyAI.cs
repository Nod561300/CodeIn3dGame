using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float shootingRange = 10f;
    public float shootingDelay = 1f;
    public float shootingPower = 20f;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public Transform player;
    public float maxChaseDistance = 12f;
    public int damageAmount = 10; // Damage amount for this enemy

    private bool canShoot = true;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void Update()
    {
        // Calculate distance between enemy and player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if player is within shooting range
        if (distanceToPlayer <= shootingRange)
        {
            // Rotate towards the player
            Vector3 targetDirection = player.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

            // Shoot at the player
            if (canShoot)
            {
                Shoot();
                canShoot = false;
                Invoke("ResetShoot", shootingDelay);
            }
        }
        else if (distanceToPlayer <= maxChaseDistance)
        {
            // Move towards the player
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }

    private void Shoot()
    {
        // Instantiate projectile
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

        // Attach BulletDamage script to the projectile
        BulletDamage bulletDamage = projectile.AddComponent<BulletDamage>();
        bulletDamage.damageAmount = damageAmount; // Set the damage amount
        bulletDamage.isEnemyBullet = true; // Set isEnemyBullet to true

        // Add velocity to the projectile
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        projectileRb.velocity = projectile.transform.forward * shootingPower;

        // Destroy projectile after a certain time
        Destroy(projectile, 2f);
    }

    private void ResetShoot()
    {
        canShoot = true;
    }
}
