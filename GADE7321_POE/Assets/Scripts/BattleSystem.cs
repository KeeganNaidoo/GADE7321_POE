using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { Start, Player1Turn, Player2Turn, Winner }

public class BattleSystem : MonoBehaviour
{
    public GameObject player1Prefab1;
    public GameObject player1Prefab2;
    public GameObject player2Prefab1;
    public GameObject player2Prefab2;
    
    public Transform player1Character1BattleStation;
    public Transform player1Character2BattleStation;
    public Transform player2Character1BattleStation;
    public Transform player2Character2BattleStation;
    
    public Unit player1Unit1;
    public Unit player1Unit2;
    public Unit player2Unit1;
    public Unit player2Unit2;

    public Unit targetUnit;
    public BattleHUD targetUnitHUD;

    public Text dialogueText;
    
    public GameObject winnerText;
    
    public BattleHUD player1Character1HUD;
    public BattleHUD player1Character2HUD;
    public BattleHUD player2Character1HUD;
    public BattleHUD player2Character2HUD;
    
    public GameObject p1CharacterSelectPanel;
    public GameObject p2CharacterSelectPanel;
    public GameObject p1Character1ActionsPanel;
    public GameObject p1Character2ActionsPanel;
    public GameObject p2Character1ActionsPanel;
    public GameObject p2Character2ActionsPanel;
    
    // Variables to store character buttons
    public Button character1Button;
    public Button character2Button;

    // Indicator prefab
    public GameObject indicatorPrefab;

    // Variables to store the instantiated indicators
    private GameObject _character1Indicator;
    private GameObject _character2Indicator;
    
    public BattleState state;
    
    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.Start;
        StartCoroutine(SetupBattle());
        
        // disable button panels on start
        p1CharacterSelectPanel.SetActive(false);
        p2CharacterSelectPanel.SetActive(false);
    }
    
    IEnumerator SetupBattle()
    {
        GameObject player1Character1 = Instantiate(player1Prefab1, player1Character1BattleStation);
        player1Unit1 =player1Character1.GetComponent<Unit>();
        GameObject player1Character2 = Instantiate(player1Prefab2, player1Character2BattleStation);
        player1Unit2 =player1Character2.GetComponent<Unit>();
        GameObject player2Character1 = Instantiate(player2Prefab1, player2Character1BattleStation);
        player2Unit1 =player2Character1.GetComponent<Unit>();
        GameObject player2Character2 = Instantiate(player2Prefab2, player2Character2BattleStation);
        player2Unit2 =player2Character2.GetComponent<Unit>();
        
        dialogueText.text = "Battle begins!\nPlayer 1: " + player1Unit1.unitName + " and " + player1Unit2.unitName + "\nVS\nPlayer 2: " + player2Unit1.unitName + " and " + player2Unit2.unitName;
        
        player1Character1HUD.SetHUD(player1Unit1);
        player1Character2HUD.SetHUD(player1Unit2);
        player2Character1HUD.SetHUD(player2Unit1);
        player2Character2HUD.SetHUD(player2Unit2);

        yield return new WaitForSeconds(2f);
        
        RandomisePlayerTurn();
    }
    
    void SwitchToOpponentTurn()
    {
        if (state == BattleState.Player1Turn)
        {
            state = BattleState.Player2Turn;
            dialogueText.text = "Player 2's turn!";
            
        }
        else if (state == BattleState.Player2Turn)
        {
            state = BattleState.Player1Turn;
            dialogueText.text = "Player 1's turn!";
            
        }
    }
    
    void EndBattle()
    {
        if (state == BattleState.Winner)
        {
            winnerText.SetActive(true);
        }
    }
    
    void RandomisePlayerTurn()
    {
        if (UnityEngine.Random.Range(0, 2) == 0)
        {
            state = BattleState.Player1Turn;
            Debug.Log("Player 1 starts first!");
            
        }
        else
        {
            state = BattleState.Player2Turn;
            Debug.Log("Player 2 starts first!");
            
        }
        
        StartTurn();
    }
    
    void StartTurn()
    {
        switch (state)
        {
            case BattleState.Player1Turn:
                Debug.Log("Player 1's turn!");
                dialogueText.text = "Player 1's turn!\nChoose a Character:";
                // Call a method to handle player 1's turn
                p1CharacterSelectPanel.SetActive(true);
                p2CharacterSelectPanel.SetActive(false);
                // Hide indicators
                // HideIndicators();
                break;
            case BattleState.Player2Turn:
                Debug.Log("Player 2's turn!");
                dialogueText.text = "Player 2's turn!\nChoose a Character:";
                // Call a method to handle player 2's turn
                p1CharacterSelectPanel.SetActive(false);
                p2CharacterSelectPanel.SetActive(true);
                break;
        }
    }
    
    IEnumerator Character1MeleeAttack()
    {
        // target unit is the enemy Character the current player chooses to attack & and then call a method to deal damage to the enemy 
        bool isDead = targetUnit.TakeDamage(player1Unit1.meleeDamage);
        targetUnitHUD.SetHP(targetUnit.currentHealth);
        
        yield return new WaitForSeconds(1f);
        
        // call a method to identify if the opponents character is still alive and which is the current state and switch to other players state
        if (isDead)
        {
            state = BattleState.Winner;
            EndBattle();
            // end battle
        }
        else
        {
            // call a method to identify which is the current state and switch to other players state
            SwitchToOpponentTurn();
        }
    }
    
    IEnumerator Character1RangedAttack()
    {
        // target unit is the enemy Character the current player chooses to attack & and then call a method to deal damage to the enemy 
        bool isDead = targetUnit.TakeDamage(player1Unit1.rangedDamage);
        targetUnitHUD.SetHP(targetUnit.currentHealth);
        
        yield return new WaitForSeconds(1f);
        
        // call a method to identify if the opponents character is still alive and which is the current state and switch to other players state
        if (isDead)
        {
            state = BattleState.Winner;
            EndBattle();
            // end battle
        }
        else
        {
            // call a method to identify which is the current state and switch to other players state
            SwitchToOpponentTurn();
        }
    }

    public void OnP1Character1MeleeAttackButton()
    {
        StartCoroutine(Character1MeleeAttack());
    }
    
    public void OnP1Character1RangedAttackButton()
    {
        StartCoroutine(Character1RangedAttack());
    }

    public void OnP2Character1MeleeAttackButton()
    {
        StartCoroutine(Character1MeleeAttack());
    }
    
    public void OnP2Character1RangedAttackButton()
    {
        StartCoroutine(Character1RangedAttack());
    }
    
    public void OnP1Character2MeleeAttackButton()
    {
        StartCoroutine(Character1MeleeAttack());
    }
    
    public void OnP1Character2RangedAttackButton()
    {
        StartCoroutine(Character1RangedAttack());
    }
    
    public void OnP2Character2MeleeAttackButton()
    {
        StartCoroutine(Character1MeleeAttack());
    }
    
    public void OnP2Character2RangedAttackButton()
    {
        StartCoroutine(Character1RangedAttack());
    }
    
    

    
    
    /*
    // Method to handle when a player chooses a character to attack with
    public void Player1AttackWithCharacter(string unitName)
    {
        // Hide character buttons
        character1Button.gameObject.SetActive(false);
        character2Button.gameObject.SetActive(false);

        // Show indicator above the chosen character
        switch (unitName)
        {
            case "Knight":
                if (_character1Indicator == null)
                {
                    _character1Indicator = Instantiate(indicatorPrefab, player1Prefab1.transform.position + Vector3.up * 2f, Quaternion.identity);
                }
                else
                {
                    _character1Indicator.SetActive(true);
                }
                break;
            case "Mage":
                if (_character2Indicator == null)
                {
                    _character2Indicator = Instantiate(indicatorPrefab, player1Prefab2.transform.position + Vector3.up * 2f, Quaternion.identity);
                }
                else
                {
                    _character2Indicator.SetActive(true);
                }
                break;
        }

        // Now you can handle the attack logic for the chosen character
        Debug.Log("Attacking with " + unitName);
    }
    
    // Method to hide the indicators
    private void HideIndicators()
    {
        if (_character1Indicator != null)
        {
            _character1Indicator.SetActive(false);
        }
        if (_character2Indicator != null)
        {
            _character2Indicator.SetActive(false);
        }
    }
*/
    
}
