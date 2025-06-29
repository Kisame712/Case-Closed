using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public int health;

    public GameObject block;
    public Transform spawnPosition;
    public GameObject finalLetter;

    public GameObject bloodEffect;

    public GameObject mainSource1;
    public GameObject mainSource2;
    public GameObject bgSource1;
    public GameObject bgSource2;

    public Slider bossHealth;

    Animator bossAnim;
    Rigidbody2D bossRb;
    private bool isFacingRight = true;

    public bool isInvulnerable = false;

    void Start()
    {
        bossAnim = GetComponent<Animator>();
        bossRb = GetComponent<Rigidbody2D>();
        bossHealth.minValue = 0;
        bossHealth.maxValue = 20;
        bossHealth.value = bossHealth.maxValue;
    }

    void Update()
    {
        if(health <= 10)
        {
            mainSource1.SetActive(false);
            bgSource1.SetActive(false);
            mainSource2.SetActive(true);
            bgSource2.SetActive(true);

            bossAnim.SetTrigger("enraged");
        }
        if(health <= 0)
        {
            bossAnim.SetTrigger("death");
        }

        Flip();
    }

    public void AttackOne()
    {
        Instantiate(block, spawnPosition.position, spawnPosition.rotation);
    }

    public void BossDeath()
    {
        Instantiate(finalLetter, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void TakeDamage(int damageAmount)
    {
        if (isInvulnerable)
        {
            return;
        }
        health -= damageAmount;
        bossHealth.value = health;
        Instantiate(bloodEffect, transform.position, transform.rotation);

    }

    void Flip()
    {

        if (bossRb.linearVelocity.x < 0 && isFacingRight == true)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            isFacingRight = false;
        }
        else if (bossRb.linearVelocity.x > 0 && isFacingRight == false)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            isFacingRight = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();
            player.TakeDamage(1);
        }
    }
}
