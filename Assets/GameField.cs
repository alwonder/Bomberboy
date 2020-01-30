using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameField : MonoBehaviour
{

    public int fieldWidth = 25;
    public int fieldHeight = 15;
    public int obstaclesQuantity = 20;
    public Tile wallTile;
    public Tile destructibleTile;
    private Vector3Int origin;
    private Tilemap tilemap;
    // Start is called before the first frame update
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        origin = new Vector3Int(-fieldWidth / 2 - 1, -fieldHeight / 2, 0);
        MakeBounds();
        PlaceColons();
        PlaceObstacles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MakeBounds() {
        for (int i = -1; i <= fieldWidth; i++)
        {
            tilemap.SetTile(GetTile(i, -1), wallTile);
            tilemap.SetTile(GetTile(i, fieldHeight), wallTile);
        }
        for (int i = 0; i < fieldHeight; i++)
        {
            tilemap.SetTile(GetTile(-1, i), wallTile);
            tilemap.SetTile(GetTile(fieldWidth, i), wallTile);
        }
    }

    void PlaceColons() {
        for (int i = 1; i < fieldWidth; i += 2)
        {
            for (int j = 1; j < fieldHeight; j += 2)
            {
                tilemap.SetTile(GetTile(i, j), wallTile);
            }
        }
    }

    void PlaceObstacles() {
        List<bool> placePositions = new List<bool>();
        int freeTiles = GetFreeTilesQuantity();
        for (int i = 0; i < freeTiles; i++)
        {
            placePositions.Add(i < obstaclesQuantity);
        }

        int n = placePositions.Count;  
        while (n > 1) {  
            n--;  
            int k = Random.Range(0, n + 1);  
            bool value = placePositions[k];  
            placePositions[k] = placePositions[n];  
            placePositions[n] = value;  
        }

        int placeListIndex = 0;
        for (int i = 0; i < fieldWidth; i++)
        {
            for (int j = 0; j < fieldHeight; j++)
            {
                if (i % 2 == 1 && j % 2 == 1) continue;
                if (i <= 1 && j >= fieldHeight - 2) continue;

                if (placePositions[placeListIndex] == true)
                {
                    tilemap.SetTile(GetTile(i, j), destructibleTile);
                }
                placeListIndex++;
            }
        }
    }

    public Vector3Int GetTile(int x, int y)
    {
        return new Vector3Int(x, y, 0) + origin;
    }

    public Vector3Int GetTile(Vector2Int tile)
    {
        return new Vector3Int(tile.x, tile.y, 0) + origin;
    }

    public Vector3 GetPlayerStartPosition() {
        Vector3Int startTile = GetTile(0, fieldHeight - 1);
        return tilemap.GetCellCenterWorld(startTile);
    }

    private int GetFreeTilesQuantity() {
        int playerReservedTiles = 3;
        return fieldWidth * fieldHeight - ((fieldWidth / 2) + (fieldHeight / 2)) - playerReservedTiles;
    }
}
