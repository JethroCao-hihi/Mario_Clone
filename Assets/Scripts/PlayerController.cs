using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private bool useMobileControls = false;
    private float groundCheckRadius = 0.2f;
    private bool canDoubleJump;
    private bool isGrounded;
    private Rigidbody2D rb;
    private Animator animator;
    private float moveInput;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GetInput();
        CheckGround();
        HandleJump();
        HandleFlip();
        UpdateAnimation();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    private void GetInput()
    {
        if (useMobileControls && MobileControls.Instance != null)
        {
            moveInput = MobileControls.Instance.GetMoveInput();
        }
        else
        {
            moveInput = Input.GetAxisRaw("Horizontal");
        }
    }

    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void HandleMovement()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    private void HandleFlip()
    {
        if (moveInput > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < 0) transform.localScale = new Vector3(-1, 1, 1);
    }

    private void HandleJump()
    {
        bool jumpInput = false;

        if (useMobileControls && MobileControls.Instance != null)
        {
            jumpInput = MobileControls.Instance.GetJumpInput();
        }
        else
        {
            jumpInput = Input.GetKeyDown(KeyCode.Space);
        }

        if (jumpInput)
        {
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                canDoubleJump = true;
                
                if (AudioManager.Instance != null && AudioManager.Instance.jump != null)
                {
                    AudioManager.Instance.PlaySFX(AudioManager.Instance.jump);
                }
            }
            else if (canDoubleJump)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                canDoubleJump = false;

                if (AudioManager.Instance != null && AudioManager.Instance.jump != null)
                {
                    AudioManager.Instance.PlaySFX(AudioManager.Instance.jump);
                }
            }
        }
    }

    private void UpdateAnimation()
    {
        animator.SetBool("isRunning", Mathf.Abs(rb.linearVelocity.x) > 0.1f);
        animator.SetBool("isJumping", !isGrounded);
    }
}