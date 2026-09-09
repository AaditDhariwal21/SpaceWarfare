    using UnityEngine;

    public class Projectile : MonoBehaviour
    {
        public float speed = 10f;
        public float lifetime = 5f;
        public int damage = 5;

        public PlayerStateMachine stateMachine;

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
                collision.collider.GetComponent<PlayerHealth>().TakeDamage(damage);
            }
            else if (collision.collider.CompareTag("Shield"))
            {
                var stateMachine = collision.collider.GetComponentInParent<PlayerStateMachine>();

                if (stateMachine.currentState == PlayerActionState.ShieldRaise)
                {
                    collision.collider.GetComponentInParent<PlayerHealth>().TakeDamage(damage);
                }
                else if (stateMachine.currentState == PlayerActionState.Shielding)
                {
                    collision.collider.GetComponentInParent<PlayerFocus>().AddFocus(damage);
                }
            }
        }
    }
