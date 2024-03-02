using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    public int count;
    public InventoryObject item; // Assuming InventoryObject is already a class or another struct

    // Constructor to initialize a new instance of InventorySlot
    public InventorySlot(InventoryObject item, int count)
    {
        this.item = item;
        this.count = count;
    }
}