using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Owner
{
    Player, Enemy
}
public class LifeManager : MonoBehaviour
{
    public int Life = 50;
    [SerializeField]
    Owner owner;

    [HideInInspector]
    public int AcctuallyLife;

    private void Start()
    {
        AcctuallyLife = Life;
    }
    private void OnEnable()
    {
        AcctuallyLife = Life;
    }

    public void AddDamage(int damage)
    {
        AcctuallyLife -= damage;

        if (AcctuallyLife <= 0)
            OnDead();
    }

    void OnDead()
    {
        switch (owner)
        {
            case Owner.Player:

                break;

            case Owner.Enemy:
                gameObject.SetActive(false);
                break;
        }
    }
}
