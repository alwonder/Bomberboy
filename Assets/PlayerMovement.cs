using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float velocity = 3f;
    private Rigidbody2D rb;
    private Animator animator;
    private BombSpawner bombSpawner;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bombSpawner = GetComponent<BombSpawner>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(0, 0);
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2 (moveHorizontal, moveVertical);

        rb.velocity = movement * velocity;
        
     }

     private void Update()
     {
        if (Input.GetKeyDown(KeyCode.Space))
            bombSpawner.Spawn();

        animator.SetFloat("Speed", rb.velocity.magnitude);

        if (rb.velocity.x > 0.1)
            GetComponent<SpriteRenderer>().flipX = false;
        if (rb.velocity.x < -0.1)
            GetComponent<SpriteRenderer>().flipX = true;
     }
}
