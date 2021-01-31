using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGameEvent : MonoBehaviour
{
    InputHandler inputHandler;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
        }
    }
}
