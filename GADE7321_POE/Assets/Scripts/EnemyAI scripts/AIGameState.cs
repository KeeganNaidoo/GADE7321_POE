using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;



public class AIGameState
{
    public int playerKnightHealth;
    public int playerMageHealth;
    public int enemyKnightHealth;
    public int enemyMageHealth;
    public int nextAttackBuff;
    public AIBattleState currentState;
    public bool isPlayerTurn;
}

    // Class to handle AI logic using Minimax algorithm
public class MinimaxAI
{
    private const int MAX_DEPTH = 2; // Maximum depth for minimax recursion

    // Method to get the best move for the AI
    public (string action, string target) GetBestMove(AIGameState state)
    {
        int bestValue = int.MinValue;
        (string action, string target) bestMove = ("", "");

        // Iterate through all possible moves and choose the one with the highest minimax value
        foreach (var move in GetAllPossibleMoves(state))
        {
            AIGameState newState = SimulateMove(state, move);
            int moveValue = Minimax(newState, MAX_DEPTH, false);
            if (moveValue > bestValue)
            {
                bestValue = moveValue;
                bestMove = move;
            }
        }

        return bestMove;
        
    }

    // Minimax algorithm implementation
    private int Minimax(AIGameState state, int depth, bool isMaximizingPlayer)
    {
        if (depth == 0 || IsTerminalState(state))
        {
            return EvaluateState(state); // Evaluate the game state if terminal or max depth reached
        }

        if (isMaximizingPlayer)
        {
            int bestValue = int.MinValue;
            foreach (var move in GetAllPossibleMoves(state))
            {
                AIGameState newState = SimulateMove(state, move);
                int moveValue = Minimax(newState, depth - 1, false);
                bestValue = Mathf.Max(bestValue, moveValue);
            }
            return bestValue;
        }
        else
        {
            int bestValue = int.MaxValue;
            foreach (var move in GetAllPossibleMoves(state))
            {
                AIGameState newState = SimulateMove(state, move);
                int moveValue = Minimax(newState, depth - 1, true);
                bestValue = Mathf.Min(bestValue, moveValue);
            }
            return bestValue;
        }
    }

    // Check if the game has reached a terminal state
    private bool IsTerminalState(AIGameState state)
    {
        return state.playerKnightHealth <= 0 && state.playerMageHealth <= 0 ||
               state.enemyKnightHealth <= 0 && state.enemyMageHealth <= 0;
    }

    // Evaluate the game state by comparing player and enemy health
    private int EvaluateState(AIGameState state)
    {
        int playerHealth = state.playerKnightHealth + state.playerMageHealth;
        int enemyHealth = state.enemyKnightHealth + state.enemyMageHealth;
        return playerHealth - enemyHealth;
    }

    // Get all possible moves for the current player
    private IEnumerable<(string action, string target)> GetAllPossibleMoves(AIGameState state)
    {
        var moves = new List<(string action, string target)>();

        if (state.isPlayerTurn)
        {
            
            moves.Add(("MeleeAttack", "EnemyKnight"));
            moves.Add(("MeleeAttack", "EnemyMage"));
            moves.Add(("RangedAttack", "EnemyKnight"));
            moves.Add(("RangedAttack", "EnemyMage"));
            moves.Add(("Defend", "PlayerKnight")); // Add defend action here();
            moves.Add(("Special", "EnemyKnight")); // Add special action here();
            moves.Add(("Special", "EnemyMage")); 
            moves.Add(("Buff", "PlayerKnight")); // Add buff action here();
            moves.Add(("Buff", "PlayerMage")); 
            moves.Add(("Heal", "PlayerKnight")); // Add heal action here();
            moves.Add(("Heal", "PlayerMage")); 
            
        }
        else 
        {
            moves.Add(("MeleeAttack", "PlayerKnight"));
            moves.Add(("MeleeAttack", "PlayerMage"));
            moves.Add(("RangedAttack", "PlayerKnight"));
            moves.Add(("RangedAttack", "PlayerMage"));
            moves.Add(("Defend", "EnemyKnight")); // Add defend action here();
            moves.Add(("Special", "PlayerKnight")); // Add special action here();
            moves.Add(("Special", "PlayerMage")); 
            moves.Add(("Buff", "EnemyKnight")); // Add buff action here();
            moves.Add(("Buff", "EnemyMage")); 
            moves.Add(("Heal", "EnemyKnight")); // Add heal action here();
            moves.Add(("Heal", "EnemyMage"));
        }
        

        return moves;
    }

    // Simulate the effect of a move on the game state
    public AIGameState SimulateMove(AIGameState state, (string action, string target) move)
    {
        AIGameState newState = new AIGameState
        {
            playerKnightHealth = state.playerKnightHealth,
            playerMageHealth = state.playerMageHealth,
            enemyKnightHealth = state.enemyKnightHealth,
            enemyMageHealth = state.enemyMageHealth,
            nextAttackBuff = state.nextAttackBuff,
            currentState = state.currentState,
            isPlayerTurn = !state.isPlayerTurn
        };

        // Apply move effects to the new game state
        
        switch (move.action)
        {
            case "Melee Attack":
                if (move.target == "Player Knight")
                {
                    Debug.Log("Player knight health: " + newState.playerKnightHealth);
                    newState.playerKnightHealth -= 20;
                }
                else if (move.target == "Player Mage")
                {
                    Debug.Log("Player mage health: " + newState.playerMageHealth);
                    newState.playerMageHealth -= 20;
                }
                //else if (move.target == "PlayerKnight") newState.playerKnightHealth -= 20;
                //else if (move.target == "PlayerMage") newState.playerMageHealth -= 20;
                break;
            case "Ranged Attack":
                //if (move.target == "EnemyKnight") newState.enemyKnightHealth -= 15;
                //else if (move.target == "EnemyMage") newState.enemyMageHealth -= 15;
                if (move.target == "Player Knight")
                {
                    newState.playerKnightHealth -= 15;
                    Debug.Log("Player knight health: " + newState.playerKnightHealth);
                }
                else if (move.target == "Player Mage")
                {
                    newState.playerMageHealth -= 15;
                    Debug.Log("Player mage health: " + newState.playerMageHealth);
                }
                break;
            case "Shield":
                if (move.target == "EnemyKnight")
                {
                    Debug.Log("Player knight health: " + newState.enemyKnightHealth);
                    newState.enemyKnightHealth -= 20; // Reduce health by 50%
                }
                else if (move.target == "EnemyMage")
                {
                    Debug.Log("Player mage health: " + newState.enemyMageHealth);
                    newState.enemyMageHealth += (int)(newState.enemyMageHealth * 0.5); // Reduce health by 50%
                }
                break;
            case "Special":
                if (move.target == "PlayerKnight")
                {
                    //create special attack
                    newState.playerKnightHealth -= 30;
                    Debug.Log("Player knight health: " + newState.playerKnightHealth);
                }
                else if (move.target == "PlayerMage")
                {
                    //create special attack
                    newState.playerMageHealth -= 30;
                    Debug.Log("Player mage health: " + newState.playerMageHealth);
                }
                break;
            case "Buff":
                if (move.target is "EnemyKnight" or "EnemyMage")
                {
                    newState.nextAttackBuff += 10;
                    Debug.Log("Next attack buff increased by 10");
                }
                break;
            case "Heal":
                if (move.target == "EnemyKnight")
                {
                    newState.enemyKnightHealth += 30;
                    Debug.Log("Enemy knight health: " + newState.enemyKnightHealth);
                }
                else if (move.target == "EnemyMage")
                {
                    newState.enemyMageHealth += 30;
                    Debug.Log("Enemy mage health: " + newState.enemyMageHealth);
                }
                break;
        }

        return newState;
    }
}
