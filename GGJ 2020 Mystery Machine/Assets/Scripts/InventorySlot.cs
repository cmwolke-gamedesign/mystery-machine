using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Item containedItem;
    public Image image;

    public void SetItem(Item i) {
        Sprite s = Resources.Load<Sprite>("Icons/" + i.ToString());
        image.sprite = s;
        containedItem = i;
    }

    public void HoldItem() {
        Player.Instance.HoldItem(containedItem, image.sprite);
    }

}

public enum Item {
    None, Flower, Shovel, Necklace, Hairpin, Candle, LitCandle, Matches, Food, MirrorShards, Stool, Hammer
}