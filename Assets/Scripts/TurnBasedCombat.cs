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
        PLAYERATTACKCHOICE,
        ENEMYATTACKCHOICE,
        PLAYERDEFENDCHOICE,
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
    private bool isBlocking;

    // Use this for initialization
    void Start()
    {
        currentState = BattleStates.START;

        _player = GameObject.FindWithTag("Player");

        Player = _player.GetComponent<PlayerCombatBehaviour>();

        isBlocking = false;
    }

    public void AttackButton()
    {
        StartCoroutine(Attack());
    }
    public void DefendButton()
    {
        StartCoroutine(Defend());
    }
    public IEnumerator Attack()
    {
        if (currentState == BattleStates.START || currentState == BattleStates.ENEMYATTACKCHOICE)
        {
            currentState = BattleStates.PLAYERATTACKCHOICE;
        }
        UIUpdate.HideUI();
        StateMachine();
        yield return new WaitForSeconds(2);
        currentState = BattleStates.ENEMYATTACKCHOICE;
        UIUpdate.ShowUI();
        StateMachine();
    }
    public IEnumerator Defend()
    {
        if (currentState == BattleStates.START || currentState == BattleStates.ENEMYATTACKCHOICE)
        {
            currentState = BattleStates.PLAYERDEFENDCHOICE;
        }
        UIUpdate.HideUI();
        StateMachine();
        yield return new WaitForSeconds(2);
        currentState = BattleStates.ENEMYATTACKCHOICE;
        UIUpdate.ShowUI();
        StateMachine();
    }

    public void StateMachine()
    {
        switch (currentState)
        {
            case (BattleStates.START):
                {
                    UIUpdate.SetEnemyHealthUI();
                    UIUpdate.SetPlayerHealthUI();
                }
                break;
            case (BattleStates.PLAYERATTACKCHOICE):
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
            case (BattleStates.ENEMYATTACKCHOICE):
                {
                    if (isBlocking == false)
                    {
                        Enemy.GetComponent<EnemyBehaviour>().DoDamage(Player);
                    }
                    else if (isBlocking == true)
                    {
                        Enemy.GetComponent<EnemyBehaviour>().DoDamage(Player);
                        Player.PlayerCurrentHP += 5;
                    }
                    if (Player.PlayerCurrentHP <= 0)
                    {
                        Enemy.GetComponent<EnemyBehaviour>().outCombat();
                    }
                    UIUpdate.SetPlayerHealthUI();
                }
                break;
            case (BattleStates.PLAYERDEFENDCHOICE):
                {
                    isBlocking = true;
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
