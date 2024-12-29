using UnityEngine;

public class LadderClimbing : MonoBehaviour
{
    private bool isClimbing = false;
    private bool canClimb = false; // Flag to check if the player can start climbing
    private float climbSpeed = 3f;
    private Rigidbody2D rb; // Use Rigidbody2D for 2D physics
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the 2D Rigidbody component
        animator = GetComponent<Animator>(); // Get the Animator component attached to the player
    }

    void Update()
    {
        // Check if the player is near the ladder and presses the "W" key to start climbing
        if (canClimb && (Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown(KeyCode.UpArrow)))
        {
            isClimbing = true;
            animator.SetBool("IsClimbing", true); // Trigger climbing animation
        }

        // Climbing logic (move vertically and horizontally when climbing)
        if (isClimbing)
        {
            // Disable gravity while climbing
            rb.gravityScale = 0f;

            // Get vertical and horizontal input
            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");

            // Apply both vertical and horizontal movement
            rb.linearVelocity = new Vector2(horizontal * climbSpeed, vertical * climbSpeed);

            // Trigger climbing animation
            animator.SetBool("IsClimbing", true);
        }
        else
        {
            // Restore gravity when not climbing
            rb.gravityScale = 1f;

            // Trigger idle animation when not climbing
            animator.SetBool("IsClimbing", false);
        }
    }

    // Detect when the player is near the ladder
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladder")) // Ensure the ladder has the "Ladder" tag
        {
            Debug.Log("Trigger with ladder");
            canClimb = true; // Allow the player to start climbing when near the ladder
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            canClimb = false; // Disable climbing when exiting the ladder
            isClimbing = false; // Stop climbing animation
            animator.SetBool("IsClimbing", false); // Stop climbing animation
        }
    }
}
