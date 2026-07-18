using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private float speed = 5f;

    private Rigidbody2D rb;
    private Vector2 serverInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        Keyboard keyboard = Keyboard.current;

        if (keyboard == null)
            return;

        Vector2 input = Vector2.zero;

        if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed)
            input.y += 1;

        if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed)
            input.y -= 1;

        if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)
            input.x -= 1;

        if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
            input.x += 1;

        CmdSetInput(input.normalized);
    }

    [Command]
    private void CmdSetInput(Vector2 input)
    {
        serverInput = Vector2.ClampMagnitude(input, 1f);
    }

    [ServerCallback]
    private void FixedUpdate()
    {
        rb.linearVelocity = serverInput * speed;
    }
}