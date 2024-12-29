using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float crawlSpeed = 2f;
    public float jumpForce = 10f;
    public float stairsClimbSpeed = 3f; // Speed for climbing stairs
    public Animator animator;
    public Rigidbody2D rb;
    public LayerMask groundLayer;

    private bool isCrawling = false;
    private bool isGrounded = false;
    private bool isDead = false;
    private bool isOnStairs = false; // Check if the player is on stairs
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isDead) return;

        float moveInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        float speed = isCrawling ? crawlSpeed : moveSpeed;

        // Normal horizontal movement
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        // Stairs climbing logic
        if (isOnStairs)
        {
            if (Input.GetKey(KeyCode.W)) // Climb up
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, stairsClimbSpeed);
            }
            else if (Input.GetKey(KeyCode.S)) // Climb down
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, -stairsClimbSpeed);
            }
            else
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); // Stop vertical movement
            }
        }

        // Flip sprite based on direction
        if (moveInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);
        }

        // Jump logic
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            animator.SetTrigger("Jump");
        }

        // Toggle crawling
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrawling = !isCrawling;
        }

        // Update animator parameters
        animator.SetFloat("Speed", Mathf.Abs(moveInput));
        animator.SetBool("IsCrawling", isCrawling);

        // Update sorting order for depth
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100);
        }
    }

    private bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 groundCheckPosition = position + Vector2.down * 0.1f;
        float checkRadius = 0.2f;
        return Physics2D.OverlapCircle(groundCheckPosition, checkRadius, groundLayer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Stairs"))
        {
            isOnStairs = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Stairs"))
        {
            isOnStairs = false;
        }
    }

    public void Die()
    {
        isDead = true;
        rb.linearVelocity = Vector2.zero;
        animator.SetTrigger("Die");
    }
}
