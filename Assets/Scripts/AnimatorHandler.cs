﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationType { None, Idle, Walk, Sabotage, Repair }

public class AnimatorHandler : MonoBehaviour
{
    //[SerializeField] Transform mesh;
    private Animator animator;
    private AnimationType currentAnimation; //<- the animation currently played (debug)
    public bool DebugAnimation; //debug only, if true prints out the currentAnimation in the update
    private void Awake()
    {
        animator = GetComponent<Animator>();
        SetAnimation(AnimationType.Idle);
    }

    /// <summary>
    /// Sets the right animation on this character. If you pass Nothing or type None, animation is not set.
    /// </summary>
    /// <param name="animationType"></param>
    public void SetAnimation(AnimationType animationType = AnimationType.None)
    {
        return;
        if (animationType == AnimationType.Walk)
            animator.SetBool("walk", true);
        if (animationType == AnimationType.Idle)
            animator.SetBool("walk", false);
        if (animationType == AnimationType.None)
            return;

        currentAnimation = animationType;
    }

    private void Update()
    {
        if (DebugAnimation)
            Debug.Log("Current Animation on: " + gameObject.name + " is " + currentAnimation.ToString());
    }
}