using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveDirection
{
    Up,
    Down,
    Left,
    Right,
}

public class EnemyMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float velocity = 2f;
    public float raycastDistance = 0.36f;
    public float changeDirectionProbability = 0.3f;
    public float ChangeDirectionRepeatRate = 0.3f;
    public MoveDirection currentMoveDirection;
    private Vector2 currentMoveVector;
    private bool walkingAround = true;
    private LayerMask raycastMask;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        raycastMask = LayerMask.GetMask("Walls", "Fire", "Bombs");
        SetDirection(GetRandomDirection());
        InvokeRepeating("MaybeChangeDirection", ChangeDirectionRepeatRate, ChangeDirectionRepeatRate);
    }


    // Update is called once per frame
    void Update()
    {
        WalkAround();
    }

    void WalkAround() {
        if (hasObstaclesAhead(currentMoveVector))
        {
            SetDirection(GetRandomDirection());
        }
        
        rb.velocity = currentMoveVector * velocity;
    }

    void FixedUpdate() {
        
    }

    private void MaybeChangeDirection() {
        int random = Random.Range(0, 100);
        if (random > changeDirectionProbability * 100) return;

        Vector2 randomDirVector = GetMoveVector(GetRandomDirection());
        
        if (!hasObstaclesAhead(randomDirVector))
        {
            SetDirection(GetRandomDirection());
        }
    }

    private bool hasObstaclesAhead(Vector2 directionVector) {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionVector, raycastDistance, raycastMask);
        return hit.collider != null;
    }

    public void SetDirection(MoveDirection direction) {
        currentMoveDirection = direction;
        currentMoveVector = GetMoveVector(direction);
    } 

    private Vector2 GetMoveVector(MoveDirection direction) {
        switch (direction)
        {
            case MoveDirection.Up:
                return new Vector2(0, 1);
            case MoveDirection.Down:
                return new Vector2(0, -1);
            case MoveDirection.Left:
                return new Vector2(-1, 0);
            case MoveDirection.Right:
                return new Vector2(1, 0);
            default:
                throw new System.Exception();
        }
    }

    private MoveDirection GetRandomDirection() {
        return (MoveDirection)Random.Range(0, 4);
    }
}
