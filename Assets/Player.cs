using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
  public Tilemap tilemap;
  private GameObject sceneHelper;
  // Start is called before the first frame update
  void Start()
  {
    sceneHelper = GameObject.Find("SceneManagerHelper");
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void Die()
  {
    Debug.Log("I'm ded");
    sceneHelper.GetComponent<SceneManagerHelper>().Restart();
    Destroy(gameObject);
  }
}
