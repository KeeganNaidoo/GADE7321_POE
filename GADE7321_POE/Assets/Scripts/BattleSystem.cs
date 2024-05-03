using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { Start, Player1Turn, Player2Turn, Winner }

public class BattleSystem : MonoBehaviour
{
    public static BattleSystem instance;
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

    public GameObject player1HUD1;
    public GameObject player1HUD2;
    public GameObject player2HUD1;
    public GameObject player2HUD2;

    public string characterSelected;
    public string targetSelected = "melee";

    public int playerTurn;
    
    public GameObject hudPrefab;
    
    // Variables to store character buttons
    //public Button character1Button;
    //public Button character2Button;

    // Indicator prefab
    //public GameObject indicatorPrefab;

    // Variables to store the instantiated indicators
    //private GameObject _character1Indicator;
    //private GameObject _character2Indicator;
    
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

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    /// <summary>
    /// This code snippet defines an IEnumerator function named SetupBattle.
    /// It instantiates game objects for player characters,
    /// retrieves their associated Unit components,
    /// sets up dialogue text for the battle scenario,
    /// updates the HUD for each player character,
    /// waits for 2 seconds using yield return new WaitForSeconds(2f),
    /// and then calls a method to randomize player turns.
    /// </summary>
    /// <returns></returns>
    IEnumerator SetupBattle()
    {
        GameObject player1Character1 = Instantiate(player1Prefab1, player1Character1BattleStation);
        player1Unit1 =player1Character1.GetComponent<Unit>();
        GameObject _player1HUD = Instantiate(hudPrefab, player1HUD1.transform);
        player1Unit1.unitHUD = _player1HUD.GetComponent<BattleHUD>();
        player1Unit1.unitName = "P1U1";
        
        GameObject player1Character2 = Instantiate(player1Prefab2, player1Character2BattleStation);
        player1Unit2 =player1Character2.GetComponent<Unit>();
        GameObject __player1HUD = Instantiate(hudPrefab, player1HUD2.transform);
        player1Unit2.unitHUD = __player1HUD.GetComponent<BattleHUD>();
        player1Unit2.unitName = "P1U2";
        
        GameObject player2Character1 = Instantiate(player2Prefab1, player2Character1BattleStation);
        player2Unit1 =player2Character1.GetComponent<Unit>();
        GameObject _player2HUD = Instantiate(hudPrefab, player2HUD1.transform);
        player2Unit1.unitHUD = _player2HUD.GetComponent<BattleHUD>();
        player2Unit2.unitName = "P2U1";
        
        
        GameObject player2Character2 = Instantiate(player2Prefab2, player2Character2BattleStation);
        player2Unit2 =player2Character2.GetComponent<Unit>();
        GameObject __player2HUD = Instantiate(hudPrefab, player2HUD2.transform);
        player2Unit2.unitHUD = __player2HUD.GetComponent<BattleHUD>();
        player2Unit2.unitName = "P2U2";
        
        dialogueText.text = "Battle begins!\nPlayer 1: " + player1Unit1.unitName + " and " + player1Unit2.unitName + "\nVS\nPlayer 2: " + player2Unit1.unitName + " and " + player2Unit2.unitName;
        
        _player1HUD.GetComponent<BattleHUD>().SetHUD(player1Unit1);
        __player1HUD.GetComponent<BattleHUD>().SetHUD(player1Unit2);
        _player2HUD.GetComponent<BattleHUD>().SetHUD(player2Unit1);
        __player2HUD.GetComponent<BattleHUD>().SetHUD(player2Unit2);

        yield return new WaitForSeconds(2f);
        
        RandomisePlayerTurn();
    }
    
    public void SwitchToOpponentTurn()
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
    
    public void EndBattle()
    {
        if (state == BattleState.Winner)
        {
            winnerText.SetActive(true);
        }
    }
    
    /// <summary>
    /// This code snippet randomly selects which player gets to start a turn in a game.
    /// It uses UnityEngine.Random.Range(0, 2) to randomly choose between 0 and 1,
    /// assigning the turn to either Player 1 or Player 2 accordingly.
    /// Finally, it calls the StartTurn() function to begin the game sequence.
    /// </summary>
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

    public void CharacterAttack(string AttackType)
    {
        Unit selectedUnit = null;
        if (playerTurn == 1)
        {
            if (characterSelected == "melee")
            {
                selectedUnit = player1Unit1;
            }
            else
            {
                selectedUnit = player1Unit2;
            }

            if (targetSelected == "melee")
            {
                selectedUnit.targetUnit = player2Unit1;
            }
            else
            {
                selectedUnit.targetUnit = player2Unit2;
            }
        }
        else
        {
            if (characterSelected == "melee")
            {
                selectedUnit = player2Unit1;
            }
            else
            {
                selectedUnit = player2Unit2;
            }
            if (targetSelected == "melee")
            {
                selectedUnit.targetUnit = player2Unit1;
            }
            else
            {
                selectedUnit.targetUnit = player2Unit2;
            }
        }
        
        selectedUnit.Attack(AttackType);
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
