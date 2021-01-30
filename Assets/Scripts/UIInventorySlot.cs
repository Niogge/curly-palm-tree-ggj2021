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
    public string SlotName { get { return Name.text; } }
    [HideInInspector]
    public bool IsEmpty;
    [HideInInspector]
    public int quantity { get { return int.Parse(Quantity.text); } }

    //details
    public GameObject DetailsPanel;
    public TextMeshProUGUI Description;
    public TMP_InputField NumOfSelectionItemText;

    private void Awake()
    {
        IsEmpty = true;
    }
    public void PopulateSlot(Item item)
    {
        Image.enabled = true;
        Image.sprite = item.UIRepresentation;
        Name.text = item.Name;
        Quantity.text = item.Quantity.ToString();
        Description.text = item.Description;
        NumOfSelectionItemText.Select();
        IsEmpty = false;
    }

    public void ClearSlot()
    {
        Image.sprite = null;
        Image.enabled = false;
        Name.text = "";
        Quantity.text = "";
        Description.text = "";
        NumOfSelectionItemText.Select();
        DetailsPanel.SetActive(false);
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

    public void OpenSlotDetails()
    {
        if (!IsEmpty)
            DetailsPanel.SetActive(!DetailsPanel.activeSelf);

        NumOfSelectionItemText.text = "";
        NumOfSelectionItemText.Select();
    }
    public void DropItemButton()
    {
        GameEventSystem.DropInventoryItem(SlotName, int.Parse(NumOfSelectionItemText.text));
        OpenSlotDetails();
    }
}
