using UnityEngine;

public class LadderClimbing : MonoBehaviour
{
    private bool isClimbing = false;
    private bool canClimb = false; // Flag to check if the player can start climbing
    private float climbSpeed = 3f;
    private Rigidbody2D rb; // Use Rigidbody2D for 2D physics
    private Animator animator;

    // References to both colliders on the player
    public Collider2D bodyCollider; // The player's main collider (e.g., body)
    public Collider2D ladderTriggerCollider; // The player's collider for detecting ladder interaction

    private float lockedXPosition; // Store the x-axis position when climbing starts

    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        if (animator == null)
            animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if the player is near the ladder and presses the "W" key to start climbing
        if (canClimb && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            isClimbing = true;
            lockedXPosition = transform.position.x; // Lock the player's x-axis position
        }

        // Climbing logic (move vertically only when climbing)
        if (isClimbing)
        {
            // Disable gravity while climbing
            rb.gravityScale = 0f;

            // Get vertical input
            float vertical = Input.GetAxis("Vertical");

            // Lock x-axis movement by directly setting position
            rb.linearVelocity = new Vector2(0, vertical * climbSpeed);
            transform.position = new Vector3(lockedXPosition, transform.position.y, transform.position.z);

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

    // Detect when the player's body collider is near the ladder
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladder") && other == ladderTriggerCollider) // Check if the body collider interacts with the ladder
        {
            Debug.Log("Player's body is near the ladder");
            canClimb = true; // Allow the player to start climbing when near the ladder
        }
    }

    // Detect when the player exits the ladder trigger
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder") && other == ladderTriggerCollider) // Check if the body collider exits the ladder area
        {
            Debug.Log("Player's body is no longer near the ladder");
            canClimb = false; // Disable climbing when exiting the ladder
            isClimbing = false; // Stop climbing animation
            animator.SetBool("IsClimbing", false); // Stop climbing animation
        }
    }
}
