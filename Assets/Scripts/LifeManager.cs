using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum Owner
{
    Player, Enemy
}
public class LifeManager : MonoBehaviour
{
    public int Life = 50;
    public Slider LifeBar;

    [SerializeField]
    Owner owner;

    [HideInInspector]
    public int AcctuallyLife;

    private void Start()
    {
        AcctuallyLife = Life;
        LifeBar.maxValue = Life;
    }
    private void OnEnable()
    {
        AcctuallyLife = Life;
        LifeBar.maxValue = Life;
    }

    public void AddDamage(int damage)
    {
        AcctuallyLife -= damage;
        LifeBar.value = AcctuallyLife;

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
