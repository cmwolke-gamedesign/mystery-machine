using System.Collections;
using UnityEngine;

public class Chest : Collectable {
  
  public override void Collect() {
    Inventory.Instance.RemoveItem(requiredItemToCollect);
    StartCoroutine(_Collect());
  }

  // say something, fade out screen, open fridge, close fridge ... 
  private IEnumerator _Collect() {
    // todo player anims?
    Player.Instance.SetPlayerControls(false);
    Dialogue.Instance.SaySomething(new string[] {"I'll open it with the hair pin."});
    Player.Instance.Pickup();
    yield return new WaitForSeconds(1.5f);
    ScreenEffects.Instance.FadeOutEffect(0.5f, 2f, 0.5f);
    yield return new WaitForSeconds(1f);
    GetComponent<SpriteRenderer>().enabled = false;
    yield return new WaitForSeconds(2f);
    Dialogue.Instance.SaySomething(new string[] {
      "Ah, the hairpin broke. But I opened it by force.",
      "There's a beautiful necklace inside. Whose was it?"
    });
    yield return new WaitForSeconds(1f);
    Inventory.Instance.AddItem(item);
    SoundsManager.Instance.PlaySound(collectSound);
    Player.Instance.SetPlayerControls(true);
    gameObject.SetActive(false);
  }
}