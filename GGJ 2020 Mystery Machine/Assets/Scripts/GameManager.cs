using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour {
  
  public static GameManager Instance;

  public Vector3 playerEntryPosition;

  private Player _player;

  private void Start() {
    if (Instance != null) {
      GameObject.Destroy(Instance.gameObject);
    }
    Instance = this;
    _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    StartCoroutine(EnterGame());
  }

  private IEnumerator EnterGame() {
    yield return new WaitForFixedUpdate();
    ScreenEffects.Instance.FadeOutEffect(0f, 2f, 1f);
    _player.transform.position = playerEntryPosition;
    _player.SetPlayerControls(false);
    yield return new WaitForSeconds(3f);
    string[] entryDialogue = new string[] {
      "Ahh, the old house.",
      "...",
      "A bit creepy, now that the old man's dead.",
      "   ",
      "I liked working for him.",
      "But until a new owner is found, I am supposed to keep watering the plants.",
      "The watering can was in the shack, through the kitchen on the right."
    };
    Dialogue.Instance.SaySomething(entryDialogue);
    yield return new WaitForSeconds(Dialogue.Instance.GetDurationOfSnippets(entryDialogue));
    _player.SetPlayerControls(true);
  }

}