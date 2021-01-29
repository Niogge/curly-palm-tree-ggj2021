using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static bool SpaceAvailable { get { return itemSlots.Count < MaxSlots; } }
    public static int MaxSlots;
    public static List<ItemSlot> itemSlots;

    public RecipeSO[] RecipesSO; //<- set in inspector and don't use it in game.
    [HideInInspector] public Recipe[] Recipes;

    private void Awake()
    {
        itemSlots = new List<ItemSlot>();

        Recipes = new Recipe[RecipesSO.Length];
        for (int i = 0; i < RecipesSO.Length; i++)
        {
            Recipes[i] = new Recipe(RecipesSO[i]);
        }

        GameEventSystem.AddInventoryItemEvent += AddItem;
        GameEventSystem.TryGiveMaterialToBuildEvent += GiveMaterial;
        GameEventSystem.AcquireCraftingRecipeEvent += UnlockRecipe;
        GameEventSystem.CraftItemEvent += CraftItem;
    }

    private void Start()
    {
        GameEventSystem.ChangeInventoryMaxSlots(MaxSlots);
        GameEventSystem.LoadAllRecipes(ref Recipes);
    }

    private void UnlockRecipe(string recipeName)
    {
        for (int i = 0; i < Recipes.Length; i++)
        {
            if(Recipes[i].RecipeName.Equals(recipeName))
            {
                Recipes[i].Acquired = true;
                //evento per popuppino che dice "Hai imparato sto caxxo!"
            }
        }
    }

    private void GiveMaterial(string itemName, int quantity)
    {
        for (int i = 0; i < itemSlots.Count; i++) //search all the slots
        {
            if (itemSlots[i].ItemName.Equals(itemName)) //if there is a slot of the given item type
            {
                int removedQuantity = itemSlots[i].Remove(quantity);
                //if after the removal there is no more quantity
                if (itemSlots[i].Quantity <= 0)
                {
                    itemSlots[i].Clear();
                    itemSlots.RemoveAt(i); //remove the slot
                    GameEventSystem.RemoveInventorySlot(itemName);
                    //aggiornare anche la UI dell'inventario
                }

                GameEventSystem.GiveMaterial(removedQuantity);
            }
        }
    }

    private void AddItem(Item item, int quantity)
    {
        for (int i = 0; i < itemSlots.Count; i++) //search all the slots
        {
            if (itemSlots[i].ItemName.Equals(item.Name)) //if there is a slot of the given item type
            {
                if (itemSlots[i].Add(quantity))//add the given quantity to this slot
                    GameEventSystem.ChangeInventorySlotQuantity(item.Name, itemSlots[i].Quantity);

                return;
            }
        }

        //here, the slot of this item type doesn't exist, so create the slot type and add the item quantity in it.
        if (SpaceAvailable)
        {
            ItemSlot slot = new ItemSlot(item.Name, item.Quantity, item.CanStack, item.MaxQuantity);
            itemSlots.Add(slot);
            GameEventSystem.AddInventorySlot(item);
        }
    }

    private void RemoveItem(string itemName, int quantity)
    {
        for (int i = 0; i < itemSlots.Count; i++) //search all the slots
        {
            if (itemSlots[i].ItemName.Equals(itemName)) //if there is a slot of the given item type
            {
                itemSlots[i].Remove(quantity);
                //if after the removal there is no more quantity
                if (itemSlots[i].Quantity <= 0)
                {
                    itemSlots[i].Clear();
                    itemSlots.RemoveAt(i); //remove the slot
                    GameEventSystem.RemoveInventorySlot(itemName);
                    //aggiornare anche la UI
                }
            }
        }
    }

    private void CraftItem(string itemName, int quantity)
    {
        for (int i = 0; i < itemSlots.Count; i++) //search all the slots
        {
            if (itemSlots[i].ItemName.Equals(itemName)) //if there is a slot of the given item type
            {
                itemSlots[i].Remove(quantity);
                //if after the removal there is no more quantity
                if (itemSlots[i].Quantity <= 0)
                {
                    itemSlots[i].Clear();
                    itemSlots.RemoveAt(i); //remove the slot
                    GameEventSystem.RemoveInventorySlot(itemName);
                    //aggiornare anche la UI
                }
                break;
            }
        }
    }

    public static ItemSlot FindSlot(string name)
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (itemSlots[i].ItemName.Equals(name))
                return itemSlots[i];
        }
        return null;
    }

    private void Update()
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (itemSlots[i].ItemName != "")
                Debug.Log("Item " + itemSlots[i].ItemName + " Slot Number " + i + " Quantity: " + itemSlots[i].Quantity);
        }
    }
}