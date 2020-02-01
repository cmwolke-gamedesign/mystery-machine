using UnityEngine;
public class WorldPuzzle : MonoBehaviour, IInteractable
{
  public InventoryItem requiredItem;

  public void LookAt()
  {
    // todo use dialog to give info about object
  }
  public void InteractWithItem(InventoryItem i) {
    if (i == requiredItem) {
      // success, do thing
    } else {
      // todo use dialog to give info about failed use
    }
  }
}