using System.Collections.Generic;
using UnityEngine;

public class MysteryMachine : MonoBehaviour, IInteractable
{
  [SerializeField]
  private List<Item> requiredItems = new List<Item> {Item.Head, Item.Flower, Item.Cake, Item.Necklace, Item.MirrorShards};
  private List<Item> addedItems = new List<Item>();
  public GameObject headSlot, flowerSlot, cakeslot, necklaceSlot, mirrorShardsSlot;
  private Dictionary<Item, GameObject> itemSlots;
  public List<string[]> lookAtDialogue;
  private int lookAtDialogueIdx = 0;

  private Animator _anim;

  public void Start() 
  {
    itemSlots = new Dictionary<Item, GameObject> 
    {
      {Item.Head, headSlot},
      {Item.Flower, flowerSlot},
      {Item.Cake, cakeslot},
      {Item.Necklace, necklaceSlot},
      {Item.MirrorShards, mirrorShardsSlot},
    };
    foreach(GameObject go in itemSlots.Values) {
      go.SetActive(false);
    }
    _anim = GetComponent<Animator>();
  }
  public Transform GetTransform()
  {
     return transform;
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
    Dialogue.Instance.SaySomething(lookAtDialogue[lookAtDialogueIdx]);
    if (lookAtDialogueIdx != lookAtDialogue.Count - 1) {
      lookAtDialogueIdx++;
    }
  }

  private void MachineAssembled() 
  {
    // BOOM
  }
}