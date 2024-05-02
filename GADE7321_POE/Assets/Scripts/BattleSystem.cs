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

    public Text dialogueText;
    
    public GameObject winnerText;
    
    public BattleHUD player1Character1HUD;
    public BattleHUD player1Character2HUD;
    public BattleHUD player2Character1HUD;
    public BattleHUD player2Character2HUD;
    
    
    
    public BattleState state;
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.Start;
        StartCoroutine(SetupBattle());
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
                dialogueText.text = "Player 1's turn!\nChoose an action:";
                // Call a method to handle player 1's turn
                break;
            case BattleState.Player2Turn:
                Debug.Log("Player 2's turn!");
                dialogueText.text = "Player 2's turn!\nChoose an action:";
                // Call a method to handle player 2's turn
                break;
        }
    }

    
}
