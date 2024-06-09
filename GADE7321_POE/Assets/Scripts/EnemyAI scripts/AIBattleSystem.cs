using System.Collections;
using UnityEngine;
using UnityEngine.UI;


// Class to manage the battle system
namespace EnemyAI_scripts
{
    public class AIBattleSystem : MonoBehaviour
    {
        public static AIBattleSystem instance; // Singleton instance

        public GameObject characterSelectPanel; // Panel for character selection
        public GameObject knightActionPanel; // Panel for knight actions
        public GameObject mageActionPanel; // Panel for mage actions
        public GameObject enemySelectPanel; // Panel for enemy selection

        public GameObject playerPrefab1;
        public GameObject playerPrefab2;
        public GameObject enemyPrefab1;
        public GameObject enemyPrefab2;
    
        public Transform playerCharacter1BattleStation;
        public Transform playerCharacter2BattleStation;
        public Transform enemyCharacter1BattleStation;
        public Transform enemyCharacter2BattleStation;
        
        public Transform playerKnightHUDPosition;
        public Transform playerMageHUDPosition;
        public Transform enemy1HUDPosition;
        public Transform enemy2HUDPosition;
    
        public Unit playerUnit1;
        public Unit playerUnit2;
        public Unit enemyUnit1;
        public Unit enemyUnit2;
        
        private Unit playerKnight;
        private Unit playerMage;
        private Unit enemyKnight;
        private Unit enemyMage;
    
        public Button knightButton; // Button to select knight character
        public Button mageButton; // Button to select mage character

        public Button knightMeleeButton; // Button for knight melee attack
        public Button knightRangedButton; // Button for knight ranged attack
        public Button knightDefendButton; // Button for knight defend action
        public Button knightSpecialButton; // Button for knight special attack

        public Button mageMeleeButton; // Button for mage melee attack
        public Button mageRangedButton; // Button for mage ranged attack
        public Button mageBuffButton; // Button for mage buff action
        public Button mageHealButton; // Button for mage heal action

        public Button enemyKnightButton; // Button to select enemy knight as target
        public Button enemyMageButton; // Button to select enemy mage as target

        public Text dialogueText; // Text element to display dialogue messages

        public GameObject hudPrefab;
        public BattleHUD playerKnightHUD; // HUD for player's knight
        public BattleHUD playerMageHUD; // HUD for player's mage
        public BattleHUD enemy1HUD; // HUD for enemy's knight
        public BattleHUD enemy2HUD; // HUD for enemy's mage
        
        public GameObject playerHUD1;
        public GameObject playerHUD2;
        public GameObject enemyHUD1;
        public GameObject enemyHUD2;

        private string _selectedCharacter; // Currently selected character
        private string _selectedAction; // Currently selected action
        private string _selectedEnemy; // Currently selected enemy
    
        public AIBattleState state; // Current state of the battle
        private MinimaxAI _minimaxAI; // Instance of the MinimaxAI class
        private AIGameState _gameState; // Current game state

        void Awake()
        {
            instance = this; // Set singleton instance
        }
        // Start is called before the first frame update
        void Start()
        {
            // Add listeners to character selection buttons
            knightButton.onClick.AddListener(() => OnCharacterSelect("Knight"));
            mageButton.onClick.AddListener(() => OnCharacterSelect("Mage"));

            // Add listeners to knight action buttons
            knightMeleeButton.onClick.AddListener(() => OnActionSelect("melee"));
            knightRangedButton.onClick.AddListener(() => OnActionSelect("ranged"));
            knightDefendButton.onClick.AddListener(() => OnActionSelect("defend"));
            knightSpecialButton.onClick.AddListener(() => OnActionSelect("special"));

            // Add listeners to mage action buttons
            mageMeleeButton.onClick.AddListener(() => OnActionSelect("melee"));
            mageRangedButton.onClick.AddListener(() => OnActionSelect("ranged"));
            mageBuffButton.onClick.AddListener(() => OnActionSelect("buff"));
            mageHealButton.onClick.AddListener(() => OnActionSelect("heal"));

            // Add listeners to enemy selection buttons
            enemyKnightButton.onClick.AddListener(() => OnEnemySelect("EnemyKnight"));
            enemyMageButton.onClick.AddListener(() => OnEnemySelect("EnemyMage"));
        
            _minimaxAI = new MinimaxAI();
            state = AIBattleState.AIStart;
            SetupPveBattle(); // Set up the battle at the start of the game
        }

        // Method to set up the battle
        void SetupPveBattle()
        {
            _gameState = new AIGameState
            {
                playerKnightHealth = 100,
                playerMageHealth = 80,
                enemyKnightHealth = 100,
                enemyMageHealth = 80,
                currentState = AIBattleState.AIStart,
                isPlayerTurn = true
            };

            // Set up character prefabs
            InitialiseCharacterPrefabs();

            RandomisePlayerTurn(); // Randomly determine who goes first
        }

        private void InitialiseCharacterPrefabs()
        {
            GameObject playerCharacter1 = Instantiate(playerPrefab1, playerCharacter1BattleStation);
            Unit playerUnit1 = playerCharacter1.GetComponent<Unit>();
            GameObject playerHUD1 = Instantiate(hudPrefab, playerKnightHUDPosition);
            playerUnit1.unitHUD = playerHUD1.GetComponent<BattleHUD>();
            playerUnit1.unitHUD.SetHUD(playerUnit1);
            
            
            /*
            GameObject playerCharacter2 = Instantiate(playerPrefab2, playerCharacter2BattleStation);
            playerUnit2 =playerCharacter2.GetComponent<Unit>();
            GameObject __player1HUD = Instantiate(hudPrefab, playerMageHUD.transform);
            playerUnit2.unitHUD = __player1HUD.GetComponent<BattleHUD>();
            playerUnit2.unitName = "MAGE";
            */
            GameObject playerCharacter2 = Instantiate(playerPrefab2, playerCharacter2BattleStation);
            Unit playerUnit2 = playerCharacter2.GetComponent<Unit>();
            GameObject playerHUD2 = Instantiate(hudPrefab, playerMageHUDPosition);
            playerUnit2.unitHUD = playerHUD2.GetComponent<BattleHUD>();
            playerUnit2.unitHUD.SetHUD(playerUnit2);
        
            //GameObject enemyCharacter1 = Instantiate(enemyPrefab1, enemyCharacter1BattleStation);
            //enemyUnit1 =enemyCharacter1.GetComponent<Unit>();
           // GameObject _player2HUD = Instantiate(hudPrefab, enemy1HUD.transform);
            //enemyUnit1.unitHUD = _player2HUD.GetComponent<BattleHUD>();
            //enemyUnit1.unitName = "WHITE SKELETON";
            
            GameObject enemyCharacter1 = Instantiate(enemyPrefab1, enemyCharacter1BattleStation);
            Unit enemyUnit1 = enemyCharacter1.GetComponent<Unit>();
            GameObject enemy1HUD = Instantiate(hudPrefab, enemy1HUDPosition);
            enemyUnit1.unitHUD = enemy1HUD.GetComponent<BattleHUD>();
            enemyUnit1.unitHUD.SetHUD(enemyUnit1);
        
            //GameObject enemyCharacter2 = Instantiate(enemyPrefab2, enemyCharacter2BattleStation);
            //enemyUnit2 =enemyCharacter2.GetComponent<Unit>();
            //GameObject __player2HUD = Instantiate(hudPrefab, enemy2HUD.transform);
            //enemyUnit2.unitHUD = __player2HUD.GetComponent<BattleHUD>();
            //enemyUnit2.unitName = "BROWN SKELETON";
            
            GameObject enemyCharacter2 = Instantiate(enemyPrefab2, enemyCharacter2BattleStation);
            Unit enemyUnit2 = enemyCharacter2.GetComponent<Unit>();
            GameObject enemy2HUD = Instantiate(hudPrefab, enemy2HUDPosition);
            enemyUnit2.unitHUD = enemy2HUD.GetComponent<BattleHUD>();
            enemyUnit2.unitHUD.SetHUD(enemyUnit2);
        
        }
        // Method to randomly determine which player goes first
        private void RandomisePlayerTurn()
        {
            if (UnityEngine.Random.Range(0, 2) == 0)
            {
                state = AIBattleState.AIPlayerTurn;
                _gameState.isPlayerTurn = true;
            }
            else
            {
                state = AIBattleState.AIEnemyTurn;
                _gameState.isPlayerTurn = false;
            }
            StartTurn(); // Start the first turn
        }

        // Method to start the current player's turn
        public void StartTurn()
        {
            if (state == AIBattleState.AIPlayerTurn)
            {
                // Enable player UI, character selection, etc.
                dialogueText.text = "Your turn! Choose a character.";
                Debug.Log("Player's turn");
                EnablePanel(characterSelectPanel, true);
            }
            else if (state == AIBattleState.AIEnemyTurn)
            {
                StartCoroutine(EnemyTurn()); // Start the enemy's turn coroutine
            }
        }

        // Coroutine to handle the enemy's turn
        IEnumerator EnemyTurn()
        {
            dialogueText.text = "Enemy's turn...";
            Debug.Log("Enemy's turn"); // Log that the enemy's turn has started();
            yield return new WaitForSeconds(1f); // Wait for a second before the enemy acts

            dialogueText.text = "Enemy is thinking of an attack...";
            Debug.Log("Enemy is thinking of an attack..."); // Log that the enemy is thinking of an attack
            
            yield return new WaitForSeconds(3f); // Wait for a second before the AI makes its move
            
            var bestMove = _minimaxAI.GetBestMove(_gameState); // Get the best move for the AI
            _gameState = _minimaxAI.SimulateMove(_gameState, bestMove); // Simulate the AI's move

            //UpdateGameStateUI(); // Update the game state UI

            dialogueText.text = "Enemy " + bestMove.action + " " + bestMove.target; // Log the AI's move
            Debug.Log("Enemy " + bestMove.action + " " + bestMove.target);
            
            yield return new WaitForSeconds(2f); // Wait for a second before the AI makes its next move

            if (CheckForWinner()) // Check if there's a winner
            {
                state = AIBattleState.AIWinner;
                EndBattle(); // End the game if there's a winner
            }
            else
            {
                state = AIBattleState.AIPlayerTurn; // Switch to the player's turn
                _gameState.isPlayerTurn = true;
                StartTurn(); // Start the player's turn
            }
        }

        void OnCharacterSelect(string character)
        {
            _selectedCharacter = character;
            EnablePanel(characterSelectPanel, false);

            if (character == "Knight")
            {
                dialogueText.text = character + " selected. Choose an action:";
                Debug.Log("knight selected");
                EnablePanel(knightActionPanel, true);
            }
            else if (character == "Mage")
            {
                dialogueText.text = character + " selected. Choose an action:";
                Debug.Log("mage selected");
                EnablePanel(mageActionPanel, true);
            }
            
            
        }

        void OnActionSelect(string action)
        {
            _selectedAction = action;
            EnablePanel(knightActionPanel, false);
            EnablePanel(mageActionPanel, false);
            EnablePanel(enemySelectPanel, true);

            if (action == "melee")
            {
                dialogueText.text = action + " Attack selected. Choose an enemy:";
                Debug.Log("melee attack selected"); // Log that the melee attack has been selected();
            }
            else if (action == "ranged")
            {
                dialogueText.text = action + " Attack selected. Choose an enemy:";
                Debug.Log("ranged attack selected");
            }
            else if (action == "heal")
            {
                dialogueText.text = action + " selected. Choose a character:";
                Debug.Log("heal selected");
            }
            else if (action == "buff")
            {
                dialogueText.text = action + " selected. Choose a character:";
                Debug.Log("buff selected");
            }
            else if (action == "special")
            {
                dialogueText.text = "OH YES!!!" + action + " Attack selected. Choose an enemy:";
                Debug.Log("special attack selected");
            }
            else if (action == "defend")
            {
                dialogueText.text = action + " selected.";
                Debug.Log("defend selected");
            }
        }

        void OnEnemySelect(string enemy)
        {
            _selectedEnemy = enemy;
            EnablePanel(enemySelectPanel, false);

            if (enemy == "EnemyKnight")
            {
                dialogueText.text = enemy + " selected.";
                Debug.Log("Enemy knight selected");
            }
            else if (enemy == "EnemyMage")
            {
                dialogueText.text = enemy + " selected.";
                Debug.Log("Enemy mage selected");
            }
            // Find the target unit and perform the selected action
            /*
            Unit target = null;
            if (enemy == "EnemyKnight")
            {
                target = enemy1HUD.unit;
                Debug.Log("Enemy knight selected");
            }
            else if (enemy == "EnemyMage")
            {
                target = enemy2HUD.unit;
                Debug.Log("Enemy mage selected");
            }

            // Perform the selected action on the target unit
            if (_selectedCharacter == "Knight")
            {
                playerKnightHUD.unit.targetUnit = target;
                playerKnightHUD.unit.Attack(_selectedAction);
            }
            else if (_selectedCharacter == "Mage")
            {
                playerMageHUD.unit.targetUnit = target;
                if (_selectedAction == "heal")
                {
                    playerMageHUD.unit.Heal(target);
                }
                else if (_selectedAction == "buff")
                {
                    playerMageHUD.unit.ApplyBuff(target);
                }
                else
                {
                    playerMageHUD.unit.Attack(_selectedAction);
                }
            }*/
            
            Unit targetUnit = null;
            
            switch (enemy)
            {
                case "Knight":
                    targetUnit = enemyKnight;
                    break;
                case "Mage":
                    targetUnit = enemyMage;
                    break;
            }

            if (_selectedCharacter != null && targetUnit != null)
            {
               //_selectedCharacter.Attack(_selectedEnemy, _selectedAction);
               Debug.Log("Selected Character: " + _selectedCharacter + " Target Unit: " + targetUnit + " Action: " + _selectedAction); // Log the selected character and action();
            }

            EnablePanel(enemySelectPanel, false);
            state = AIBattleState.AIEnemyTurn;
            StartCoroutine(EnemyTurn());
        }

        void EndBattle()
        {
            if (state == AIBattleState.AIWinner)
            {
                dialogueText.text = "You won!";
            }
            else
            {
                dialogueText.text = "You lost!";
            }
        }

        bool CheckForWinner()
        {
            // Check if any player has won
            // Add logic to check if all enemy units or player units are dead
            return false;
        }

        void EnablePanel(GameObject panel, bool isEnabled)
        {
            panel.SetActive(isEnabled);
        }

        public void SwitchToOpponentTurn()
        {
            if (state == AIBattleState.AIPlayerTurn)
            {
                state = AIBattleState.AIEnemyTurn;
                StartCoroutine(EnemyTurn());
            }
            else if (state == AIBattleState.AIEnemyTurn)
            {
                state = AIBattleState.AIPlayerTurn;
                StartTurn();
            }
        }
    }
}
