using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public List<InventorySlot> inventorySlots = new List<InventorySlot>();

    public bool CanItemBeSold(string id)
    {
        return inventorySlots.Exists(slot => slot.item.name == id && slot.count > 0);
    }

    public void AddItemToInventory(InventorySlot item)
    {
        inventorySlots.Add(item);
    }

    public void RemoveItem(InventorySlot slot, int amount = 1)
    {
        if (CanItemBeSold(slot.item.name))
        {
            InventorySlot foundItem = inventorySlots.Find(foundSlot => foundSlot.item.name == slot.item.name);

            Debug.Log(foundItem);
            Debug.Log(foundItem.count);
            if (foundItem.count - amount >= 0)
            {
                foundItem.count -= amount;
            }
        }
    }

    public void AddItem(InventorySlot slot, int amount)
    {
        slot.count = amount;
        Debug.Log("bought"+slot.item.name);
        AddItemToInventory(slot);
    }
}
