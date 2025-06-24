using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    [SerializeField] float jumpSpeed;
    [SerializeField] float moveSpeed;


    Vector2 playerInput;
    Rigidbody2D playerRb;
    Animator playerAnim;

    BoxCollider2D playerFeetCollider;
    private bool isFacingRight = true;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerFeetCollider = GetComponent<BoxCollider2D>();
        playerAnim = GetComponent<Animator>();
    }

    void Update()
    {
        Run();
        Flip();
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
}
