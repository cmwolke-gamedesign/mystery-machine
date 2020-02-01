using UnityEngine;

public class Shovel : Collectable {
  
  public override void Collect() {
    base.Collect();
    /*  todo script what happens 
      - pickup anim
      - remove item from world
      - say something?
    */
    Destroy(gameObject);
  }
}