using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movementDirection;
    private bool isColliding = false;
    private Collision2D collision;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        movementDirection = Random.insideUnitCircle.normalized;
        rb.velocity = movementDirection * moveSpeed;
    }

    private void FixedUpdate()
    {
        if (isColliding)
        {
            movementDirection = Vector2.Reflect(movementDirection, collision.contacts[0].normal);
            rb.velocity = movementDirection * moveSpeed;
            collision = null;
            isColliding = false;
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        isColliding = true;
        this.collision = collision;
    }
}
