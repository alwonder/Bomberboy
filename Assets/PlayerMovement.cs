using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float velocity = 3f;
    public Rigidbody2D rb;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update() {
        animator.SetFloat("Speed", rb.velocity.magnitude);

        if (rb.velocity.x > 0.1)
            GetComponent<SpriteRenderer>().flipX = false;
        if (rb.velocity.x < -0.1)
            GetComponent<SpriteRenderer>().flipX = true;

        rb.velocity = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.LeftArrow))
            rb.velocity += new Vector2(-velocity, 0);
            // rb.AddForce(Vector3.left);
        if (Input.GetKey(KeyCode.RightArrow))
            rb.velocity += new Vector2(velocity, 0);
            // rb.AddForce(Vector3.right);
        if (Input.GetKey(KeyCode.UpArrow))
            rb.velocity += new Vector2(0, velocity);
            // rb.AddForce(Vector3.up);
        if (Input.GetKey(KeyCode.DownArrow))
            rb.velocity += new Vector2(0, -velocity);
        if (Input.GetKeyDown(KeyCode.Space))
            GetComponent<BombSpawner>().Spawn();
        
     }
}
