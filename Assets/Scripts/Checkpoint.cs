using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Checkpoint : MonoBehaviour
{
    public GameObject flag; // Reference to the flag GameObject
    private bool isActive = false;

    private void Start()
    {
        if (flag != null)
        {
            // Ensure the flag's SpriteRenderer is initially disabled
            flag.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isActive)
        {
            ActivateCheckpoint(other.gameObject);
        }
    }

    private void ActivateCheckpoint(GameObject player)
    {
        isActive = true;

        // Enable the flag's SpriteRenderer
        if (flag != null)
        {
            flag.GetComponent<SpriteRenderer>().enabled = true;
        }

        // Notify the player's script about the new checkpoint
        PlayerMovment playerMovement = player.GetComponent<PlayerMovment>();
        if (playerMovement != null)
        {
            playerMovement.UpdateCheckpoint(transform.position);
        }
    }
}
