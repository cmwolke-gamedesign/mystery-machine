using UnityEngine;

public class GameEvents : MonoBehaviour {
  public delegate void MMEvent();
  public static event MMEvent MMItemAdded;

  public static void OnMMItemAdded() {
    MMItemAdded();
  }
}
