using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  public float velocity = 3f;
  private Rigidbody2D rb;
  private Animator animator;
  private BombSpawner bombSpawner;
  private Vector2 velocityVector = new Vector2(0, 0);

  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    bombSpawner = GetComponent<BombSpawner>();
  }

  public void SetVelocity(Vector2 movement)
  {
    velocityVector = movement * velocity;
  }

  public void PlaceBomb()
  {
    bombSpawner.Spawn();
  }

  private void Update()
  {
    rb.velocity = velocityVector;
    animator.SetFloat("Speed", rb.velocity.magnitude);

    if (rb.velocity.x > 0.1)
      GetComponent<SpriteRenderer>().flipX = false;
    if (rb.velocity.x < -0.1)
      GetComponent<SpriteRenderer>().flipX = true;
  }
}
