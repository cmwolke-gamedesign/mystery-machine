using UnityEngine; 

[RequireComponent(typeof(Collider2D))]
public class Collectable : MonoBehaviour, IInteractable {
  
  public InventoryItem item;

  public InventoryItem? requiredItemToCollect;

  public string[] collectDialogue;
  public string[] inspectDialogue;
  public void Collect() {
    Inventory.Instance.AddItem(item);
    Dialogue.Instance.SaySomething(collectDialogue);
  }

  public void Interact(InventoryItem? i = null)
  {
    if (i == requiredItemToCollect) {
      this.Collect();
    }
  }

  public void LookAt()
  {
    Dialogue.Instance.SaySomething(inspectDialogue);
  }

  private void InteractionSequence() {
    /*  todo script what happens 
      - pickup anim
      - remove item from world
      - say something?
    */
  }

  public Transform GetTransform() {
    return transform;
  }
}