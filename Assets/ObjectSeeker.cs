using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSeeker : MonoBehaviour
{
    
    public float obstacleRaycastDistance = 0.36f;
    public float bombRaycastDistance = 3f;
    public float bombSafeDistance = 4f;
    private EnemyMovement movement;
    private LayerMask obstacleLayerMask;
    private LayerMask bombLayerMask;
    private Collider2D lastSpottedBomb;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<EnemyMovement>();
        obstacleLayerMask = LayerMask.GetMask("Walls", "Fire");
        bombLayerMask = LayerMask.GetMask("Bombs");
        InvokeRepeating("CheckBombs", 0.2f, 0.2f);
    }

    private void CheckBombs() {
        Collider2D bombAhead = GetBombAhead(movement.currentMoveVector);
        if (bombAhead != null)
        {
            lastSpottedBomb = bombAhead;
        }
        GetComponent<Animator>().SetBool("HasBombAhead", bombAhead != null);
        GetComponent<Animator>().SetBool("IsInSafeDistance", IsInSafeDistance());
    }
    
    public bool HasObstaclesAhead(Vector2 directionVector) {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionVector, obstacleRaycastDistance, obstacleLayerMask);
        return hit.collider != null;
    }

    public Collider2D GetBombAhead(Vector2 directionVector) {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionVector, bombRaycastDistance, bombLayerMask);
        
        return hit.collider;
    }

    public bool IsInSafeDistance() {
        if (lastSpottedBomb == null) return true;
        return Vector2.Distance(transform.position, lastSpottedBomb.transform.position) < bombSafeDistance;
    }

    public void ForgetSpottedBomb() {
        lastSpottedBomb = null;
    }
}
