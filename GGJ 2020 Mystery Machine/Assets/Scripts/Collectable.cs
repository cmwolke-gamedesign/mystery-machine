using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class Collectable : MonoBehaviour, IInteractable{
  
  public Item item;

  public Item requiredItemToCollect = Item.None;

  public string[] collectDialogue = { "I will take this." };
  public string[] inspectDialogue = { "Missing Inspect Dialogue!" };
  public string[] collectFailDialogue = { "I guess I am missing something ..." };
  public virtual void Collect() {
    Inventory.Instance.AddItem(item);
    Dialogue.Instance.SaySomething(collectDialogue);
  }

  public void Interact(Item i = Item.None)
  {
    if (i == requiredItemToCollect) {
      this.Collect();
    } else {
      Dialogue.Instance.SaySomething(collectFailDialogue);
    }
  }

  public void LookAt()
  {
    Dialogue.Instance.SaySomething(inspectDialogue);
  }

  private void InteractionSequence() {
  }

  public Transform GetTransform() {
    return transform;
  }

}