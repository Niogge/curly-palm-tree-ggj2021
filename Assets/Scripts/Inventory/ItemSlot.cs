using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ItemSlot
{
    public string ItemName;
    public int Quantity;
    public bool CanStack;
    public int MaxStack;

    public ItemSlot(string itemName, int quantity, bool canStack, int maxStack)
    {
        ItemName = itemName;
        Quantity = quantity;
        CanStack = canStack;
        MaxStack = maxStack;
    }

    /// <summary>
    /// Adds the given quantity to the stack, if this slot can stack.
    /// </summary>
    /// <param name="quantity"></param>
    public void Add(int quantity)
    {
        if(CanStack && Quantity < MaxStack)
        {
            Quantity += quantity;
            if (Quantity > MaxStack)
                Quantity = MaxStack;
        }
    }

    /// <summary>
    /// Returns true if there is still quantity available of this object after removal.
    /// Returns false if quantity is 0.
    /// </summary>
    /// <param name="quantity"></param>
    /// <returns></returns>
    public bool Remove(int quantity)
    {
        Quantity -= quantity;
        return quantity > 0;
    }
}