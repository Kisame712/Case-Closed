using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
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

    [Header("Magnet")]
    public float forceX;
    public float forceY;

    [Header("Effects and references")]
    public GameObject bullet;
    public Transform bulletSpawnPoint;
    public GameObject jumpEffect;
    public Transform jumpEffectSpawnPoint;
    public GameObject shootEffect;
    public Transform shootEffectSpawnPoint;
    public GameObject magnetEffect;

    [Header("Timers")]
    public float timeBetweenAttacks;
    public float timeBetweenPulls;
    private float nextAttackTime;
    private float nextPullTime;

    [Header("Health")]
    public int health;

    [Header("Audio Clips")]
    public AudioClip jumpSound;
    public AudioClip shootSound;
    public AudioClip magnetSound;

    Vector2 playerInput;
    Rigidbody2D playerRb;
    Animator playerAnim;

    AudioSource playerAudio;

    BoxCollider2D playerFeetCollider;
    private bool isFacingRight = true;
    float myGravityScale;

    private bool canAttack = true;
    private bool canPull = true;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerFeetCollider = GetComponent<BoxCollider2D>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        myGravityScale = playerRb.gravityScale;
    }

    void Update()
    {
        Run();
        Flip();
        Climbing();
        AttackTimer();
        PullTimer();
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
            playerAudio.PlayOneShot(jumpSound, 0.8f);
            Instantiate(jumpEffect, jumpEffectSpawnPoint.position, jumpEffectSpawnPoint.rotation);
            playerRb.linearVelocity += new Vector2(0f, jumpSpeed);
        }
    }

    void OnAttack(InputValue value)
    {
        if (!canAttack)
        {
            return;
        }

        if (value.isPressed)
        {
            StartCoroutine(ShootBullet());

        }
    }

    void OnSprint(InputValue value)
    {
        if (!canPull)
        {
            return;
        }
        Magnet[] magnets = FindObjectsByType<Magnet>(FindObjectsSortMode.None);
        bool foundOneActiveMagnet = false;
        Magnet activeMagnet = null;
        foreach(Magnet magnet in magnets)
        {
            if(magnet.magnetActive == true)
            {
                activeMagnet = magnet;
                foundOneActiveMagnet = true;
                break;
            }
        }
        if (value.isPressed && foundOneActiveMagnet == true && activeMagnet!=null)
        {
            StartCoroutine(MagnetPull(activeMagnet));
            foundOneActiveMagnet = false;
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

    void AttackTimer()
    {
        if (Time.time > nextAttackTime)
        {
            nextAttackTime = Time.time + timeBetweenAttacks;
            canAttack = true;
        }
    }

    void PullTimer()
    {
        if (Time.time > nextPullTime)
        {
            nextPullTime = Time.time + timeBetweenPulls;
            canPull = true;
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
        Instantiate(shootEffect, shootEffectSpawnPoint.position, shootEffectSpawnPoint.rotation);
        playerAudio.PlayOneShot(shootSound, 0.8f);
        Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        yield return new WaitForSeconds(0.6f);
        gunContainer.SetActive(false);
        canAttack = false;
    }

    IEnumerator MagnetPull(Magnet magnet)
    {
        playerRb.gravityScale = 0;
        grabContainer.SetActive(true);
        playerAnim.SetTrigger("grab");
        Instantiate(magnetEffect, shootEffectSpawnPoint.position, shootEffectSpawnPoint.rotation);
        playerAudio.PlayOneShot(magnetSound, 0.8f);
        Vector2 direction = (magnet.transform.position - transform.position).normalized;
        playerRb.AddForce(new Vector2(direction.x * forceX, direction.y * forceY), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.6f);
        grabContainer.SetActive(false);
        playerRb.gravityScale = myGravityScale;
        canPull = false;
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if(health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
}
