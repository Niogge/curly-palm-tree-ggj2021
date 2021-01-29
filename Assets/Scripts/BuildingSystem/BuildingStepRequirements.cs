using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Stone, Wood, Leaf, Metal, Coral }

[System.Serializable]
public class BuildingStepRequirements
{
    public ItemType ItemType;
    public int Quantity;
    public int GivenQuantity;
}