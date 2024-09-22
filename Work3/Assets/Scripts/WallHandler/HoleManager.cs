using System.Collections;
using UnityEngine;

public class HoleManager : MonoBehaviour
{
    public GameObject holePrefab;
    public GameObject[] wallBorders; // 0- top, 1-bottom, 2-left, 3-right
    public float holeDuration = 5f;
    public float minSpawnTime = 3f;
    public float maxSpawnTime = 8f;
    public float borderWidth; // Width of the wall (same for all walls)

    private GameObject activeHole;
    private Collider2D activeWallCollider;

    void Start()
    {
        StartCoroutine(SpawnHolesRoutine());
    }

    private IEnumerator SpawnHolesRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));

            SpawnHole();
            yield return new WaitForSeconds(holeDuration);

            CloseHole();
        }
    }

    private void SpawnHole()
    {
        Vector2 holePosition = GetRandomHolePosition();
        Quaternion holeRotation = GetHoleRotation(holePosition); // Set correct rotation
        activeHole = Instantiate(holePrefab, holePosition, holeRotation);
        activeHole.SetActive(true);

        // Get the appropriate wall collider based on the position
        activeWallCollider = GetWallCollider(holePosition);

        if (activeWallCollider != null)
        {
            // Temporarily ignore collision between the ball and the wall where the hole is
            Collider2D holeCollider = activeHole.GetComponent<Collider2D>();
            if (holeCollider != null)
            {
                Physics2D.IgnoreCollision(holeCollider, activeWallCollider, true);
            }
        }
    }

    private Collider2D GetWallCollider(Vector2 holePosition)
    {
        // Determine which wall collider to disable based on hole position
        if (holePosition.y == wallBorders[0].transform.position.y) // Top wall
        {
            return wallBorders[0].GetComponent<Collider2D>();
        }
        else if (holePosition.y == wallBorders[1].transform.position.y) // Bottom wall
        {
            return wallBorders[1].GetComponent<Collider2D>();
        }
        else if (holePosition.x == wallBorders[2].transform.position.x) // Left wall
        {
            return wallBorders[2].GetComponent<Collider2D>();
        }
        else if (holePosition.x == wallBorders[3].transform.position.x) // Right wall
        {
            return wallBorders[3].GetComponent<Collider2D>();
        }

        return null; // Default to no collider
    }

    private void CloseHole()
    {
        if (activeHole != null)
        {
            Collider2D holeCollider = activeHole.GetComponent<Collider2D>();
            if (holeCollider != null && activeWallCollider != null)
            {
                // Re-enable the collision between the ball and the wall
                Physics2D.IgnoreCollision(holeCollider, activeWallCollider, false);
            }

            Destroy(activeHole);
        }
    }

    private Vector2 GetRandomHolePosition()
    {
        float randomX = 0f, randomY = 0f;
        float halfBorderWidth = borderWidth / 2f; // Half the width of the border (used for offsets)

        switch (Random.Range(0, 4))
        {
            case 0: // Top wall
                randomX = Random.Range(wallBorders[2].transform.position.x + halfBorderWidth,
                                       wallBorders[3].transform.position.x - halfBorderWidth);
                randomY = wallBorders[0].transform.position.y; // Fixed y at the top wall's position
                break;

            case 1: // Bottom wall
                randomX = Random.Range(wallBorders[2].transform.position.x + halfBorderWidth,
                                       wallBorders[3].transform.position.x - halfBorderWidth);
                randomY = wallBorders[1].transform.position.y; // Fixed y at the bottom wall's position
                break;

            case 2: // Left wall
                randomX = wallBorders[2].transform.position.x; // Fixed x at the left wall's position
                randomY = Random.Range(wallBorders[1].transform.position.y + halfBorderWidth,
                                       wallBorders[0].transform.position.y - halfBorderWidth);
                break;

            case 3: // Right wall
                randomX = wallBorders[3].transform.position.x; // Fixed x at the right wall's position
                randomY = Random.Range(wallBorders[1].transform.position.y + halfBorderWidth,
                                       wallBorders[0].transform.position.y - halfBorderWidth);
                break;
        }

        return new Vector2(randomX, randomY);
    }

    private Quaternion GetHoleRotation(Vector2 holePosition)
    {
        if (holePosition == wallBorders[0].transform.position || holePosition == wallBorders[1].transform.position)
        {
            // Horizontal rotation for Top and Bottom walls
            return Quaternion.identity; // No rotation (horizontal)
        }
        else if (holePosition == wallBorders[2].transform.position || holePosition == wallBorders[3].transform.position)
        {
            // Vertical rotation for Left and Right walls
            return Quaternion.Euler(0, 0, 90); // 90-degree rotation for vertical alignment
        }
        return Quaternion.identity; // Default no rotation
    }
}
