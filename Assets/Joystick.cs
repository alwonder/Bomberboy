using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{
  private GameObject player;
  private PlayerMovement playerMovement;
  private bool touchStarted = false;
  private float lastTouchTime;
  private Vector2 pointA;
  private Vector2 pointB;

  private static readonly float TAP_MAX_DISTANCE = 0.2f;
  private static readonly float TAP_RELEASE_TIME = 0.1f;
  private static readonly float STICK_THRESHOLD = 1;
  private static readonly float DISTANCE_COEFFICIENT = 5;

  // Start is called before the first frame update
  void Start()
  {
    player = GameObject.Find("Player");
    playerMovement = player.GetComponent<PlayerMovement>();
  }

  void Update()
  {
    if (player == null) return;

    if (!touchStarted && Input.GetMouseButtonDown(0))
    {
      HandleMouseDown();
    }
    else if (touchStarted)
    {
      pointB = GetMousePosition();

      if (!Input.GetMouseButton(0)) HandleMouseUp();
      else playerMovement.SetVelocity(GetDeltaVector());
    }
  }

  private void HandleMouseDown()
  {
    pointA = GetMousePosition();
    touchStarted = true;
    lastTouchTime = Time.time;
  }

  private void HandleMouseUp()
  {
    touchStarted = false;
    if (IsTapped()) playerMovement.PlaceBomb();
    playerMovement.SetVelocity(new Vector2(0, 0));
  }

  private bool IsTapped()
  {
    bool isTapDistance = Vector2.Distance(pointA, pointB) < TAP_MAX_DISTANCE;
    bool isTapDuration = Time.time - lastTouchTime < TAP_RELEASE_TIME;
    return isTapDistance && isTapDuration;
  }

  private Vector2 GetMousePosition()
  {
    return Camera.main.ScreenToViewportPoint(new Vector3(
      Input.mousePosition.x,
      Input.mousePosition.y,
      Camera.main.transform.position.z
    ));
  }

  private Vector2 GetDeltaVector()
  {
    float deltaX = Mathf.Clamp((pointB.x - pointA.x) * DISTANCE_COEFFICIENT, -STICK_THRESHOLD, STICK_THRESHOLD);
    float deltaY = Mathf.Clamp((pointB.y - pointA.y) * DISTANCE_COEFFICIENT, -STICK_THRESHOLD, STICK_THRESHOLD);
    return new Vector2(deltaX, deltaY);
  }
}
