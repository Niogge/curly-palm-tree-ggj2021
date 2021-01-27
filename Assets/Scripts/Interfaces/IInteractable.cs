using UnityEngine;

public enum InteractableType { Shop, LightPost, LightGenerator, Chest, AssemblyDoor }
public enum InteractionType { Normal, Special }

public interface IInteractable
{
    /// <summary>
    /// <para>This is the index of the interactable inside the worldInteractables Dictionary of WorlInteractablesHandler.</para>
    /// <para>It is automatically assigned when all the world interactables are loaded when OnLevelWasLoaded is called.</para>
    /// <para>This is also used to send or receive an interactable through the network.</para>
    /// </summary>
    int CategoryID { get; set; } //<- l'ID dell'oggetto nella sua categoria, stabilito dal WorlInteractablesHandler al caricamento della scena

    /// <summary>
    /// <para>This is the text to be displayed when this interaction is possible and the interaction hint is visible.</para>
    /// <para>Being this a property, you can define multiple get results based on your conditions.</para>
    /// </summary>
    string NormalInteractionHintText { get; } //<- cosa appare nel testo di interazione?
    /// <summary>
    /// <para>This is the text to be displayed when this interaction is possible and the interaction hint is visible.</para>
    /// <para>Being this a property, you can define multiple get results based on your conditions.</para>
    /// </summary>
    string SpecialInteractionHintText { get; } //<- cosa appare nel testo di interazione?

    /// <summary>
    /// <para>Define if this interactable is currently interactable or not.</para>
    /// <para>For example, if someone is using it and is already interacting with it, you should set it to false.</para>
    /// </summary>
    bool IsInteractable { get; set; } //<- se è true in quel momento ci si può interagire, altrimenti no

    /// <summary>
    /// <para>This is simply the type of the interactable, usually recalls the Class Name.</para>
    /// <para>The type defines in which interactables list it will be put inside from the WorldInteractablesHandler.</para>
    /// <para>This is used along with the CategoryID to send or receive an interactable through the network.</para>
    /// </summary>
    InteractableType InteractableType { get; } //<- che tipologia di interagibile è? (corrisponde alla classe)

    /// <summary>
    /// <para>This is the animation to be performed when doing a normal interaction on this interactable.</para>
    /// <para>It is usually defined if there is one, and if this interaction is considered Long and requires an animation.</para>
    /// <para>Being this a property, you can define specific animations based on what you prefere.</para>
    /// </summary>
    AnimationType NormalInteractionAnimation { get; } //<- che animazione va performata in caso di normal interaction?

    /// <summary>
    /// <para>This is the animation to be performed when doing a special interaction on this interactable.</para>
    /// <para>It is usually defined if there is one, and if this interaction is considered Long and requires an animation.</para>
    /// <para>Being this a property, you can define specific animations based on what you prefere.</para>
    /// </summary>
    AnimationType SpecialInteractionAnimation { get; } //<- che animazione va performata in caso di special interaction?

    /// <summary>
    /// <para>This is literally the complete behaviour to perform on this item when a normal interaction is triggered on it.</para>
    /// <para>It should firstly call the BaseNormalStartSetup, and then define what happens on this player's machine.</para>
    /// </summary>
    void StartNormalInteraction(); //<- cosa succede quando l'interazione viene avviata/comincia?

    /// <summary>
    /// <para>This is literally the complete behaviour to perform on this item when a special interaction is triggered on it.</para>
    /// <para>It should firstly call the BaseSpecialStartSetup, and then define what happens on this player's machine.</para>
    /// </summary>
    void StartSpecialInteraction(); //<- cosa succede quando l'interazione viene avviata/comincia?

    /// <summary>
    /// <para>This is literally what happens when the normal interaction has been completed on this interactable.</para>
    /// <para>Beware, this is not what happens if the interaction is interrupted. Use InterruptInteraction for that.</para>
    /// <para>This requires a role because this method could be called over the network, and the end of the interaction may vary from the user's role.</para>
    /// </summary>
    void CompleteNormalInteraction(Role role); //<- cosa succede quando l'interazione viene fermata/finisce?

    /// <summary>
    /// <para>This is literally what happens when the special interaction has been completed on this interactable.</para>
    /// <para>Beware, this is not what happens if the interaction is interrupted. Use InterruptInteraction for that.</para>
    /// <para>This requires a role because this method could be called over the network, and the end of the interaction may vary from the user's role.</para>
    /// </summary>
    void CompleteSpecialInteraction(Role role); //<- cosa succede quando l'interazione viene fermata/finisce?

    /// <summary>
    /// <para>This defines the standard things that happen when a normal interaction is started by anyone on this interactable.</para>
    /// <para>You can consider it as a constructor method to be called before StartNormalInteraction.</para>
    /// <para>It decouples the consequences of the interaction from the interactable's changes.</para>
    /// <para>So if someone else is interacting with this, you only trigger the base, while if you are, you call the base first and the start later.</para>
    /// </summary>
    void BaseNormalStartSetup(); //<- cosa accade di base quando l'interazione normale inizia?

    /// <summary>
    /// <para>This defines the standard things that happen when a special interaction is started by anyone on this interactable.</para>
    /// <para>You can consider it as a constructor method to be called before StartSpecialInteraction.</para>
    /// <para>It decouples the consequences of the interaction from the interactable's changes.</para>
    /// <para>So if someone else is interacting with this, you only trigger the base, while if you are, you call the base first and the start later.</para>
    /// </summary>
    void BaseSpecialStartSetup(); //<- cosa accade di base quando l'interazione speciale inizia?

    /// <summary>
    /// <para>This defines what happens if an interaction is interrupted on this interactable.</para>
    /// <para>You should define this for long interactions only.</para>
    /// <para>Beware, this is not what happens if the interaction is completed. Use CompleteInteraction for that.</para>
    /// </summary>
    void InterruptInteraction(); //<- cosa accade quando interrompo di forza l'interazione?

    /// <summary>
    /// This is just a debug method. Remove it in production. It is currently used to ease Line-Drawing operations.
    /// </summary>
    /// <returns></returns>
    Vector3 GetPosition(); //<- debug only, usata per fare il DrawLine verso l'interactable più vicino, remove in production!
}