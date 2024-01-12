using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventoryComponent : MonoBehaviour
{
    public Inventory inventory;
    public List<InventorySlot> keyValueList = new List<InventorySlot>();
    public int currentMoney = 10;

    void Start()
    {
        inventory = new Inventory();
        for (int i = 0; i < keyValueList.Count; i++)
        {
            inventory.AddItemToInventory(keyValueList[i]);
        }
    }

    void Update()
    {
        for (int i = 0; i < inventory.inventorySlots.Count; i++)
        {
            // Debug.Log(inventory.inventorySlots[i].item.name);
            // Debug.Log(inventory.inventorySlots[i].count);
        }
    }

    public void BuyItem(InventorySlot slot, int amount = 1)
    {
        if (currentMoney - slot.item.price * amount >= 0)
        {
            currentMoney -= slot.item.price * amount;
            inventory.BuyItem(slot, amount);
        }
        else
        {
            Debug.Log("No enough money to buy: " + amount + " x " + slot.item.name);
        }
    }
    public void AddMoney(int value)
    {
        currentMoney += value;
    }

    public void RemoveMoney(int value)
    {
        if (currentMoney - value > 0)
        {
            currentMoney -= value;
        }
        else
        {
            Debug.Log("No mani, customer");
        }
    }

}
