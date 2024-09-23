using UnityEngine;

public class BallHoleCollisionHandler : MonoBehaviour
{
    private BallHoleCollisionChannel ballHoleCollisionChannel;

    void Start()
    {
        Beacon beacon = Beacon.GetInstance();
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