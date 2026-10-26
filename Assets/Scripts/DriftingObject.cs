using UnityEngine;

public class DriftingObject : MonoBehaviour
{
    private RectTransform rt;
    private int dir;
    private float speed, spin, limit;

    public void Init(int dir, float speed, float spin, float limit)
    {
        rt = (RectTransform)transform;
        this.dir = dir;
        this.speed = speed;
        this.spin = spin;
        this.limit = limit;
    }
    
    private void Update()
    {
        rt.anchoredPosition += Vector2.right * dir * speed * Time.deltaTime;
        rt.Rotate(0f, 0f, spin * Time.deltaTime);

        if (rt.anchoredPosition.x * dir > limit) Destroy(gameObject);
    }
}