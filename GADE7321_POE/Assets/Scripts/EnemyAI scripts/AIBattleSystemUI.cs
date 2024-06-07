using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIBattleSystemUI : MonoBehaviour
{
    
    public Button attackButton; // Reference to the attack button
    public Button defendButton; // Reference to the defend button
    // Other UI elements...

    void Start()
    {
        // Add listeners to buttons
        attackButton.onClick.AddListener(OnPlayerAttack);
        defendButton.onClick.AddListener(OnPlayerDefend);
        // Initialize other elements...
    }

    // Method called when player chooses to attack
    void OnPlayerAttack()
    {
        // Logic for player attack
        // Update game state and UI
        
        //AIGameState.isPlayerTurn = false;
        //state = AIBattleState.AIEnemyTurn;
        //StartTurn(); // Switch to enemy's turn
    }

    // Method called when player chooses to defend
    void OnPlayerDefend()
    {
        // Logic for player defend
        // Update game state and UI
        //gameState.isPlayerTurn = false;
        //state = AIBattleState.AIEnemyTurn;
        //StartTurn(); // Switch to enemy's turn
    }

    // Add more methods for other player actions
}
