using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(horizontalInput) > Mathf.Abs(verticalInput))
        {
            movement.x = horizontalInput;
            movement.y = 0;
        }
        else
        {
            movement.x = 0;
            movement.y = verticalInput;
        }

        movement.Normalize();
    }

    void FixedUpdate()
    {
        rb.velocity = movement * moveSpeed;
    }
}