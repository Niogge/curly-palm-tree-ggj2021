using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIInventorySlot : MonoBehaviour
{
    public Image Image;
    public TextMeshProUGUI Name, Quantity;

    [HideInInspector]
    public string SlotName { get { return Name.text; }}
    [HideInInspector]
    public bool IsEmpty;
    [HideInInspector]
    public int quantity { get { return int.Parse(Quantity.text);}}

    private void Awake()
    {
        IsEmpty = true;
    }

    public void PopulateSlot(Item item)
    {
        Image.sprite = item.UIRepresentation;
        Name.text = item.Name;
        Quantity.text = item.Quantity.ToString();
        IsEmpty = false;
    }

    public void ClearSlot()
    {
        Image.sprite = null;
        Name.text = "";
        Quantity.text = "";
        IsEmpty = true;
    }

    public void SetQuantity(int quantity)
    {
        Quantity.text = quantity.ToString();
    }

    public void ChangeQuantity(int quantity)
    {
        int numOfItem = this.quantity + quantity;
        Quantity.text = numOfItem.ToString();
    }
}
