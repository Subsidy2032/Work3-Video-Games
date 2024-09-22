using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHoleCollisionHandler : MonoBehaviour
{
    private BallHoleCollisionChannel ballHoleCollisionChannel;
    [SerializeField] GameObject[] wallBorders; // 0. Top 1. Bottom 2. Left 3. Right\
    [SerializeField] GameObject ball;

    void Start()
    {
        ballHoleCollisionChannel = Beacon.GetInstance().ballHoleCollisionChannel;
        ballHoleCollisionChannel.CollisionDetected += DestroyBall;
    }

    void DestroyBall(GameObject hole)
    {
        Collider2D holeColider = hole.GetComponent<Collider2D>();
        GameObject wall = FindWall(hole);
        DisableWallColliderInLocation(wall, hole);

        //TODO: Destroy ball
    }

    private GameObject FindWall(GameObject hole)
    {
        float tolerance = 0.1f; // Small tolerance for position comparison

    if (Mathf.Abs(hole.transform.position.y - wallBorders[0].transform.position.y) < tolerance)
    {
        return wallBorders[0];
    }
    else if (Mathf.Abs(hole.transform.position.y - wallBorders[1].transform.position.y) < tolerance)
    {
        return wallBorders[1];
    }
    else if (Mathf.Abs(hole.transform.position.x - wallBorders[2].transform.position.x) < tolerance)
    {
        return wallBorders[2];
    }
    else if (Mathf.Abs(hole.transform.position.x - wallBorders[3].transform.position.x) < tolerance)
    {
        return wallBorders[3];
    }

    return null;
    }

    private void DisableWallColliderInLocation(GameObject wall, GameObject hole)
    {
        if (wall == null)
        {
            Debug.LogError("Wall reference is null");
            return;
        }
        Collider2D wallCollider = wall.GetComponent<BoxCollider2D>();
        Collider2D holeCollider = hole.GetComponent<BoxCollider2D>();
        Collider2D ballCollider = ball.GetComponent<CircleCollider2D>();

        if (wallCollider == null)
        {
            Debug.Log("Wall collider is null");
        }

        if (holeCollider == null)
        {
            Debug.Log("Box collider is null");
        }

        if (wallCollider != null && holeCollider != null)
        {
            Debug.Log("Entered condition");

            Bounds holeBounds = holeCollider.bounds;

            Bounds wallBounds = wallCollider.bounds;

            Physics2D.IgnoreCollision(holeCollider, wallCollider, true);
            Physics2D.IgnoreCollision(ballCollider, wallCollider, true);
        }
    }

}
