using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class to manage the battle system
public class AIBattleSystem : MonoBehaviour
{
    public AIBattleState state; // Current state of the battle
    private MinimaxAI minimaxAI; // Instance of the MinimaxAI class
    private AIGameState gameState; // Current game state

    // Start is called before the first frame update
    void Start()
    {
        minimaxAI = new MinimaxAI();
        state = AIBattleState.AIStart;
        SetupBattle(); // Set up the battle at the start of the game
    }

    // Method to set up the battle
    void SetupBattle()
    {
        gameState = new AIGameState
        {
            playerKnightHealth = 100,
            playerMageHealth = 80,
            enemyKnightHealth = 100,
            enemyMageHealth = 80,
            currentState = AIBattleState.AIStart,
            isPlayerTurn = true
        };

        RandomisePlayerTurn(); // Randomly determine who goes first
    }

    // Method to randomly determine which player goes first
    private void RandomisePlayerTurn()
    {
        if (UnityEngine.Random.Range(0, 2) == 0)
        {
            state = AIBattleState.AIPlayerTurn;
            gameState.isPlayerTurn = true;
        }
        else
        {
            state = AIBattleState.AIEnemyTurn;
            gameState.isPlayerTurn = false;
        }
        StartTurn(); // Start the first turn
    }

    // Method to start the current player's turn
    public void StartTurn()
    {
        if (state == AIBattleState.AIPlayerTurn)
        {
            // Enable player UI, character selection, etc.
        }
        else if (state == AIBattleState.AIEnemyTurn)
        {
            StartCoroutine(EnemyTurn()); // Start the enemy's turn coroutine
        }
    }

    // Coroutine to handle the enemy's turn
    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(1f); // Wait for a second before the enemy acts

        var bestMove = minimaxAI.GetBestMove(gameState); // Get the best move for the AI
        gameState = minimaxAI.SimulateMove(gameState, bestMove); // Simulate the AI's move

        UpdateGameStateUI(); // Update the game state UI

        if (CheckForWinner()) // Check if there's a winner
        {
            state = AIBattleState.AIWinner;
            EndGame(); // End the game if there's a winner
        }
        else
        {
            state = AIBattleState.AIPlayerTurn; // Switch to the player's turn
            gameState.isPlayerTurn = true;
            StartTurn(); // Start the player's turn
        }
    }

    // Method to update the game state UI
    void UpdateGameStateUI()
    {
        // Update health bars, dialogue text, etc.
    }

    // Method to check if there's a winner
    bool CheckForWinner()
    {
        return gameState.playerKnightHealth <= 0 && gameState.playerMageHealth <= 0 ||
               gameState.enemyKnightHealth <= 0 && gameState.enemyMageHealth <= 0;
    }

    // Method to handle the end of the game
    void EndGame()
    {
        // Show winner message, disable gameplay UI, etc.
    }
}
