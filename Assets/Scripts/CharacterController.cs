﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private InputHandler inputHandler;
    private Player player;
    private CameraBehaviour cameraTarget;
    private PlayerInteractionArea playerInteractionArea;
    private AnimatorHandler animatorHandler;

    private void Awake()
    {
        animatorHandler = GetComponent<AnimatorHandler>();
        inputHandler = GetComponent<InputHandler>();
        inputHandler.Init();
        player = GetComponent<Player>();
        cameraTarget = GameObject.Find("Target").GetComponent<CameraBehaviour>();
        cameraTarget.InputHandler = inputHandler;
        cameraTarget.Init(transform);
        playerInteractionArea = GetComponentInChildren<PlayerInteractionArea>();
    }

    void Update()
    {
        //player is in the pause menu
        if (player.IsPaused) //<- check inputs in the pause menu
        {
            //if pressing the pause key or the interrupt key, trigger the TRY INTERRUPT PAUSE event
            if (inputHandler.pauseButtonPressed || inputHandler.interruptButtonPressed)
                GameEventSystem.TryInterruptPause();

            return;
        }

        //player is playing
        if (!player.IsInteracting) //<- if the player is not blocked interacting with something
        {
            if (inputHandler.pauseButtonPressed)
            {
                GameEventSystem.TogglePauseUI(true);
            }

            //find average velocity vector on x/0/z plane using camera orientation and movement values
            Vector3 movementDirection = (cameraTarget.Camera.right * inputHandler.movementHorizontal) +
                (cameraTarget.CameraForward * -inputHandler.movementVertical);
            Debug.Log(movementDirection);

            //if moving
            if (movementDirection != Vector3.zero)
            {
                //set the animation
                animatorHandler.SetAnimation(AnimationType.Walk);
                //move the transform
                transform.position += movementDirection * player.MovementSpeed * Time.deltaTime;
                //look in the moving direction
                Quaternion lookDir = Quaternion.LookRotation(movementDirection, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookDir, player.RotationSpeed * Time.deltaTime);
                //transform.forward = Vector3.Lerp(transform.forward, movementDirection, player.RotationSpeed * Time.deltaTime);
            }
            else
            {
                animatorHandler.SetAnimation(AnimationType.Idle);
            }

            //if not already interacting, and trying to interact
            if (inputHandler.normalInteractButtonPressed)
            {
                //if there is an interactable nearby
                if (playerInteractionArea.NearestInteractable != null)
                {
                    //if the interactable is actually available to interact with it
                    if (playerInteractionArea.NearestInteractable.IsInteractable)
                    {
                        playerInteractionArea.NearestInteractable.StartNormalInteraction();
                    }
                }
            }
            if (inputHandler.specialInteractButtonPressed)
            {
                //if there is an interactable nearby
                if (playerInteractionArea.NearestInteractable != null)
                {
                    //if the interactable is actually available to interact with it
                    if (playerInteractionArea.NearestInteractable.IsInteractable)
                    {
                        //if i am the host, immediately interact
                        playerInteractionArea.NearestInteractable.StartSpecialInteraction();
                    }
                }
            }
        }
        else //if the player is blocked interacting with something
        {
            //if the player gives an interrupt input
            if (inputHandler.interruptButtonPressed)
            {
                //TODO? here we should also check if the interactable interaction is INTERRUPTIBLE.
                //stop the interaction
                playerInteractionArea.NearestInteractable.InterruptInteraction();
            }
        }
    }
}
