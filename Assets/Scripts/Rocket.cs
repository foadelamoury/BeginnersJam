using System.Collections;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 10f;
    public Animator rocketAnimator;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 diagonalDirection = new Vector2(0f, -1f).normalized;
            rb.linearVelocity = diagonalDirection * speed; 
        }

        Destroy(gameObject, lifetime); 
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Trigger entered with: " + collider.gameObject.name);

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
            rocketAnimator.SetTrigger("Damage");
        }

        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject); 
    }
}
