using UnityEngine;
using TMPro;

public class PlayerMovment : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float crawlSpeed = 2f;
    public float jumpForce = 10f;
    public TextMeshProUGUI pickUpText;
    public Animator animator;
    public Rigidbody2D rb;
    public LayerMask groundLayer;
    public float stairsClimbSpeed = 3f;

//    public AudioClip walkSound; // Add audio clip for walking

    private bool isCrawling = false;
    private bool isGrounded = false;
    private bool isDead = false;
    private SpriteRenderer spriteRenderer;
    private Vector3 pickUpScale;

  //  private bool isPlayingSound = false; // To track if a sound is already playing

    void Start()
    {
        pickUpScale = pickUpText.gameObject.transform.localScale;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        #region Turning the text with the player
        if (transform.localScale.x < 0)
        {
            if (pickUpScale.x > 0)
                pickUpScale = new Vector3(-pickUpScale.x, pickUpScale.y, pickUpScale.z);
        }
        else if (transform.localScale.x > 0)
        {
            if (pickUpScale.x < 0)
                pickUpScale = new Vector3(-pickUpScale.x, pickUpScale.y, pickUpScale.z);
        }

        pickUpText.gameObject.transform.localScale = pickUpScale;
        #endregion

        if (isDead) return;

        float moveInput = Input.GetAxisRaw("Horizontal");
        float speed = isCrawling ? crawlSpeed : moveSpeed;
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        // Flip the character based on movement direction
        if (moveInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        // Handle jumping
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            animator.SetTrigger("Jump");
        }

        // Update animation and play sound
        if (Mathf.Abs(moveInput) > 0)
        {
            animator.SetFloat("Speed", speed);
            //PlayMovementSound(walkSound);
        }
        else
        {
            animator.SetFloat("Speed", 0);
            //StopMovementSound();
        }
    }

    private bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 groundCheckPosition = position + Vector2.down * 1.5f;
        float checkRadius = 0.5f;
        return Physics2D.OverlapCircle(groundCheckPosition, checkRadius, groundLayer);
    }

    //private void PlayMovementSound(AudioClip clip)
    //{
    //    if (!isPlayingSound)
    //    {
    //        AudioManager.Instance.PlaySFX(clip);
    //        isPlayingSound = true;
    //    }
    //}

    //private void StopMovementSound()
    //{
    //    isPlayingSound = false;
    //    // You can add logic here if you want to stop sounds explicitly using AudioManager
    //    AudioManager.Instance.StopSFX();
    //}

    public void Die()
    {
        isDead = true;
        rb.linearVelocity = Vector2.zero;
        animator.SetTrigger("Die");
        //StopMovementSound();
    }
}
