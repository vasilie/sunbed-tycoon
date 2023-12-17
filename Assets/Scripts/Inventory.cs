using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public List<InventorySlot> inventorySlots = new List<InventorySlot>();

    public bool HasItem(string id)
    {
        return inventorySlots.Exists(slot => slot.item.name == id);
    }

    public void AddItemToInventory(InventorySlot item)
    {
        inventorySlots.Add(item);
    }

    public InventorySlot SellItem(InventorySlot slot, int amount = 1)
    {
        if (HasItem(slot.item.name))
        {
            InventorySlot foundItem = inventorySlots.Find(foundSlot => foundSlot.item.name == slot.item.name);
            if (foundItem.count - amount >= 0)
            {
                foundItem.count -= amount;
            }
        }
        return slot;
    }

    public void BuyItem(InventorySlot item, int amount)
    {
        item.count = amount;
        inventorySlots.Add(item);
    }
}
