using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public InventoryItem containedItem;
    public Image image;

    public void SetItem(InventoryItem i) {
        // image.sprite = 
        containedItem = i;
    }
}

public enum InventoryItem {
    Flower, Shovel, Necklace, Hairpin, Candle, LitCandle, Matches, Food, MirrorShards, Stool, Hammer
}
