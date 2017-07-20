using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class TurnBasedCombat : MonoBehaviour
{
    public enum BattleStates
    {
        START,
        PLAYERCHOICE,
        ENEMYCHOICE,
        LOSE,
        WIN
    }

    [HideInInspector]
    public PlayerCombatBehaviour Player;
    public CombatUI UIUpdate;
    public CombatEvent Won = new CombatEvent();
    public CombatEvent Lost = new CombatEvent();

    private BattleStates currentState;
    private GameObject _player;
    static public GameObject Enemy;

    // Use this for initialization
    void Start()
    {
        currentState = BattleStates.START;

        _player = GameObject.FindWithTag("Player");

        Player = _player.GetComponent<PlayerCombatBehaviour>();
    }

    public void AttackButton()
    {
        StartCoroutine(Attack());
    }

    public IEnumerator Attack()
    {
        if (currentState == BattleStates.START || currentState == BattleStates.ENEMYCHOICE)
        {
            currentState = BattleStates.PLAYERCHOICE;
        }
        UIUpdate.HideUI();
        StateMachine();
        yield return new WaitForSeconds(2);
        currentState = BattleStates.ENEMYCHOICE;
        UIUpdate.ShowUI();
        StateMachine();
    }

    public void StateMachine()
    {
        switch (currentState)
        {
            case (BattleStates.START):
                {
                    Debug.Log(Enemy.GetComponent<EnemyBehaviour>().EnemyCurrentHP);
                    UIUpdate.SetEnemyHealthUI();
                    UIUpdate.SetPlayerHealthUI();
                }
                break;
            case (BattleStates.PLAYERCHOICE):
                {
                    Player.DoDamage(Enemy.GetComponent<EnemyBehaviour>());
                    if (Enemy.GetComponent<EnemyBehaviour>().EnemyCurrentHP <= 0)
                    {
                        Destroy(Enemy.GetComponent<EnemyBehaviour>().gameObject);
                        Enemy.GetComponent<EnemyBehaviour>().outCombat();
                    }
                    UIUpdate.SetEnemyHealthUI();
                }
                break;
            case (BattleStates.ENEMYCHOICE):
                {
                    Enemy.GetComponent<EnemyBehaviour>().DoDamage(Player);
                    if (Player.PlayerCurrentHP <= 0)
                    {
                        Enemy.GetComponent<EnemyBehaviour>().outCombat();
                    }
                    UIUpdate.SetPlayerHealthUI();
                }
                break;
            case (BattleStates.LOSE):
                {
                    Won.Invoke();
                }
                break;
            case (BattleStates.WIN):
                {
                    Lost.Invoke();
                }
                break;
        }
    }

    public void StartedCombat()
    {
        UIUpdate.Enemy = Enemy.GetComponent<EnemyBehaviour>();
        currentState = BattleStates.START;
        StateMachine();
    }
    
    [Serializable]
    public class CombatEvent : UnityEvent { }
}
