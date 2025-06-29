using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public int moveSpeed;
    public float attackRange;

    Rigidbody2D enemyRb;
    Player player;
    Animator enemyAnim;
    private bool isFacingRight = false;
    private Vector2 direction;

    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        enemyAnim = GetComponent<Animator>();
        player = FindFirstObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
        if(Vector2.Distance(enemyRb.position, player.gameObject.transform.position) < attackRange)
        {
            enemyAnim.SetBool("isAttacking", true);
            direction = new Vector2(player.gameObject.transform.position.x - enemyRb.position.x, 0f).normalized;
            enemyRb.linearVelocity = moveSpeed * direction;
        }
        else
        {
            enemyAnim.SetBool("isAttacking", false);
        }

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.forward, attackRange);
    }

    void Flip()
    {

        if (enemyRb.linearVelocity.x < 0 && isFacingRight == true)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            isFacingRight = false;
        }
        else if (enemyRb.linearVelocity.x > 0 && isFacingRight == false)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            isFacingRight = true;
        }

    }
}
