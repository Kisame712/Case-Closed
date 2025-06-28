using UnityEngine;

public class Boss : MonoBehaviour
{
    public int health;
    public float moveSpeed_phase1;
    public float moveSpeed_phase2;

    public GameObject block;
    public Transform spawnPosition;
    public GameObject finalLetter;

    Animator bossAnim;
    Rigidbody2D bossRb;
    Player player;

    void Start()
    {
        bossAnim = GetComponent<Animator>();
        bossRb = GetComponent<Rigidbody2D>();
        player = FindFirstObjectByType<Player>();
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
}
