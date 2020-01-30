using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameField : MonoBehaviour
{

    public int fieldWidth = 25;
    public int fieldHeight = 15;
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

    public Vector3Int GetTile(int x, int y)
    {
        return new Vector3Int(x, y, 0) + origin;
    }

    public Vector3Int GetTile(Vector2Int tile)
    {
        return new Vector3Int(tile.x, tile.y, 0) + origin;
    }
}
