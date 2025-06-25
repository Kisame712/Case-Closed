using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    [SerializeField] float jumpSpeed;
    [SerializeField] float moveSpeed;
    [SerializeField] float climbSpeed;

    public GameObject idleContainer;
    public GameObject climbContainer;
    public GameObject gunContainer;
    public GameObject grabContainer;


    Vector2 playerInput;
    Rigidbody2D playerRb;
    Animator playerAnim;

    BoxCollider2D playerFeetCollider;
    private bool isFacingRight = true;
    float myGravityScale;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerFeetCollider = GetComponent<BoxCollider2D>();
        playerAnim = GetComponent<Animator>();
        myGravityScale = playerRb.gravityScale;
    }

    void Update()
    {
        Run();
        Flip();
        Climbing();
    }

    void Run()
    {

        if (playerInput.x != 0)
        {
            playerAnim.SetBool("isRunning", true);
        }
        else
        {
            playerAnim.SetBool("isRunning", false);
        }
        playerRb.linearVelocity = new Vector2(playerInput.x * moveSpeed, playerRb.linearVelocity.y);
    }


    void OnMove(InputValue input)
    {
        playerInput = input.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if (value.isPressed)
        {
            playerAnim.SetTrigger("jump");
            playerRb.linearVelocity = new Vector2(0f, jumpSpeed);
        }
    }

    void Flip()
    {

        if (playerRb.linearVelocity.x < 0 && isFacingRight == true)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            isFacingRight = false;
        }
        else if (playerRb.linearVelocity.x > 0 && isFacingRight == false)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            isFacingRight = true;
        }

    }

    void Climbing()
    {
        if (!playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            playerRb.gravityScale = myGravityScale;
            playerAnim.SetBool("isClimbing", false);
            return;
        }

        

        if (playerInput.y != 0)
        {
            playerAnim.SetBool("isClimbing", true);
            idleContainer.SetActive(false);
            climbContainer.SetActive(true);
        }
        else
        {
            playerAnim.SetBool("isClimbing", false);
            idleContainer.SetActive(true);
            climbContainer.SetActive(false);
        }
        playerRb.gravityScale = 0;
        playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, playerInput.y * climbSpeed);

    }
}
