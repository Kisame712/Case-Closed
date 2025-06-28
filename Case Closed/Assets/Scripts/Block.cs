using UnityEngine;

public class Block : MonoBehaviour
{
    public float blockSpeed;

    Rigidbody2D bulletRb;
    Boss boss;

    int direction;
    private void Start()
    {
        boss = FindFirstObjectByType<Boss>();
        bulletRb = GetComponent<Rigidbody2D>();
        if (boss.transform.rotation == Quaternion.Euler(0f, 0f, 0f))
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }

    }

    void Update()
    {
        bulletRb.linearVelocity = new Vector2(blockSpeed * direction, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();
            player.TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
