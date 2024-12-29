using UnityEngine;
using TMPro;

public class PlayerMovment : MonoBehaviour
{
    public float moveSpeed = 5f;
    public TextMeshProUGUI pickUpText;
    public float crawlSpeed = 2f;
    public float jumpForce = 10f;
    public Animator animator;
    public Rigidbody2D rb;
    public LayerMask groundLayer;
    public float stairsClimbSpeed = 3f; // Speed for climbing stairs

    private bool isCrawling = false;
    private bool isGrounded = false;
    private bool isDead = false;
    private bool isOnStairs = false; // Check if the player is on stairs
    private SpriteRenderer spriteRenderer;
    private Vector3 pickUpScale;
    public float speed;
    void Start()
    {
        pickUpScale = pickUpText.gameObject.transform.localScale;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        #region Turning the text with the player
        if (transform.localScale.x < 0)
            if (pickUpScale.x >0)
                pickUpScale = new Vector3(-pickUpScale.x, pickUpScale.y, pickUpScale.z);
        else if (transform.localScale.x > 0)
            if(pickUpScale.x <0)
            pickUpScale = new Vector3(-pickUpScale.x, pickUpScale.y, pickUpScale.z);
        
        pickUpText.gameObject.transform.localScale = pickUpScale;
        #endregion

        if (isDead) return;
        float moveInput = Input.GetAxisRaw("Horizontal");
         speed = isCrawling ? crawlSpeed : moveSpeed ;
        rb.linearVelocity = new Vector2(moveInput * speed , rb.linearVelocity.y);
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

        if (moveInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);
        }

        if (Input.GetButtonDown("Jump") && IsGrounded() && !isCrawling)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            animator.SetTrigger("Jump");
            Debug.Log("Jump Triggered");
        }
        if (Input.GetKey(KeyCode.C))
        {
            isCrawling = !isCrawling;
        }

        if (Mathf.Abs(moveInput) == 0)
        {
        Debug.Log("Move Input: " + moveInput);
            animator.SetFloat("Speed", 5.0f);
            
        }
        else
        {
            animator.SetFloat("Speed", 7);

           
        }

        // Update animator parameters
        //animator.SetFloat("Speed", Mathf.Abs(moveInput));

        animator.SetBool("IsCrawling", isCrawling);
        //animator.SetBool("IsGrounded", IsGrounded());
        // Update sorting order for depth

    }

    private bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 groundCheckPosition = position + Vector2.down * 1.5f;
        //groundCheckPosition = position + Vector2.down * 0.1f;
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
