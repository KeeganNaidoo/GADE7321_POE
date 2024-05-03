using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    // public int unitLevel; --- not used yet
    public int meleeDamage;
    public int rangedDamage;
    // block -- reduces incoming damage by 50% for 2 turns.
    public int block;
    // buff -- increases damage of selected character by 50% for 2 turns.
    public int buff;
    // special attack -- strong attack that deals 50 damage
    public int specialAttack;
    // heal -- heals 50 health for a selected character
    public int heal;
    public int maxHealth;
    public int currentHealth;

    public Unit targetUnit;
    public string move;
    public BattleHUD unitHUD;
    
    
    public bool TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        if(currentHealth <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    
    public void Attack(string attackType)
    {
        // target unit is the enemy Character the current player chooses to attack & and then call a method to deal damage to the enemy 
        bool isDead = false;
        switch (attackType)
        {
            case "melee":
                isDead = targetUnit.TakeDamage(meleeDamage);
                break;
            case "ranged":
                isDead = targetUnit.TakeDamage(rangedDamage);
                break;
        }
        
        targetUnit.unitHUD.SetHP(targetUnit.currentHealth);
        Debug.Log("name" + unitName + "Attacks" + targetUnit.unitName + "Health" + targetUnit.currentHealth);
        // figure out how to wait for a sec
        
        // call a method to identify if the opponents character is still alive and which is the current state and switch to other players state
        if (isDead)
        {
            BattleSystem.instance.state = BattleState.Winner;
            BattleSystem.instance.EndBattle();
            
            // end battle
        }
        else
        {
            // call a method to identify which is the current state and switch to other players state
            BattleSystem.instance.SwitchToOpponentTurn();
        }
    }
}
