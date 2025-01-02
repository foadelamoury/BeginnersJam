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
    public float stairsClimbSpeed = 3f;
    [SerializeField] float offset = 1.5f;
    [SerializeField] float checkRadius = 0.5f;
    private bool isCrawling = false;
    private bool isGrounded = false;
    private bool isDead = false;
    private SpriteRenderer spriteRenderer;
    private Vector3 pickUpScale;
    private Vector3 currentCheckpoint; // To store the current checkpoint position
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
        float speed = isCrawling ? crawlSpeed : moveSpeed ;
        rb.linearVelocity = new Vector2(moveInput * speed , rb.linearVelocity.y);


        if (moveInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        Debug.Log(IsGrounded());
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            animator.SetTrigger("Jump");
            Debug.Log("Jump Triggered");
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
    }

    public void UpdateCheckpoint(Vector3 checkpointPosition)
    {
        currentCheckpoint = checkpointPosition;
    }

}
