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
    public float changeDirectionProbability = 0.3f;
    public float ChangeDirectionRepeatRate = 0.3f;
    public MoveDirection currentMoveDirection;
    public Vector2 currentMoveVector;
    private ObjectSeeker objectSeeker;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        objectSeeker = GetComponent<ObjectSeeker>();
        SetDirection(GetRandomDirection());
    }

    void FixedUpdate()
    {
        rb.velocity = currentMoveVector * velocity;
    }

    public void StartRandomMovement() {
        InvokeRepeating("MaybeChangeDirection", ChangeDirectionRepeatRate, ChangeDirectionRepeatRate);
    }

    public void StopRandomMovement() {
        CancelInvoke("MaybeChangeDirection");
    }

    public void WalkAround() {
        if (objectSeeker.HasObstaclesAhead(currentMoveVector))
        {
            SetDirection(GetRandomDirection());
        }
    }

    private void MaybeChangeDirection() {
        int random = Random.Range(0, 100);
        if (random > changeDirectionProbability * 100) return;

        Vector2 randomDirVector = GetMoveVector(GetRandomDirection());
        
        if (!objectSeeker.HasObstaclesAhead(randomDirVector))
        {
            SetDirection(GetRandomDirection());
        }
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

    public MoveDirection GetOppositeDirection(MoveDirection direction) {
        switch (direction)
        {
            case MoveDirection.Up:
                return MoveDirection.Down;
            case MoveDirection.Down:
                return MoveDirection.Up;
            case MoveDirection.Left:
                return MoveDirection.Right;
            case MoveDirection.Right:
                return MoveDirection.Left;
            default:
                throw new System.Exception();
        }
    }


    private MoveDirection GetRandomDirection() {
        return (MoveDirection)Random.Range(0, 4);
    }
}
