using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int MaxSlots;
    public List<ItemSlot> itemSlots;

    private void Awake()
    {
        itemSlots = new List<ItemSlot>();
        GameEventSystem.AddInventoryItemEvent += AddItem;
    }

    private void AddItem(Item item, int quantity)
    {
        for (int i = 0; i < itemSlots.Count; i++) //search all the slots
        {
            if (itemSlots[i].ItemName.Equals(item.Name)) //if there is a slot of the given item type
            {
                itemSlots[i].Add(quantity); //add the given quantity to this slot
                return;
            }
        }

        //here, the slot of this item type doesn't exist, so create the slot type and add the item quantity in it.
        if(itemSlots.Count < MaxSlots)
        {
            ItemSlot slot = new ItemSlot(item.Name, item.Quantity, item.CanStack, item.MaxQuantity);
        }

        Debug.Log(item.Name + " Added in inventory!");
    }

    private void RemoveItem(Item item, int quantity)
    {
        for (int i = 0; i < itemSlots.Count; i++) //search all the slots
        {
            if (itemSlots[i].ItemName.Equals(item.Name)) //if there is a slot of the given item type
            {
                if (!itemSlots[i].Remove(quantity)) //if after the removal there is no more quantity
                    itemSlots.RemoveAt(i); //remove the slot
            }
        }
    }
}