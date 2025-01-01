using UnityEngine;
using System.Collections; // Required for coroutines

public class LadderClimbing : MonoBehaviour
{
    private bool isClimbing = false;
    private bool canClimb = false; // Flag to check if the player can start climbing
    private float climbSpeed = 5f;
    private Rigidbody2D rb; // Use Rigidbody2D for 2D physics
    private Animator animator;
    public LayerMask groundLayer;

    // References to both colliders on the player
    public Collider2D bodyCollider; // The player's main collider (e.g., body)
    public Collider2D ladderTriggerCollider; // The player's collider for detecting ladder interaction
    public CapsuleCollider2D ladderCollider2D;

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
        Debug.Log(canClimb);
        if (!ladderCollider2D.isTrigger && canClimb && (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)))
        {
            ladderCollider2D.isTrigger = true;
            isClimbing = true;
            lockedXPosition = transform.position.x;
        }

        // Climbing logic
        if (isClimbing)
        {
            // Exit climbing if grounded and moving horizontally
            if (((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) ||
                 (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))) && IsGrounded())
            {
                StopClimbing();
            }
            else
            {
                rb.gravityScale = 0f;

                // Get vertical input
                float vertical = Input.GetAxis("Vertical");

                // Lock x-axis movement by directly setting position
                rb.linearVelocity = new Vector2(0, vertical * climbSpeed);
                transform.position = new Vector3(lockedXPosition, transform.position.y, transform.position.z);

                // Pause the animation if no vertical input
                animator.speed = Mathf.Approximately(vertical, 0f) ? 0f : 1f;

                // Trigger climbing animation
                animator.SetBool("IsClimbing", true);
            }
        }
        else
        {
            rb.gravityScale = 1f;
            // Reset climbing animation
            animator.SetBool("IsClimbing", false);
        }
    }

    private bool IsGrounded()
    {
        // Check if the player is on the ground
        Vector2 position = transform.position;
        Vector2 groundCheckPosition = position + Vector2.down * 0.1f; // Adjust offset as needed
        float checkRadius = 0.2f;
        return Physics2D.OverlapCircle(groundCheckPosition, checkRadius, groundLayer) != null;
    }

    private void StopClimbing()
    {
        isClimbing = false;
        rb.gravityScale = 1f; // Restore gravity
        animator.SetBool("IsClimbing", false); // Reset climbing animation
    }

    // Detect when the player's body collider is near the ladder
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladder") && other == ladderTriggerCollider)
        {
            canClimb = true;
        }
    }

    // Detect when the player exits the ladder trigger
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder") && other == ladderTriggerCollider)
        {
            canClimb = false;
            StopClimbing();
            ladderCollider2D.isTrigger = false;
        }
    }
}
