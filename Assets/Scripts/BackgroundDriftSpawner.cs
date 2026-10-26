using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundDriftSpawner : MonoBehaviour
{
    [SerializeField] private RectTransform container;
    [SerializeField] private DriftingObject prefab;
    [SerializeField] private Sprite[] sprites;

    [SerializeField] private Vector2 spawnIntervalRange = new Vector2(3f, 6f);
    [SerializeField] private Vector2 speedRange = new Vector2(20f, 60f);   // canvas units/sec
    [SerializeField] private Vector2 sizeRange = new Vector2(40f, 120f);
    [SerializeField] private Vector2 alphaRange = new Vector2(0.2f, 0.5f);
    [SerializeField] private Vector2 spinRange = new Vector2(-15f, 15f);   // degrees/sec
    [SerializeField] private int maxActive = 6;
    [SerializeField] private float edgeMargin = 150f;

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(spawnIntervalRange.x, spawnIntervalRange.y));
            if (container.childCount < maxActive) Spawn();
        }
    }

    private void Spawn()
    {
        float halfW = container.rect.width / 2f;
        float halfH = container.rect.height / 2f;
        int dir = Random.value < 0.5f ? 1 : -1;               // 1 = moving right (spawns on left)
        float limit = halfW + edgeMargin;

        DriftingObject obj = Instantiate(prefab, container);
        RectTransform rt = (RectTransform)obj.transform;
        rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = new Vector2(-dir * limit, Random.Range(-halfH, halfH));

        float size = Random.Range(sizeRange.x, sizeRange.y);
        rt.sizeDelta = new Vector2(size, size);

        Image img = obj.GetComponent<Image>();
        img.sprite = sprites[Random.Range(0, sprites.Length)];
        img.preserveAspect = true;
        Color c = img.color;
        c.a = Random.Range(alphaRange.x, alphaRange.y);
        img.color = c;

        obj.Init(dir, Random.Range(speedRange.x, speedRange.y),
                 Random.Range(spinRange.x, spinRange.y), limit);
    }
}