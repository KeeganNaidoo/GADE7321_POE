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
}
