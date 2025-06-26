using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    [Header("Speed Controls")]
    [SerializeField] float jumpSpeed;
    [SerializeField] float moveSpeed;
    [SerializeField] float climbSpeed;

    [Header("Animation Controls")]
    public GameObject idleContainer;
    public GameObject climbContainer;
    public GameObject gunContainer;
    public GameObject grabContainer;

    [Header("Effects and references")]
    public GameObject bullet;
    public Transform bulletSpawnPoint;


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

    void OnAttack(InputValue value)
    {
        if (value.isPressed)
        {
            StartCoroutine(ShootBullet());

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

    IEnumerator ShootBullet()
    {
        gunContainer.SetActive(true);
        playerAnim.SetTrigger("shoot");
        Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        yield return new WaitForSeconds(0.6f);
        gunContainer.SetActive(false);
    }
}
