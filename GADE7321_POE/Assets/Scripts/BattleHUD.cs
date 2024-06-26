using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text nameText;
    public Slider hpSlider;
    public Unit unit;

    public void SetHUD(Unit unit)
    {
            
        nameText.text = unit.unitName;
        hpSlider.maxValue = unit.maxHealth;
        hpSlider.value = unit.currentHealth;
    }
    
    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    }

    public void SetHP(Unit playerUnit1)
    {
        throw new System.NotImplementedException();
    }
}
