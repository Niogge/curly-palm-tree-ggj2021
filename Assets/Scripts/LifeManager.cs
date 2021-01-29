using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    public int Life = 50;

    [HideInInspector]
    public int AcctuallyLife;

    private void Start()
    {
        AcctuallyLife = Life;
    }

    public void AddDamage(int damage)
    {
        AcctuallyLife -= Life;

        if (AcctuallyLife <= 0)
            OnDead();
    }

    void OnDead()
    {
        Debug.Log("porcamadonnalaida");
    }
}
