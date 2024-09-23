using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBallOnCollision : MonoBehaviour
{
    private BallHoleCollisionChannel ballHoleCollisionChannel;

    void Start()
    {
        Beacon beacon = Beacon.GetInstance();
        ballHoleCollisionChannel = beacon.ballHoleCollisionChannel;
        ballHoleCollisionChannel.CollisionDetected += DestroyBall;
    }

    void DestroyBall(GameObject ball)
    {
        Destroy(ball);
    }
}
