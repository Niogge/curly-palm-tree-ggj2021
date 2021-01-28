using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    public GameObject SlotPrefab;

    private List<UIInventorySlot> UIInventorySlots;

    private void Awake()
    {
        GameEventSystem.ChangeInventoryMaxSlotsEvent += ChangeMaxSlots;
        GameEventSystem.AddInventorySlotEvent += AddNewSlot;
        GameEventSystem.RemoveInventorySlotEvent += RemoveSlot;
        GameEventSystem.ChangeInventorySlotQuantityEvent += ChangeQuantity;

        UIInventorySlots = new List<UIInventorySlot>();
    }
    

    public void ChangeMaxSlots(int slots)
    {
        int slotsToAdd = slots - transform.childCount;
        for (int i = 0; i < slotsToAdd; i++)
        {
            GameObject go = Instantiate(SlotPrefab, transform);
            UIInventorySlots.Add(go.GetComponent<UIInventorySlot>());
        }
    }

    public void AddNewSlot(Item item)
    {
        for (int i = 0; i < UIInventorySlots.Count; i++)
        {
            if (UIInventorySlots[i].IsEmpty)
            {
                UIInventorySlots[i].PopulateSlot(item);
                break;
            }
        }
    }

    public void ChangeQuantity(Item item, int quantity)
    {
        for (int i = 0; i < UIInventorySlots.Count; i++)
        {
            if (item.Name.Equals(UIInventorySlots[i].SlotName))
            {
                UIInventorySlots[i].SetQuantity(quantity);
            }
        }
    }
    public void RemoveSlot(Item item)
    {
        for (int i = 0; i < UIInventorySlots.Count; i++)
        {
            if(item.Name.Equals(UIInventorySlots[i].SlotName))
            {
                UIInventorySlots[i].ClearSlot();
                UIInventorySlots[i].transform.SetAsLastSibling();
                break;
            }
        }
    }
}
