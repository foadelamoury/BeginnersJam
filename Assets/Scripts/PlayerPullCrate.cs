using UnityEngine;

public class PlayerPullCrate : MonoBehaviour
{
    public float pullSpeed = 2.0f; // Speed at which the crate is pulled
    private GameObject crateToPull = null; // Reference to the crate
    private Rigidbody2D playerRb;
    private bool isPulling = false;
    public PlayerMovment playerMovment;
    private bool isHolding = false;


    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Check if the E key is held and there's a crate nearby
        if (PressToHold() && crateToPull != null)
        {
            isPulling = true;
            playerMovment.moveSpeed *= -1;

        }
        else
        {
            isPulling = false;

        }

        if (isPulling && crateToPull != null)
        {
            PullCrate();
        }
    }

    private void PullCrate()
    {
        // Get the direction the player is moving (left or right)
        float moveX = Input.GetAxis("Horizontal");

        if (Mathf.Abs(moveX) > 0)
        {
            // Calculate the target position
            Vector3 targetPosition = transform.position;
            targetPosition.y = crateToPull.transform.position.y; // Maintain the y position of the crate

            // Lerp the crate position towards the player's position
            crateToPull.transform.position = Vector3.Lerp(
                crateToPull.transform.position,
                targetPosition,
                Time.deltaTime * pullSpeed
            );
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object is a crate
        if (collision.gameObject.CompareTag("Crate"))
        {
            crateToPull = collision.gameObject; // Assign the crate reference
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Remove the crate reference if the player moves away
        if (collision.gameObject == crateToPull)
        {
            crateToPull = null;
            playerMovment.moveSpeed *= -1;

        }
    }

    private bool PressToHold()
    {
        if (Input.GetKey(KeyCode.E))
        {
            isHolding = !isHolding;
        }
        return isHolding;
       
    }
}
