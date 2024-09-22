using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed = 10f;
    public float jumpAmount = 10f;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
            Debug.LogError("Rigidbody2D could not be found");
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 direction = new Vector2(horizontalInput, 0);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            Jump();

        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);

    }

    void Jump()
    {
        rb.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Ball"))
        {
            Jump();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
