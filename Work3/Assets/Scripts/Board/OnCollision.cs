using UnityEngine;

public class OnCollision : MonoBehaviour
{
    private BallWallCollision i_BallWallCollision;
    void Awake()
    {
        i_BallWallCollision = Beacon.GetInstance().i_BallWallCollision;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            i_BallWallCollision.InvokeCollisionDetected();

        }
    }
}
