using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 5f; 

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
}
