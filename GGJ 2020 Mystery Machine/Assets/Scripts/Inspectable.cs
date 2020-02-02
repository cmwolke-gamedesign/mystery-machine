using UnityEngine;
using UnityEngine.EventSystems;

public class Inspectable : MonoBehaviour, IInteractable {
  
  public string[] inspectDialogue;

  public void Interact(Item i = Item.None)
  {
    this.LookAt();
  }

  public void LookAt()
  {
    Dialogue.Instance.SaySomething(inspectDialogue);
    Player.Instance.PutBackItem();
  }
  public void Click(BaseEventData d) {
    Player.Instance.StartWalkingToInteractable(transform.position.x, this, InteractionType.LookAt);
  }
}