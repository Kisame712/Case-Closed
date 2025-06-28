using UnityEngine;

public class Boss : MonoBehaviour
{
    public int health;

    public GameObject block;
    public Transform spawnPosition;
    public GameObject finalLetter;

    Animator bossAnim;
    Rigidbody2D bossRb;
    private bool isFacingRight = false;

    void Start()
    {
        bossAnim = GetComponent<Animator>();
        bossRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(health <= 10)
        {
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
        health -= damageAmount;

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
}
