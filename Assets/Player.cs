using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    public Tilemap tilemap;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = tilemap.GetComponent<GameField>().GetPlayerStartPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die() {
        Debug.Log("I'm ded");
        Destroy(gameObject);
    }
}
