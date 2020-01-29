using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public bool invincible = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        if (invincible)
        {
            Debug.Log("Haha you can't beat me");
        } else
        {
            Debug.Log("I'm ded");
            Destroy(gameObject);
        }
    }
}
