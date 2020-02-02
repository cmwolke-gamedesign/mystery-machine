using UnityEngine;

public class BaseCollectable : Collectable {
  
  public override void Collect() {
    base.Collect();
    Player.Instance.Pickup();
    Inventory.Instance.RemoveItem(requiredItemToCollect);
    SoundsManager.Instance.PlaySound(collectSound);
    Destroy(gameObject);
  }
}