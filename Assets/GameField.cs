using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public struct EnemySpawn {
    public GameObject prefab;
    public int count;
}

public class GameField : MonoBehaviour
{

    public int fieldWidth = 25;
    public int fieldHeight = 15;
    public int obstaclesQuantity = 20;
    public Tile wallTile;
    public Tile destructibleTile;
    public EnemySpawn[] enemies;
    private Vector3Int origin;
    private Tilemap tilemap;
    private byte[] occupiedCells;

    // Start is called before the first frame update
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        occupiedCells = new byte[fieldWidth * fieldHeight];
        origin = new Vector3Int(-fieldWidth / 2 - 1, -fieldHeight / 2, 0);
        MakeBounds();
        PlaceColumns();
        PlaceObstacles(); 
        PlaceEnemies();

        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
        player.transform.position = GetPlayerStartPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MakeBounds() {
        for (int i = -1; i <= fieldWidth; i++)
        {
            tilemap.SetTile(GetCell(i, -1), wallTile);
            tilemap.SetTile(GetCell(i, fieldHeight), wallTile);
        }
        for (int i = 0; i < fieldHeight; i++)
        {
            tilemap.SetTile(GetCell(-1, i), wallTile);
            tilemap.SetTile(GetCell(fieldWidth, i), wallTile);
        }
    }

    void PlaceColumns() {
        for (int i = 1; i < fieldWidth; i += 2)
        {
            for (int j = 1; j < fieldHeight; j += 2)
            {
                SetTile(GetCell(i, j), wallTile);
            }
        }
    }

    void PlaceObstacles() {
        List<bool> placePositions = new List<bool>();
        int freeTiles = GetFreeCellsQuantity();
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
                    SetTile(GetCell(i, j), destructibleTile);
                }
                placeListIndex++;
            }
        }
    }

    void PlaceEnemies() {
        List<int> freeCells = GetFreeCellIndexes();
        foreach (EnemySpawn enemy in enemies)
        {
            for (int i = 0; i < enemy.count; i++)
            {
                int randomIndex = freeCells[Random.Range(0, freeCells.Count)];
                Vector3Int randomCell = GetCellByAbsIndex(randomIndex);
                Instantiate(enemy.prefab, tilemap.GetCellCenterWorld(randomCell), Quaternion.identity);
            }
        }
    }

    public void SetTile(Vector3Int tile, TileBase tileBase) {
        tilemap.SetTile(tile, tileBase);
        occupiedCells[GetAbsIndexByCell(tile)] = 1;
    }

    public Vector3Int GetCell(int x, int y)
    {
        return new Vector3Int(x, y, 0) + origin;
    }

    public Vector3Int GetTile(Vector2Int tile)
    {
        return new Vector3Int(tile.x, tile.y, 0) + origin;
    }

    public Vector3 GetPlayerStartPosition() {
        Vector3Int startTile = GetCell(0, fieldHeight - 1);
        return tilemap.GetCellCenterWorld(startTile);
    }

    private int GetFreeCellsQuantity() {
        int playerReservedCells = 3;
        return fieldWidth * fieldHeight - ((fieldWidth / 2) + (fieldHeight / 2)) - playerReservedCells;
    }
    
    private List<int> GetFreeCellIndexes() {
        List<int> freeCellIndexes = new List<int>();
        for (int i = 0; i < occupiedCells.Length; i++)
        {   
            if (occupiedCells[i] == 0) {
                freeCellIndexes.Add(i);
            }
        }
        return freeCellIndexes;
    }

    private Vector3Int GetCellByAbsIndex(int index) {
        return GetCell(index % fieldWidth, index / fieldWidth);
    }

    private int GetAbsIndexByCell(Vector3Int cell) {
        Vector3Int normalizedCell = cell - origin;
        return normalizedCell.x + normalizedCell.y * fieldWidth;
    }
}
