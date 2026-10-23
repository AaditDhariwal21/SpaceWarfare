using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    [Header("Vertical limits (ship center)")]
    public float minY = -4.5f;
    public float maxY = 4.5f;

    private float moveInput;
    private PlayerInput playerInput;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInput.SwitchCurrentControlScheme(playerInput.defaultControlScheme,Keyboard.current);
    }
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<float>();
    }

    void Update()
    {
        transform.Translate(Vector3.up * moveInput * speed * Time.deltaTime, Space.World);

        Vector3 pos = transform.position;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
    }
}
