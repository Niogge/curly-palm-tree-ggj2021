using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBehaviour : MonoBehaviour
{
    public int Damage = 5;
    [HideInInspector]
    public bool CanDamage;


    private void OnTriggerEnter(Collider other)
    {
        if (CanDamage)
        {
            Debug.Log(gameObject.tag + " sta triggerando: " + other.tag);
            if (gameObject.CompareTag("Enemy") && other.CompareTag("DamageCollider") || gameObject.CompareTag("Player") && other.CompareTag("DamageCollider_enemy"))
            {
                CanDamage = false;
                other.GetComponentInParent<LifeManager>().AddDamage(Damage);
            }
        }
    }
}


  
