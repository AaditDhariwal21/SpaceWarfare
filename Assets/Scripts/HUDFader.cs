using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class HUDFader : MonoBehaviour
{
    [SerializeField] private float fadedAlpha = 0.3f;
    [SerializeField] private float fadeSpeed = 6f;    // alpha units per second
    [SerializeField] private float inset = 0f;        // shrink ship rect in pixels to ignore transparent edges

    private RectTransform hudArea;
    private CanvasGroup group;
    private SpriteRenderer ship;
    private Camera cam;
    private readonly Vector3[] corners = new Vector3[4];

    private void Awake()
    {
        group = GetComponent<CanvasGroup>();
        cam = Camera.main;
        if (hudArea == null) hudArea = (RectTransform)transform;
    }

    public void Bind(SpriteRenderer shipRenderer) => ship = shipRenderer;

    private void Update()
    {
        if (ship == null) return;

        // Overlay canvas: world corners are already in screen pixels
        hudArea.GetWorldCorners(corners);
        Rect hud = Rect.MinMaxRect(corners[0].x, corners[0].y, corners[2].x, corners[2].y);

        Bounds b = ship.bounds;
        Vector2 min = cam.WorldToScreenPoint(b.min);
        Vector2 max = cam.WorldToScreenPoint(b.max);
        Rect shipRect = Rect.MinMaxRect(min.x + inset, min.y + inset, max.x - inset, max.y - inset);

        float target = hud.Overlaps(shipRect) ? fadedAlpha : 1f;
        group.alpha = Mathf.MoveTowards(group.alpha, target, fadeSpeed * Time.unscaledDeltaTime);
    }
}