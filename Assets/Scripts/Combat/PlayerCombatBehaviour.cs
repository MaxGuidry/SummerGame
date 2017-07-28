using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatBehaviour : MonoBehaviour, IDamageable, IDamager {
    public int PlayerCurrentHP, PlayerDamage;
    [HideInInspector] public int PlayerMaxHP;

    public void DoDamage(IDamageable defender)
    {
        defender.TakeDamage(PlayerDamage);
    }

    public void TakeDamage(int amount)
    {
        PlayerCurrentHP -= amount;
    }

    // Use this for initialization
    void Start () {
        PlayerMaxHP = PlayerCurrentHP;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
