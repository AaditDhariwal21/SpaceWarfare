    using UnityEngine;

    public class Projectile : MonoBehaviour
    {
        public float speed ;
        public float lifetime = 5f;
        public int damage;
        public int priority;

        Rigidbody2D rb;
        Vector2 direction;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void Start()
        {
            Destroy(gameObject, lifetime);
        }

        public void Launch(Vector2 fireDirection)
        {
            Launch(fireDirection,speed);
        }

        public void Launch(Vector2 fireDirection, float s)
        {
            direction = fireDirection.normalized;
            rb.linearVelocity = direction * s;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Player1") || collision.collider.CompareTag("Player2"))
            {
                collision.collider.GetComponent<PlayerHealth>().TakeDamage(damage);
                Destroy(gameObject);
            }
            else if (collision.collider.CompareTag("Shield"))
            {
                var stateMachine = collision.collider.GetComponentInParent<PlayerStateMachine>();

                if (priority == 2 || stateMachine.currentState == PlayerActionState.ShieldRaise)
                {
                    collision.collider.GetComponentInParent<PlayerHealth>().TakeDamage(damage);
                    Destroy(gameObject);
                }
                else if (stateMachine.currentState == PlayerActionState.Shielding)
                {
                    collision.collider.GetComponentInParent<PlayerFocus>().AddFocus(damage);
                    Destroy(gameObject);
                }
            }
            else if (collision.collider.GetComponent<Projectile>().priority >= priority)
            {
                Destroy(gameObject);
            }
        }
    }
