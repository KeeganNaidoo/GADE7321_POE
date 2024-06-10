using System.Collections;
using System.Collections.Generic;
using EnemyAI_scripts;
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

    
    public void Attack( string attackType)
    {
        bool isDead = false;

        switch (attackType)
        {
            case "melee":
                isDead = targetUnit.TakeDamage(meleeDamage);
                break;
            case "ranged":
                isDead = targetUnit.TakeDamage(rangedDamage);
                break;
            case "special":
                isDead = targetUnit.TakeDamage(specialAttack);
                break;
            case "heal":
                isDead = targetUnit.TakeDamage(-heal);
                targetUnit.currentHealth += heal;
                if (targetUnit.currentHealth > targetUnit.maxHealth)
                {
                    targetUnit.currentHealth = targetUnit.maxHealth;
                }
                break;
        }

        //enemyHUD.SetHP(target.currentHealth); // targetUnitcurrentHealth
        //unitHUD.SetHP(currentHealth);
        
        Debug.Log(unitName + " attacks " + targetUnit.unitName + " with " + attackType + " attack. Remaining HP: " + targetUnit.currentHealth);

        if (isDead)
        {
            AIBattleSystem.instance.state = AIBattleState.AIWinner;
            BattleSystem.instance.EndBattle();
        }
        else
        {
            BattleSystem.instance.SwitchToOpponentTurn();
        }
    }

    public void Heal(Unit target)
    {
        target.currentHealth += heal;
        if (target.currentHealth > target.maxHealth)
        {
            target.currentHealth = target.maxHealth;
        }
        target.unitHUD.SetHP(target.currentHealth);
        BattleSystem.instance.SwitchToOpponentTurn();
    }

    public void ApplyBuff(Unit target)
    {
        target.buff = (int)(target.meleeDamage * 1.5);
        BattleSystem.instance.SwitchToOpponentTurn();
    }


    /*public void Attack(string attackType)
    {
        Debug.Log(unitName + " attacks " + targetUnit.unitName + " with " + attackType + " attack. Remaining HP: " + targetUnit.currentHealth);
        throw new System.NotImplementedException();
    }*/
}
