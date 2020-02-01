using UnityEngine;

public interface IInteractable {
  void LookAt();
  
  void Interact(Item i = Item.None);

  Transform GetTransform();
}