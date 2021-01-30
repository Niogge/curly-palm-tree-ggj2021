using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Role { Villager, Courtesan, Guard, Villain, Last }
public enum FieldOfViewType { Circle, Cone }

public class Player : MonoBehaviour
{
    //This class should only contain the main player data, not behaviours!

    public bool IsPaused; //<- true if the player is in the pause menu

    public string UserName;
    public Role Role;
    public FieldOfViewType FoWType;
    public float MovementSpeed;
    public float RotationSpeed;
    public bool IsInteracting;

    //TODO: This stuff should only be on YOUR prefabs, not others.
    public float LightGeneratorsRepairSpeed;
    public float LightGeneratorsSabotageSpeed;

    private void Awake()
    {
        GameEventSystem.TogglePauseUIEvent += (bool paused) => IsPaused = paused;
    }
}