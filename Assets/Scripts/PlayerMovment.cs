using UnityEngine;
using TMPro;

public class PlayerMovment : MonoBehaviour
{
    public float moveSpeed = 8f;
    public TextMeshProUGUI pickUpText;
    public float jumpForce = 10f;
    public Animator animator;
    public Rigidbody2D rb;
    public LayerMask groundLayer;
    [SerializeField] float offset = 1.5f;
    [SerializeField] float checkRadius = 0.5f;
    private bool isGrounded = false;
    private bool isDead = false;
    private SpriteRenderer spriteRenderer;
    private Vector3 pickUpScale;
    private Vector3 currentCheckpoint; // To store the current checkpoint position
    private Vector3 initialPosition; // Stores the game's initial position
    [SerializeField] bool killPlayer = false;
    void Start()
    {
        pickUpScale = pickUpText.gameObject.transform.localScale;
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialPosition = new Vector3(10.899930953979493f, -19.441625595092775f, 0f);
        currentCheckpoint = initialPosition;
    }

    void Update()
    {
        #region Turning the text with the player
        if (transform.localScale.x < 0)
            if (pickUpScale.x > 0)
                pickUpScale = new Vector3(-pickUpScale.x, pickUpScale.y, pickUpScale.z);
            else if (transform.localScale.x > 0)
                if (pickUpScale.x < 0)
                    pickUpScale = new Vector3(-pickUpScale.x, pickUpScale.y, pickUpScale.z);

        pickUpText.gameObject.transform.localScale = pickUpScale;
        #endregion

        if (isDead) return;

        float moveInput = Input.GetAxisRaw("Horizontal");
        float speed = moveSpeed;
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        if (moveInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        // Jumping logic
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            animator.SetTrigger("Jump");
        }

        // Falling animation logic
        if (!IsGrounded() && rb.linearVelocity.y < 0)
        {
            animator.SetBool("IsFalling", true);
        }
        else
        {
            animator.SetBool("IsFalling", false);
        }

        // Movement animation logic
        if (Mathf.Abs(moveInput) == 0)
        {
            animator.SetFloat("Speed", 0.0f);
        }
        else
        {
            animator.SetFloat("Speed", 8.0f);
        }
        if (killPlayer)
        {
            Die();
        }

    }


    private bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 groundCheckPosition = position + Vector2.down * offset;
        return Physics2D.OverlapCircle(groundCheckPosition, checkRadius, groundLayer);
    }


    public void Die()
    {
        isDead = true;
        rb.linearVelocity = Vector2.zero;
        animator.SetTrigger("Die");
        // Respawn after a short delay
        Invoke(nameof(Respawn), 1.5f);
    }

    private void Respawn()
    {
        transform.position = currentCheckpoint; // Respawn at the current checkpoint
        isDead = false;
        animator.ResetTrigger("Die");
    }

    public void UpdateCheckpoint(Vector3 checkpointPosition)
    {
        currentCheckpoint = checkpointPosition;
    }

}
