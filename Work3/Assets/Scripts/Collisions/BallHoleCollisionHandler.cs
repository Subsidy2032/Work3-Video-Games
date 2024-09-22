using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHoleCollisionHandler : MonoBehaviour
{
    private BallHoleCollisionChannel ballHoleCollisionChannel;

    void Start()
    {
        ballHoleCollisionChannel = Beacon.GetInstance().ballHoleCollisionChannel;
        ballHoleCollisionChannel.CollisionDetected += DestroyBall;
    }

    void DestroyBall(GameObject ball)
    {
        Destroy(ball);
    }
}
