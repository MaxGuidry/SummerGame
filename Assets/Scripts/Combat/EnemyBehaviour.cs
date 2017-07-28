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
    private TurnBasedCombat combat;

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
        this.gameObject.tag = "InActiveEnemy";
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            combat = other.GetComponent<TurnBasedCombat>();
            inCombat();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            outCombat();
    }

    public void inCombat()
    {
        //Time.timeScale = 0;
        TurnBasedCombat.Enemy = this.gameObject;
        combat.StartedCombat();
        InCombatUI.Invoke();
        this.gameObject.tag = "ActiveEnemy";
    }

    public void outCombat()
    {
        //Time.timeScale = 1;
        OutOfCombatUI.Invoke();
        TurnBasedCombat.Enemy = null;
        this.gameObject.tag = "InActiveEnemy";
    }

    [Serializable]
    public class CombatUIEvent : UnityEvent { }
}
