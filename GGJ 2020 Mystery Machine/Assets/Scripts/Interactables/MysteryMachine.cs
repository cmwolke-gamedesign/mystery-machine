using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MysteryMachine : MonoBehaviour, IInteractable
{
  [SerializeField]
  private List<Item> requiredItems = new List<Item> {Item.Head, Item.Flower, Item.Cake, Item.Necklace, Item.Shards};
  private List<Item> addedItems = new List<Item>();
  public GameObject headSlot, flowerSlot, cakeSlot, necklaceSlot, mirrorShardsSlot;
  private Dictionary<Item, GameObject> itemSlots;
  public string[] lookAtDialogue;
  private int lookAtDialogueIdx = 0;

  private Animator _anim;

  public void Start() 
  {
    itemSlots = new Dictionary<Item, GameObject> 
    {
      {Item.Head, headSlot},
      {Item.Flower, flowerSlot},
      {Item.Cake, cakeSlot},
      {Item.Necklace, necklaceSlot},
      {Item.Shards, mirrorShardsSlot},
    };
    foreach(GameObject go in itemSlots.Values) {
      go.SetActive(false);
    }
    _anim = GetComponent<Animator>();
  }
  public void Interact(Item i = Item.None)
  {
    if (requiredItems.Contains(i)) {
      Inventory.Instance.RemoveItem(i);
      addedItems.Add(i);
      // todo play put animation / activate machine?
      itemSlots[i].SetActive(true);
      GameEvents.OnMMItemAdded();
    }
    if (requiredItems.Count == addedItems.Count) {
      MachineAssembled();
    }
  }

  public void LookAt()
  {
    Dialogue.Instance.SaySomething(lookAtDialogue);
  }

  private void MachineAssembled() 
  {
    // BOOM
  }
  
  public void Click(BaseEventData d) {
    PointerEventData ped = (PointerEventData)d;
    bool leftClick = ped.pointerId == -1;
    bool rightClick = ped.pointerId == -2;
    if (leftClick) Player.Instance.StartWalkingToInteractable(transform.position.x, this, InteractionType.InteractWith);
    else if (rightClick) Player.Instance.StartWalkingToInteractable(transform.position.x, this, InteractionType.LookAt);
  }
}