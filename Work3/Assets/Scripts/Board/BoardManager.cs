using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public float boardWidth = 20f;
    public float boardHeight = 20f;
    public float tileSize = 1f;

    void Start()
    {
        CreateGrid(boardWidth, boardHeight, tileSize);
    }

    void CreateGrid(float boardWidth, float boardHeight, float tileSize)
    {
        int numRows = Mathf.FloorToInt(boardHeight / tileSize);
        int numCols = Mathf.FloorToInt(boardWidth / tileSize);

        for (int row = 0; row < numRows; row++)
        {
            for (int col = 0; col < numCols; col++)
            {
                Vector2 position = new Vector2(col * tileSize - boardWidth / 2, row * tileSize - boardHeight / 2);
                CreateTile(position, tileSize);
            }
        }
    }

    void CreateTile(Vector2 position, float size)
    {
        GameObject tile = new GameObject("Tile");
        tile.transform.position = position;

        BoxCollider2D collider = tile.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(size, size);
        collider.isTrigger = true;

        SpriteRenderer renderer = tile.AddComponent<SpriteRenderer>();
        renderer.color = Color.gray;
    }
}
