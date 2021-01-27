using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnTogglePauseUI(bool show);
public delegate void OnTryInterruptPause();
public delegate void OnDestroyInteractable(IInteractable interactable);
public delegate void OnLongInteractionBegin();
public delegate void OnLongInteractionEnd(IInteractable interactable);
public delegate void OnShowInteractionHint(IInteractable interactable);
public delegate void OnRefreshInteractionHint(IInteractable interactable);
public delegate void OnHideInteractionHint();

public class GameEventSystem
{

    public static event OnTogglePauseUI TogglePauseUIEvent;
    public static event OnTryInterruptPause TryInterruptPauseEvent;

    public static event OnDestroyInteractable DestroyInteractableEvent;
    public static event OnLongInteractionBegin BeginLongInteractionEvent;
    public static event OnLongInteractionEnd EndLongInteractionEvent;
    public static event OnShowInteractionHint ShowInteractionHintEvent;
    public static event OnRefreshInteractionHint RefreshInteractionHintEvent;
    public static event OnHideInteractionHint HideInteractionHintEvent;

    /// <summary>
    /// You should call this when you want the interaction hint to appear.
    /// </summary>
    /// <param name="interactable"></param>
    public static void ShowInteractionHint(IInteractable interactable)
    {
        ShowInteractionHintEvent.Invoke(interactable);
    }

    /// <summary>
    /// You should call this when for any reason, the interaction hint must be refreshed.
    /// </summary>
    /// <param name="interactable"></param>
    public static void RefreshInteractionHint(IInteractable interactable)
    {
        RefreshInteractionHintEvent.Invoke(interactable);
    }

    /// <summary>
    /// You should call this when you want the interaction hint to be hidden.
    /// </summary>
    public static void HideInteractionHint()
    {
        HideInteractionHintEvent.Invoke();
    }

    /// <summary>
    /// You should call this whenever an interaction which is not immediate begins.
    /// </summary>
    public static void BeginLongInteraction()
    {
        BeginLongInteractionEvent.Invoke();
    }

    /// <summary>
    /// You should call this whenever an interaction which is not immediate ends.
    /// </summary>
    /// <param name="interactable"></param>
    public static void EndLongInteraction(IInteractable interactable)
    {
        EndLongInteractionEvent.Invoke(interactable);
    }

    /// <summary>
    /// You should call this right before an interactable is removed from the scene and Destroy is called on it.
    /// </summary>
    /// <param name="interactable"></param>
    public static void DestroyInteractable(IInteractable interactable)
    {
        DestroyInteractableEvent.Invoke(interactable);
    }

    /// <summary>
    /// You should call this whenever the player enters or exits the Pause menu.
    /// </summary>
    /// <param name="show"></param>
    public static void TogglePauseUI(bool show)
    {
        TogglePauseUIEvent.Invoke(show);
    }

    /// <summary>
    /// You should call this whenever the player navigates back in the Pause menu.
    /// </summary>
    public static void TryInterruptPause()
    {
        TryInterruptPauseEvent.Invoke();
    }
}