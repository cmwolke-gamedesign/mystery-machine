using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public InventoryItem containedItem;
    public Image image;

    public void SetItem(InventoryItem i) {
        Sprite s = Resources.Load<Sprite>("Icons/" + i.ToString());
        image.sprite = s;
        containedItem = i;
    }

    public void HoldItem() {
        Player.Instance.HoldItem(containedItem, image.sprite);
    }

}

public enum InventoryItem {
    Flower, Shovel, Necklace, Hairpin, Candle, LitCandle, Matches, Food, MirrorShards, Stool, Hammer
}