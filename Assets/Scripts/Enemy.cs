using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float detectionRange = 15f; // The maximum range of the FOV
    public float detectionAngle = 90f; // The FOV angle in degrees
    public Transform player; // Reference to the player's transform
    public GameObject projectilePrefab; // The projectile prefab
    public Transform shootPoint; // The point from where the projectile will be shot
    public float shootCooldown = 7f; // Time between consecutive shots (in seconds)

    private float cooldownTimer; // Timer to track cooldown

    void Start()
    {
        // Initialize the cooldownTimer to delay the first shot
        cooldownTimer = shootCooldown;
    }

    void Update()
    {
        // Update the cooldown timer
        cooldownTimer -= Time.deltaTime;

        // Check if the player is within FOV
        if (IsPlayerInFOV())
        {
            // Shoot a projectile if cooldown has elapsed
            if (cooldownTimer <= 0f)
            {
                ShootProjectile();
                cooldownTimer = shootCooldown; // Reset the cooldown timer
            }
        }
    }

    private bool IsPlayerInFOV()
    {
        // Calculate the direction to the player
        Vector2 directionToPlayer = player.position - transform.position;

        // Check the distance to the player
        float distanceToPlayer = directionToPlayer.magnitude;
        if (distanceToPlayer > detectionRange)
        {
            Debug.Log("Player is out of detection range: " + distanceToPlayer);
            return false;
        }

        Debug.Log("Player is within detection range: " + distanceToPlayer);
        return true; // Temporarily ignore FOV angle for testing
    }

    private void ShootProjectile()
    {
        // Instantiate the projectile
        Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        Debug.Log("Projectile Shot!");
    }
}
