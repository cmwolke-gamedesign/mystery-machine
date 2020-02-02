using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour {
  public float duration = 5f;

  private void Start() {
    Invoke("BackToMain", duration);
  }

  private void BackToMain() {
    SceneManager.LoadScene("scn_Menu");
  }
}