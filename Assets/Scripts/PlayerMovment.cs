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

    void Update()
    {
        if (isDead) return;
        float moveInput = Input.GetAxisRaw("Horizontal");
        float speed = isCrawling ? crawlSpeed : moveSpeed;
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        if (moveInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            animator.SetTrigger("Jump");
            Debug.Log("Jump Triggered");
        }
        if (Input.GetKey(KeyCode.C))
        {
            isCrawling = !isCrawling;
        }
        var pickUpScale = pickUpText.gameObject.transform.localScale;

        Debug.Log("Move Input: " + moveInput);
        if (Mathf.Abs(moveInput) == 0)
        {
            animator.SetFloat("Speed", -1.0f);
            pickUpScale = new Vector3(-0.01f, pickUpScale.y, pickUpScale.z);
        }
        else
        {
            animator.SetFloat("Speed", Mathf.Abs(moveInput));
            pickUpScale = new Vector3(0.01f, pickUpScale.y, pickUpScale.z);
        }

        // Apply the new scale to the transform
        pickUpText.gameObject.transform.localScale = pickUpScale;

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
