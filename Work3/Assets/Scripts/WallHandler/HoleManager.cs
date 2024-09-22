using System.Collections;
using UnityEngine;

public class HoleManager : MonoBehaviour
{
    [SerializeField] GameObject holePrefab;
    [SerializeField] GameObject[] wallBorders;
    [SerializeField] float holeDuration = 5f;
    [SerializeField] float minSpawnTime = 3f;
    [SerializeField] float maxSpawnTime = 8f;
    [SerializeField] float borderWidth;

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
        Quaternion holeRotation = GetHoleRotation(holePosition);
        activeHole = Instantiate(holePrefab, holePosition, holeRotation);

        activeWallCollider = GetWallCollider(holePosition);

        if (activeWallCollider != null)
        {
            Collider2D holeCollider = activeHole.GetComponent<Collider2D>();
            if (holeCollider != null)
            {
                Physics2D.IgnoreCollision(holeCollider, activeWallCollider, true);
            }
        }
    }

    private Collider2D GetWallCollider(Vector2 holePosition)
    {
        if (holePosition.y == wallBorders[0].transform.position.y)
        {
            return wallBorders[0].GetComponent<Collider2D>();
        }
        else if (holePosition.y == wallBorders[1].transform.position.y)
        {
            return wallBorders[1].GetComponent<Collider2D>();
        }
        else if (holePosition.x == wallBorders[2].transform.position.x)
        {
            return wallBorders[2].GetComponent<Collider2D>();
        }
        else if (holePosition.x == wallBorders[3].transform.position.x)
        {
            return wallBorders[3].GetComponent<Collider2D>();
        }

        return null;
    }

    private void CloseHole()
    {
        if (activeHole != null)
        {
            Collider2D holeCollider = activeHole.GetComponent<Collider2D>();
            if (holeCollider != null && activeWallCollider != null)
            {
                Physics2D.IgnoreCollision(holeCollider, activeWallCollider, false);
            }

            Destroy(activeHole);
        }
    }

    private Vector2 GetRandomHolePosition()
    {
        float randomX = 0f, randomY = 0f;
        float halfBorderWidth = borderWidth / 2f;

        switch (Random.Range(0, wallBorders.Length))
        {
            case 0:
                randomX = Random.Range(wallBorders[2].transform.position.x + halfBorderWidth,
                                       wallBorders[3].transform.position.x - halfBorderWidth);
                randomY = wallBorders[0].transform.position.y;
                break;

            case 1:
                randomX = Random.Range(wallBorders[2].transform.position.x + halfBorderWidth,
                                       wallBorders[3].transform.position.x - halfBorderWidth);
                randomY = wallBorders[1].transform.position.y;
                break;

            case 2:
                randomX = wallBorders[2].transform.position.x;
                randomY = Random.Range(wallBorders[1].transform.position.y + halfBorderWidth,
                                       wallBorders[0].transform.position.y - halfBorderWidth);
                break;

            case 3:
                randomX = wallBorders[3].transform.position.x;
                randomY = Random.Range(wallBorders[1].transform.position.y + halfBorderWidth,
                                       wallBorders[0].transform.position.y - halfBorderWidth);
                break;
        }

        return new Vector2(randomX, randomY);
    }

    private Quaternion GetHoleRotation(Vector2 holePosition)
    {
        if (holePosition.y == wallBorders[0].transform.position.y || holePosition.y == wallBorders[1].transform.position.y)
        {
            return Quaternion.identity;
        }
        else if (holePosition.x == wallBorders[2].transform.position.x || holePosition.x == wallBorders[3].transform.position.x)
        {
            return Quaternion.Euler(0, 0, 90);
        }

        return Quaternion.identity;
    }
}
