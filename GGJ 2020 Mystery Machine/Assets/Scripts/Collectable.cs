using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(EventTrigger))]
public class Collectable : MonoBehaviour, IInteractable{
  
  public Item item;

  public Item requiredItemToCollect = Item.None;

  public string[] collectDialogue = { "I will take this." };
  public string[] inspectDialogue = { "Missing Inspect Dialogue!" };
  public string[] collectFailDialogue = { "I guess I am missing something ..." };
  public string[] collectWrongItemDialogue = { "That's not going to work." };
  public virtual void Collect() {
    Inventory.Instance.AddItem(item);
    Dialogue.Instance.SaySomething(collectDialogue);
  }

  public void Interact(Item i = Item.None)
  {
    if (i == requiredItemToCollect) {
      this.Collect();
    } else {
      if (i == Item.None) {
        Dialogue.Instance.SaySomething(collectFailDialogue);
      } else {
        Dialogue.Instance.SaySomething(collectWrongItemDialogue);
      }
    }
    Player.Instance.PutBackItem();
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

  public void Click(BaseEventData d) {
    PointerEventData ped = (PointerEventData)d;
    bool leftClick = ped.pointerId == -1;
    bool rightClick = ped.pointerId == -2;
    if (leftClick) Player.Instance.StartWalkingToInteractable(transform.position.x, this, InteractionType.InteractWith);
    else if (rightClick) Player.Instance.StartWalkingToInteractable(transform.position.x, this, InteractionType.LookAt);
  }

}