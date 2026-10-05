using System;
using UnityEngine;

public class ClusterMissile : MonoBehaviour
{
    public GameObject clusterBomb;
    private Rigidbody2D rb;
    public float speed;

    private float spawnOffsetY = 1f;
    bool hasSplit = false;

    void Awake()
    {
        rb=GetComponent<Rigidbody2D>();
    }

    public void LaunchMissile(Vector2 fireDirection)
    {
        Vector2 direction = fireDirection.normalized;
        rb.linearVelocity = direction * speed;
    }

    void Update()
    {
        if(Math.Abs(transform.position.x) <= 2.5f && !hasSplit)
        {
            hasSplit = true;
            Split();
        }
    }

    void Split()
    {
        float angle = Mathf.Atan(spawnOffsetY / Mathf.Abs(transform.position.x)) * Mathf.Rad2Deg;
        SpawnBomb(+1, angle);
        SpawnBomb(0, angle);
        SpawnBomb(-1, angle);
        Destroy(gameObject);
    }

    void SpawnBomb(int side, float angle)
    {
         Vector2 dir = Quaternion.Euler(0, 0, side * angle) * transform.up;   // angled direction only
        GameObject b = Instantiate(clusterBomb, transform.position, transform.rotation);
        b.GetComponent<BombEject>().Begin(dir, speed);
    }

}
