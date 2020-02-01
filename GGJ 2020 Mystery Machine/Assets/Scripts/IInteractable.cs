using UnityEngine;

public interface IInteractable {
  void LookAt();
  
  void Interact(InventoryItem? i = null);

  Transform GetTransform();
}