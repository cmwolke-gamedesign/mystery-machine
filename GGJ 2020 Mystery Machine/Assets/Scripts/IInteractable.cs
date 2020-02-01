using UnityEngine;

public interface IInteractable {
  void LookAt();
  
  void InteractWithItem(InventoryItem i);
}