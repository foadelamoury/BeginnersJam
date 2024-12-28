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

    private bool isCrawling = false;
    private bool isGrounded = false;
    private bool isDead = false;

    private Vector3 pickUpScale;
    bool xPosFlipped = false;

    void Start()
    {
        pickUpScale = pickUpText.gameObject.transform.localScale;

    }

    void Update()
    {
        #region Turning the text with the player
        if (transform.localScale.x < 0)
        {
            xPosFlipped = true;
        }
        else if (transform.localScale.x > 0)
        {
            xPosFlipped = false;
        }

        if (xPosFlipped && pickUpScale.x > 0)
        {
            pickUpScale = new Vector3(-pickUpScale.x, pickUpScale.y, pickUpScale.z);
        }
        else if (!xPosFlipped && pickUpScale.x < 0)
        {
            pickUpScale = new Vector3(-pickUpScale.x, pickUpScale.y, pickUpScale.z);
        }

        // Apply the new scale to the transform
        pickUpText.gameObject.transform.localScale = pickUpScale;

        #endregion

        if (isDead) return;
        float moveInput = Input.GetAxisRaw("Horizontal");
        float speed = isCrawling ? crawlSpeed : moveSpeed;
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

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

        Debug.Log("Move Input: " + moveInput);
        if (Mathf.Abs(moveInput) == 0)
        {
            animator.SetFloat("Speed", -1.0f);
            
        }
        else
        {
            animator.SetFloat("Speed", Mathf.Abs(moveInput));

           
        }

  

        animator.SetBool("IsCrawling", isCrawling);
        //animator.SetBool("IsGrounded", IsGrounded());

    }

    private bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 groundCheckPosition = position + Vector2.down * 1.5f; 
        float checkRadius = 0.2f;
        return Physics2D.OverlapCircle(groundCheckPosition, checkRadius, groundLayer);
    }

    public void Die()
    {
        isDead = true;
        rb.linearVelocity = Vector2.zero;
        animator.SetTrigger("Die");
    }
}
