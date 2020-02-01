using UnityEngine;

public class Flower : Collectable {
  
  public override void Collect() {
    base.Collect();
    Inventory.Instance.RemoveItem(requiredItemToCollect);
    /*  todo script what happens 
      - pickup anim
      - remove item from world
      - say something?
    */
    Destroy(gameObject);
  }
}