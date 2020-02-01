using System.Collections;
using UnityEngine;

public class Fridge : Collectable {
  
  public override void Collect() {
    Inventory.Instance.RemoveItem(requiredItemToCollect);
    StartCoroutine(_Collect());
  }

  private IEnumerator _Collect() {
    // todo player anims?
    Dialogue.Instance.SaySomething(new string[] {"This could take a while."});
    yield return new WaitForSeconds(1.5f);
    ScreenEffects.Instance.FadeOutEffect(0.5f, 2f, 0.5f);
    yield return new WaitForSeconds(1f);
    GetComponent<SpriteRenderer>().enabled = false;
    transform.parent.Find("scn_kitchen_fridge_open").gameObject.SetActive(true);
    yield return new WaitForSeconds(2f);
    Dialogue.Instance.SaySomething(new string[] {"Finally! Wow, that looks tasty."});
    Inventory.Instance.AddItem(item);
    gameObject.SetActive(false);
  }
}