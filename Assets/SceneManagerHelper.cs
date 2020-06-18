using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerHelper : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public void Restart()
  {
    StartCoroutine(RestartScene());
  }

  private IEnumerator RestartScene(float delayBeforeRestart = 2)
  {
    yield return new WaitForSeconds(delayBeforeRestart);
    SceneManager.LoadScene("SampleScene");
  }
}
