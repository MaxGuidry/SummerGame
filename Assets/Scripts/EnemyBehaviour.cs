using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class EnemyBehaviour : MonoBehaviour, IDamageable, IDamager
{
    public int EnemyCurrentHP, EnemyDamage;
    [HideInInspector] public int EnemyMaxHP;
    public CombatUIEvent InCombatUI = new CombatUIEvent();
    public CombatUIEvent OutOfCombatUI = new CombatUIEvent();

    public void DoDamage(IDamageable defender)
    {
        defender.TakeDamage(EnemyDamage);
    }

    public void TakeDamage(int amount)
    {
        EnemyCurrentHP -= amount;
    }

    void Start()
    {
        EnemyMaxHP = EnemyCurrentHP;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inCombat();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            outCombat();
        }
    }

    void inCombat()
    {
        InCombatUI.Invoke();
        //Time.timeScale = 0;
    }

    void outCombat()
    {
        //Time.timeScale = 1;
        OutOfCombatUI.Invoke();
    }

    [Serializable]
    public class CombatUIEvent : UnityEvent { }
}
