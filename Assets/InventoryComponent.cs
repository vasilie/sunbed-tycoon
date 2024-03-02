using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventoryComponent : MonoBehaviour
{
    public Inventory inventory;
    public List<InventorySlot> keyValueList = new List<InventorySlot>();
    public int currentMoney = 10;
    public bool isPlayerInventory = false;
    public StocksManager stock;

    void Start()
    {
        inventory = new Inventory();
        for (int i = 0; i < keyValueList.Count; i++)
        {
            inventory.AddItemToInventory(keyValueList[i]);
        }
        if (isPlayerInventory) {
            stock.UpdateUI(inventory.inventorySlots);
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
            inventory.AddItem(slot, amount);
            keyValueList = inventory.inventorySlots;
        }
        else
        {
            Debug.Log("No enough money to buy: " + amount + " x " + slot.item.name);
        }
    }

    public void SellItem(InventorySlot slot, int amount = 1)
    {
        inventory.RemoveItem(slot, amount);
        if (isPlayerInventory) {
            stock.UpdateUI(inventory.inventorySlots);
        }
    }

    public bool CheckIfItemCanBeSold(InventorySlot slot) {
        return inventory.CanItemBeSold(slot.item.name);
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
