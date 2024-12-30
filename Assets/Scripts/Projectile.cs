using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    public float speed = 5f; // Speed of the projectile
    public float lifetime = 5f; // Time before the projectile is destroyed

    private void Start()
    {
        // Destroy the projectile after a set time
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // Move the projectile left on the x-axis
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Ensure the player GameObject has the "Player" tag
        {
            // Call the Die method on the player
            PlayerMovment player = collision.GetComponent<PlayerMovment>();
            if (player != null)
            {
                player.Die();
            }

            // Destroy the projectile
            Destroy(gameObject);
        }
    }
}
