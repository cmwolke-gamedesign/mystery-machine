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

    public void AddItem(InventoryItem i) {
        InventorySlot newSlot = GameObject.Instantiate(slotPrefab).GetComponent<InventorySlot>();
        newSlot.SetItem(i);
        newSlot.transform.SetParent(transform);
    }

    public bool RemoveItem(InventoryItem i) {
        InventorySlot slot = this.slots.Find(s => s.containedItem == i);
        if (slot != null) {
            GameObject.Destroy(slot.gameObject);
            return true;
        } else {
            return false;
        }
    }
}