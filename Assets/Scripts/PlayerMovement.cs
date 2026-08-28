using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
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
    }
}
