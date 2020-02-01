using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    public List<InventorySlot> slots;
    public GameObject slotPrefab;

    private void Start()
    {
        if (Instance != null) {
            GameObject.Destroy(Instance.gameObject);
        }
        Instance = this;
    }

    public void AddItem(Item i) {
        InventorySlot newSlot = GameObject.Instantiate(slotPrefab).GetComponent<InventorySlot>();
        newSlot.SetItem(i);
        newSlot.transform.SetParent(transform);
        slots.Add(newSlot);
    }

    public bool RemoveItem(Item i) {
        InventorySlot slot = this.slots.Find(s => s.containedItem == i);
        if (slot != null) {
            GameObject.Destroy(slot.gameObject);
            Player.Instance.PutBackItem();
            return true;
        } else {
            return false;
        }
    }
}