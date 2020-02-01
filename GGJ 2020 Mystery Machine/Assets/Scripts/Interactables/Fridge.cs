using System.Collections;
using UnityEngine;

public class Fridge : Collectable {
  
  public override void Collect() {
    Inventory.Instance.RemoveItem(requiredItemToCollect);
    StartCoroutine(_Collect());
  }

  // say something, fade out screen, open fridge, close fridge ... 
  private IEnumerator _Collect() {
    // todo player anims?
    Player.Instance.SetPlayerControls(false);
    Dialogue.Instance.SaySomething(new string[] {"This could take a while."});
    yield return new WaitForSeconds(1.5f);
    ScreenEffects.Instance.FadeOutEffect(0.5f, 2f, 0.5f);
    yield return new WaitForSeconds(1f);
    GetComponent<SpriteRenderer>().enabled = false;
    transform.parent.Find("scn_kitchen_fridge_open").gameObject.SetActive(true);
    yield return new WaitForSeconds(2f);
    Dialogue.Instance.SaySomething(new string[] {"Finally! Wow, that looks tasty."});
    yield return new WaitForSeconds(1f);
    transform.parent.Find("scn_kitchen_fridge_open").gameObject.SetActive(false);
    Inventory.Instance.AddItem(item);
    Player.Instance.SetPlayerControls(true);
    gameObject.SetActive(false);
  }
}