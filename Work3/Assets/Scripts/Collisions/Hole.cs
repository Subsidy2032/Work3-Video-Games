using UnityEngine;

public class Hole : MonoBehaviour
{
    private BallHoleCollisionChannel ballHoleCollisionChannel;
    Beacon beacon = Beacon.GetInstance();

    void Start()
    {
        ballHoleCollisionChannel = beacon.ballHoleCollisionChannel;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball") && collision.isTrigger)
        {
            ballHoleCollisionChannel.InvokeCollisionDetected(collision.gameObject);
        }
    }
}