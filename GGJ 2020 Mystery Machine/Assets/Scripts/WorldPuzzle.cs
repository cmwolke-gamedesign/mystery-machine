using UnityEngine;
public class WorldPuzzle : MonoBehaviour
{
  public InventoryItem requiredItem;

  public string[] inspectDialogue = new string[] {"Looks like this needs something ..."};
  public string[] interactSuccessDialogue = new string[] {"Awesome!", "What now?"};
  public string[] interactFailDialogue = new string[] {"No, that doesn't work."};

  public void LookAt()
  {
    // todo use dialog to give info about object
    Dialogue.Instance.SaySomething(inspectDialogue);
  }
  public void InteractWithItem(InventoryItem i) {
    if (i == requiredItem) {
      Inventory.Instance.RemoveItem(i);
      // success, do thing
    } else {
      // todo use dialog to give info about failed use
    }
  }
}