using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class InteractableArea : MonoBehaviour {
  
  private MouseControl mc;
  private void Start() {
    mc = FindObjectOfType<MouseControl>();
  }
  private void OnMouseEnter() {
    mc.SetMouseInteractable();
  }
  private void OnMouseExit() {
    mc.SetMouseNormal();
  }
}