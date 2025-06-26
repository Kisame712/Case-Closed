using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;

    Rigidbody2D bulletRb;
    Player player;

    int direction;
    private void Start()
    {
        player = FindFirstObjectByType<Player>();
        bulletRb = GetComponent<Rigidbody2D>();
        if(player.transform.rotation == Quaternion.Euler(0f, 0f, 0f))
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }

    }

    void Update()
    {
        bulletRb.linearVelocity = new Vector2(speed * direction, 0f);
    }

}
