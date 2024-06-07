using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AIGameState
{
    public int playerKnightHealth;
    public int playerMageHealth;
    public int enemyKnightHealth;
    public int enemyMageHealth;
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
        }
        else
        {
            moves.Add(("MeleeAttack", "PlayerKnight"));
            moves.Add(("MeleeAttack", "PlayerMage"));
            moves.Add(("RangedAttack", "PlayerKnight"));
            moves.Add(("RangedAttack", "PlayerMage"));
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
            currentState = state.currentState,
            isPlayerTurn = !state.isPlayerTurn
        };

        // Apply move effects to the new game state
        switch (move.action)
        {
            case "MeleeAttack":
                if (move.target == "EnemyKnight") newState.enemyKnightHealth -= 20;
                else if (move.target == "EnemyMage") newState.enemyMageHealth -= 20;
                else if (move.target == "PlayerKnight") newState.playerKnightHealth -= 20;
                else if (move.target == "PlayerMage") newState.playerMageHealth -= 20;
                break;
            case "RangedAttack":
                if (move.target == "EnemyKnight") newState.enemyKnightHealth -= 15;
                else if (move.target == "EnemyMage") newState.enemyMageHealth -= 15;
                else if (move.target == "PlayerKnight") newState.playerKnightHealth -= 15;
                else if (move.target == "PlayerMage") newState.playerMageHealth -= 15;
                break;
        }

        return newState;
    }
}
