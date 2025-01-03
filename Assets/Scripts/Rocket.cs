using System.Collections;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 15f;
    public Animator rocketAnimator;

    // Audio clips for the start and collision
    public AudioClip startSound;
    public AudioClip collisionSound;


    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 diagonalDirection = new Vector2(0f, -1f).normalized;
            rb.linearVelocity = diagonalDirection * speed;
        }

        // Initialize the AudioSource and play the start sound
        AudioManager.Instance.PlaySFX(startSound);

        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Trigger entered with: " + collider.gameObject.name);

        // Play the collision sound
        AudioManager.Instance.PlaySFX(collisionSound);

        StartCoroutine(HandleCollision(collider));

        if (collider.CompareTag("Player"))
        {
            PlayerMovment player = collider.GetComponent<PlayerMovment>();
            if (player != null)
            {
                player.Die();
            }
        }
    }

    IEnumerator HandleCollision(Collider2D collider)
    {
        if (rocketAnimator != null)
        {
            Debug.Log("dihiwhd");
            rocketAnimator.SetTrigger("Damage");
        }

        yield return new WaitForSeconds(1.0f);

        Destroy(gameObject);
    }
}
