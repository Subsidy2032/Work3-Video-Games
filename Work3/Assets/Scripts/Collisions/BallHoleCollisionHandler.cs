using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHoleCollisionHandler : MonoBehaviour
{
    private BallHoleCollisionChannel ballHoleCollisionChannel;
    GameObject[] wallBorders; // 0. Top 1. Bottom 2. Left 3. Right

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
        if (hole.transform.position.y == wallBorders[0].transform.position.y)
        {
            return wallBorders[0];
        }
        else if (hole.transform.position.y == wallBorders[1].transform.position.y)
        {
            return wallBorders[1];
        }
        else if (hole.transform.position.y == wallBorders[2].transform.position.x)
        {
            return wallBorders[2];
        }
        else if (hole.transform.position.y == wallBorders[3].transform.position.x)
        {
            return wallBorders[3];
        }

        return null;
    }

    private void DisableWallColliderInLocation(GameObject wall, GameObject hole)
    {
        BoxCollider2D wallCollider = wall.GetComponent<BoxCollider2D>();
        Vector2 holePosition = hole.transform.position;
        Vector2 wallSize = wallCollider.size;
        Vector2 wallCenter = wallCollider.offset;

        // Calculate the position of the hole relative to the wall
        Vector2 holeLocalPosition = holePosition - (Vector2)wall.transform.position;

        // Determine if the hole is horizontally or vertically aligned with the wall
        bool isHorizontal = Mathf.Abs(wallSize.x) > Mathf.Abs(wallSize.y);

        // If horizontal wall, split it vertically
        if (isHorizontal)
        {
            float leftEdge = wallCenter.x - wallSize.x / 2;
            float rightEdge = wallCenter.x + wallSize.x / 2;

            float holeLeft = holeLocalPosition.x - hole.GetComponent<Collider2D>().bounds.extents.x;
            float holeRight = holeLocalPosition.x + hole.GetComponent<Collider2D>().bounds.extents.x;

            // Create two new smaller colliders that exclude the hole
            CreateNewWallCollider(wall, leftEdge, holeLeft, wallSize.y);
            CreateNewWallCollider(wall, holeRight, rightEdge, wallSize.y);
        }
        else
        {
            // For vertical walls, split it horizontally
            float bottomEdge = wallCenter.y - wallSize.y / 2;
            float topEdge = wallCenter.y + wallSize.y / 2;

            float holeBottom = holeLocalPosition.y - hole.GetComponent<Collider2D>().bounds.extents.y;
            float holeTop = holeLocalPosition.y + hole.GetComponent<Collider2D>().bounds.extents.y;

            // Create two new smaller colliders that exclude the hole
            CreateNewWallCollider(wall, wallSize.x, bottomEdge, holeBottom);
            CreateNewWallCollider(wall, wallSize.x, holeTop, topEdge);
        }

        // Disable the original collider
        wallCollider.enabled = false;
    }

    // Helper method to create new smaller colliders
    private void CreateNewWallCollider(GameObject wall, float start, float end, float otherAxisSize)
    {
        float size = end - start;
        if (size <= 0) return;

        GameObject newColliderObj = new GameObject("WallPartCollider");
        newColliderObj.transform.parent = wall.transform;
        newColliderObj.transform.localPosition = new Vector2((start + end) / 2, wall.transform.position.y);  // Adjust for horizontal/vertical
        BoxCollider2D newCollider = newColliderObj.AddComponent<BoxCollider2D>();

        newCollider.size = new Vector2(size, otherAxisSize);
        newCollider.offset = Vector2.zero;
    }

}
