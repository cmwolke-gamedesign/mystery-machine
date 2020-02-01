using UnityEngine;

public class MouseControl : MonoBehaviour {
  
  public Texture2D interactableCursor;
  public CursorMode cursorMode = CursorMode.Auto;
  public Texture2D standardCursor;

  void Start()
  {
      SetMouseNormal();
  }

  public void SetMouseInteractable() {
        Cursor.SetCursor(interactableCursor, new Vector2(-0.5f, -0.5f), cursorMode);
  }
  public void SetMouseNormal() {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
  }
}