using UnityEngine;

public class Statue : Collectable {
  
  public override void Collect() {
    base.Collect();
    Inventory.Instance.RemoveItem(requiredItemToCollect);
    // todo player anim
    transform.parent.Find("scn_hall_godrays").GetComponent<Animator>().SetTrigger("disable");
    // play anim for this?:
    transform.Find("scn_hall_statue_head").gameObject.SetActive(false);
  }
}
