using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float countdown = 2f;
    public int explosionPower = 1;
    private GameObject player;
    private bool playerLeft = false;
    private Vector3Int cell;

    public void SetSpawnerPlayer(Vector3Int cell, GameObject player)
    {
        this.cell = cell;
        this.player = player;
        Physics2D.IgnoreCollision(player.GetComponent<CircleCollider2D>(), GetComponent<BoxCollider2D>());
    }

    void FixedUpdate() {
        // Ignore player that placed this bomb until he leaves the cell
        if (!playerLeft && !cell.Equals(player.GetComponent<BombSpawner>().GetCell())) {
            playerLeft = true;
            Physics2D.IgnoreCollision(player.GetComponent<CircleCollider2D>(), GetComponent<BoxCollider2D>(), false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f) {
            Explode();
        }
    }

    public void Explode() {
        FindObjectOfType<MapDestroyer>().Explode(gameObject.transform.localPosition, explosionPower);
        Destroy(gameObject);
    }
}
