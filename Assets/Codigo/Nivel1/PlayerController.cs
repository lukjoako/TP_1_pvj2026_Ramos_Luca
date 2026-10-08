using UnityEngine;
using UnityEngine.InputSystem; // Importante: librería del nuevo Input System

public class PlayerController : MonoBehaviour
{
    [Header("Ajustes de Movimiento")]
    public float speed = 17.0f;
    public float turnSpeed = 90.0f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Lectura usando Keyboard con el nuevo Input System
        float moveVertical = 0f;
        float turnHorizontal = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) moveVertical += 1f;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) moveVertical -= 1f;

            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) turnHorizontal += 1f;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) turnHorizontal -= 1f;
        }

        // Rotación del personaje
        transform.Rotate(0, turnHorizontal * turnSpeed * Time.deltaTime, 0);

        // Movimiento relativo
        Vector3 movement = transform.forward * moveVertical * speed;
        rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z);
    }
}