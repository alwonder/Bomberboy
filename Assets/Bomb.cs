using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float countdown = 2f;
    public int explosionPower = 1;

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f) {
            FindObjectOfType<MapDestroyer>().Explode(gameObject.transform.localPosition, explosionPower);
            Destroy(gameObject);
        }
    }
}
