﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Role { Villager, Courtesan, Guard, Villain, Last }
public enum FieldOfViewType { Circle, Cone }

public class Player : MonoBehaviour
{
    //This class should only contain the main player data, not behaviours!

    public bool IsPaused; //<- true if the player is in the pause menu
    public float MovementSpeed;
    public float RotationSpeed;
    public bool IsInteracting;
    public bool IsCrafting;
    public int StartingInventorySlots;

    private void Awake()
    {
        GameEventSystem.TogglePauseUIEvent += (bool paused) => IsPaused = paused;
        GameEventSystem.ToggleCraftingUIEvent += ToggleCraft;
        Inventory.MaxSlots = StartingInventorySlots;
    }

    private void ToggleCraft(bool status)
    {
        IsCrafting = status;
        if (!status)
            GetComponent<InputHandler>().GetPlayerControls().GamePlay.Movement.Enable();
        else
            GetComponent<InputHandler>().GetPlayerControls().GamePlay.Movement.Disable();
    }
}