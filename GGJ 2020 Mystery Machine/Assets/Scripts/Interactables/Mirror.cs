using System.Collections;
using UnityEngine;

public class Mirror : Collectable {
  
  public override void Collect() {
    Inventory.Instance.RemoveItem(requiredItemToCollect);
    StartCoroutine(_Collect());
  }

  // say something, fade out screen, open fridge, close fridge ... 
  private IEnumerator _Collect() {
    // todo player anims?
    Player.Instance.SetPlayerControls(false);
    Dialogue.Instance.SaySomething(new string[] {"I can reach it with the stool."});
    yield return new WaitForSeconds(1.5f);
    ScreenEffects.Instance.FadeOutEffect(0.5f, 2f, 0.5f);
    yield return new WaitForSeconds(1f);
    GetComponent<SpriteRenderer>().enabled = false;
    SoundsManager.Instance.PlaySound(collectSound);
    transform.parent.Find("scn_bedroom_mirror_broken").gameObject.SetActive(true);
    yield return new WaitForSeconds(2f);
    Dialogue.Instance.SaySomething(new string[] {
      "I am so clumsy ... hope it's not bad luck.",
      "I'll take a shard of it.",
    });
    yield return new WaitForSeconds(1f);
    Inventory.Instance.AddItem(item);
    SoundsManager.Instance.PlaySound(collectSound);
    Player.Instance.SetPlayerControls(true);
    gameObject.SetActive(false);
  }
}