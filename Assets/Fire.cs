using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float countdown = 0.3f;

    // Update is called once per frame
    void Update()
    {
        Damage();

        countdown -= Time.deltaTime;
        if (countdown <= 0f) {
            Destroy(gameObject);
        }
    }

    public void Damage() {
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        Vector3 pointA = boxCollider.bounds.min + new Vector3(0.1f, 0.1f);
        Vector3 pointB = boxCollider.bounds.max - new Vector3(0.1f, 0.1f);
        Collider2D[] overlap = Physics2D.OverlapAreaAll(pointA, pointB);

        foreach (Collider2D item in overlap)
        {
            if (item.gameObject.tag == "Bomb") {
                item.GetComponent<Bomb>().Explode();
            }
            if (item.gameObject.tag == "Player") {
                item.GetComponent<Player>().Die();
            }
            if (item.gameObject.tag == "Enemy") {
                item.GetComponent<Enemy>().Die();
            }
        }
    }
}
