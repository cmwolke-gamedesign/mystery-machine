using System.Collections;
using UnityEngine;

public class ScreenEffects : MonoBehaviour {
  
  public static ScreenEffects Instance;
  public CanvasGroup blackScreen;

  private void Start() {
    if (Instance != null) {
      GameObject.Destroy(Instance);
    }
    Instance = this;
  }
  public void FadeOutEffect(float fadeOutTime = 0.5f, float outTime = 0.5f, float fadeInTime = 0.5f) {
    StartCoroutine(_FadeOutEffect(fadeOutTime, outTime, fadeInTime));
  }

  private IEnumerator _FadeOutEffect(float fadeOutTime, float outTime, float fadeInTime) {
    float timer = 0f;
    while (timer < fadeOutTime) {
      timer += Time.deltaTime;
      blackScreen.alpha = timer/fadeOutTime;
      yield return null;
    }
    blackScreen.alpha = 1;
    yield return new WaitForSeconds(outTime);
    timer = 0;
    while (timer < fadeInTime) {
      timer += Time.deltaTime;
      blackScreen.alpha = 1- timer/fadeInTime;
      yield return null;
    }
    blackScreen.alpha = 0;
  }
}