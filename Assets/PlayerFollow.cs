using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
  public GameObject player;

  private Transform cameraTransform;
  private Transform playerTransform;
  // Start is called before the first frame update
  void Start()
  {
    cameraTransform = GetComponent<Transform>();
    playerTransform = player.GetComponent<Transform>();
  }

  // Update is called once per frame
  void Update()
  {
    if (player != null)
    {
      var cameraPosition = playerTransform.position;
      cameraTransform.position = new Vector3(cameraPosition.x, cameraPosition.y, -10);
    }
  }
}
