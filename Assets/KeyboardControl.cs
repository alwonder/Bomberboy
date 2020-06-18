using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardControl : MonoBehaviour
{
  private PlayerMovement playerMovement;

  // Start is called before the first frame update
  void Start()
  {
    playerMovement = GetComponent<PlayerMovement>();
  }

  // Update is called once per frame
  void Update()
  {
    playerMovement.SetVelocity(GetMoveVector());

    if (Input.GetKeyDown(KeyCode.Space)) playerMovement.PlaceBomb();
  }

  private Vector2 GetMoveVector()
  {
    float moveHorizontal = Input.GetAxisRaw("Horizontal");
    float moveVertical = Input.GetAxisRaw("Vertical");
    return new Vector2(moveHorizontal, moveVertical);
  }
}
