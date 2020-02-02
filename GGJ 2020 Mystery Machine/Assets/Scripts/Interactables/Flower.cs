using UnityEngine;

public class Flower : Collectable {
  
  public override void Collect() {
    base.Collect();
    Inventory.Instance.RemoveItem(requiredItemToCollect);
    Player.Instance.Pickup();
    Destroy(gameObject);
  }
}