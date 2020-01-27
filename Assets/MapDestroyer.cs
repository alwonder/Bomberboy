using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapDestroyer : MonoBehaviour
{   
    public Tilemap tilemap;
    public Tile wallTile;
    public Tile destructibleTile;
    public GameObject explosion;

    public void Explode(Vector2 worldPos, int explosionPower)
    {
        Vector3Int originCell = tilemap.WorldToCell(worldPos);

        ExplodeCell(originCell);
        ExplodeInDirection(originCell, new Vector3Int(1, 0, 0), explosionPower);
        ExplodeInDirection(originCell, new Vector3Int(-1, 0, 0), explosionPower);
        ExplodeInDirection(originCell, new Vector3Int(0, 1, 0), explosionPower);
        ExplodeInDirection(originCell, new Vector3Int(0, -1, 0), explosionPower);
    }

    private void ExplodeInDirection(Vector3Int cell, Vector3Int delta, int power)
    {
        if (power <= 0) return;

        Vector3Int newCell = cell + delta;
        if (ExplodeCell(newCell))
        {
            ExplodeInDirection(newCell, delta, power - 1);
        }
    }

    bool ExplodeCell(Vector3Int cell)
    {
        Tile tile = tilemap.GetTile<Tile>(cell);

        if (tile == wallTile) return false;

        Collider2D[] overlap = Physics2D.OverlapCircleAll(tilemap.GetCellCenterWorld(cell), 0.1f);
        foreach (Collider2D item in overlap)
        {
            if (item.gameObject.tag == "Fire") {
                return false;
            }
        }

        Vector3 explosionPos = tilemap.GetCellCenterWorld(cell);
        GameObject fire = Instantiate(explosion, explosionPos, Quaternion.identity);
        fire.GetComponent<Fire>().Damage();

        
        if (tile == destructibleTile)
        {
            tilemap.SetTile(cell, null);
            return false;
        }

        return true;
    }
}
