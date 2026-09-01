using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 5f;
    public int damage = 5;

    Rigidbody2D rb;
    Vector2 direction;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 fireDirection)
    {
        direction = fireDirection.normalized;
        rb.linearVelocity = direction * speed;
        Destroy(gameObject, lifetime);
    }

     void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            GameObject player = collision.collider.gameObject;

            if (player.GetComponent<PlayerShield>().isShieldActive())
            {
                player.GetComponent<PlayerFocus>().AddFocus(damage);
            }
            else
            {
                player.GetComponent<PlayerHealth>().TakeDamage(damage);
            }  
        }      
    }
}
