using UnityEngine;

public class BombEject : MonoBehaviour
{
    Collider2D col;
    Projectile p;
    Quaternion straight;
    float startX;
    bool ejecting;

    void Awake()
    {
        col = GetComponent<Collider2D>();
        p = GetComponent<Projectile>();
    }

    public void Begin(Vector2 ejectDir, float ejectSpeed)
    {
        startX = transform.position.x;
        ejecting = true;
        p.Launch(ejectDir, ejectSpeed);
    }

    void Update()
    {
        if (!ejecting) return;

        if (startX * transform.position.x <= 0f)
        {
            ejecting = false;
            p.Launch(transform.up);   // transform.up was never tilted, so it's already straight
            col.enabled = true;
        }
    }
}
