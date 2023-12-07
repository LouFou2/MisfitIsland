using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using System.Collections.Generic;
using UnityEngine.TextCore.Text;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        NextRound,
        PlayerTurn,
        WolfTurn,
        VillageTurn,
        GuessTheWolfRound,
        Victory,
        Defeat
    }
    private int numberOfRounds;
    private GameState currentState;
    private bool storyCoroutineRunning;

    [SerializeField]
    private float playerInfluence;
    [SerializeField]
    private float wolfInfluence;

    [SerializeField]
    private GameObject[] characters;

    [SerializeField]
    private WolfBehaviour wolfBehaviour;
    private GameObject wolf;
    private int wolfIndex;

    [SerializeField]
    private AntiBehaviour antiBehaviour;
    [SerializeField]
    private float infectionChanceIncrease = 10f;

    [SerializeField]
    private TextMeshProUGUI storyText;
    [SerializeField]
    private TextMeshProUGUI playerStatsText;
    [SerializeField]
    private TextMeshProUGUI wolfStatsText;

    void Start()
    {
        
        //select the wolf
        wolfIndex = Random.Range(0, characters.Length);
        wolf = characters[wolfIndex];
        for(int i = 0; i < characters.Length; i++) 
        {
            CharacterStatus characterStatus = characters[i].GetComponent<CharacterStatus>();

            if (characterStatus != null)
            {
                characterStatus.isWolf = (i == wolfIndex);
            }
            else
            {
                Debug.LogError("Character " + i + " is missing CharacterStatus component. Please add the CharacterStatus component.");

            }
        }
        currentState = GameState.NextRound;
        StartCoroutine(StoryCoroutine());
    }

    void Update()
    {
        // == What happens in a round? == //
        switch (currentState)
        {
            case GameState.NextRound:
                if (!storyCoroutineRunning) // Check if coroutine is not already running
                {
                    StartCoroutine(StoryCoroutine());
                }
                break;

            case GameState.PlayerTurn:
                HandlePlayerTurn();
                break;
            case GameState.WolfTurn:
                HandleWolfTurn();
                break;
            case GameState.VillageTurn:
                HandleVillageTurn();
                break;
            case GameState.GuessTheWolfRound:
                HandleWolfGuess();
                break;
            case GameState.Victory:
                HandleVictory(wolf);
                break;
            case GameState.Defeat:
                HandleDefeat();
                break;
        }
        DisplayPlayerStats();
        DisplayWolfStats();
    }
    IEnumerator StoryCoroutine()
    {
        storyCoroutineRunning = true;

        numberOfRounds += 1;

        if (numberOfRounds == 1) 
        {
            yield return new WaitForSeconds(0.2f);

            storyText.text = "There is a werewolf in your midst. Try to identify the troublemaker.";
            yield return new WaitForSeconds(1f);

            storyText.text = "your job is to brainwash... I mean, train villagers to support your cause.";
            yield return new WaitForSeconds(1f);

            storyText.text = "Select a villager to train for your cause.";
            storyCoroutineRunning = false;
            currentState = GameState.PlayerTurn;
        }
        else if (numberOfRounds == 2)
        {
            yield return new WaitForSeconds(0.2f);

            storyText.text = "Some villagers might turn against you.";
            yield return new WaitForSeconds(1f);

            storyText.text = "Select a villager to train.";
            storyCoroutineRunning = false;
            currentState = GameState.PlayerTurn;
        }
        else if (numberOfRounds > 2 && numberOfRounds < 6) 
        {
            yield return new WaitForSeconds(0.2f);

            if (playerInfluence < 0) 
            {
                storyText.text = "There is dissent growing among your villagers...";
            }
            else if (playerInfluence == 0) 
            {
                storyText.text = "You don't seem to be having much influence...";
            }
            else
                storyText.text = "You seem to be swaying some villagers to your cause";
            yield return new WaitForSeconds(1f);


            storyText.text = "Select a villager to train.";
            storyCoroutineRunning = false;
            currentState = GameState.PlayerTurn;
        }
        else if (numberOfRounds == 6)  // change this to set the total amount of rounds
        {
            yield return new WaitForSeconds(0.2f);

            storyText.text = "You must identify the werewolf.";
            yield return new WaitForSeconds(1f);

            storyText.text = "If you choose wrong you fail.";
            storyCoroutineRunning = false;
            currentState = GameState.GuessTheWolfRound;
        }
    }
    IEnumerator VictoryCoroutine()
    {
        storyCoroutineRunning = true;
        yield return new WaitForSeconds(0.2f); // Adjust as needed

        storyText.text = "You have found the werewolf!";
        //Debug.Log("There is a werewolf in your midst.");
        yield return new WaitForSeconds(1f); // Adjust as needed

        //Exit scene
        SceneManager.LoadScene("Menu");
    }
    IEnumerator DefeatCoroutine()
    {
        storyCoroutineRunning = true;
        yield return new WaitForSeconds(0.2f); // Adjust as needed

        storyText.text = "You executed an innocent villager!";
        yield return new WaitForSeconds(1f);

        storyText.text = "GAME OVER";
        yield return new WaitForSeconds(2f);

        //Exit scene
        SceneManager.LoadScene("Menu");
    }
    void HandlePlayerTurn() 
    {
        for (int i = 0; i < characters.Length; i++)
        {
            CharacterStatus characterStatus = characters[i].GetComponent<CharacterStatus>();

            // Clear indexes for "character in training"
            characterStatus.isInTraining = false;

            playerInfluence += characterStatus.proOrAntiSpectrum; // iteratively calculate the total "pro or anti" points of village
        }
        playerInfluence /= characters.Length; // average "pro vs anti" sentiment of villagers
        wolfInfluence = -playerInfluence;

        for (int i = 0; i <= 9; i++)
        {
            KeyCode keyCode = KeyCode.Alpha0 + i;

            if (Input.GetKeyDown(keyCode))
            {
                // Check if the entered number is equal to the wolfIndex
                if (i == wolfIndex)
                {
                    CharacterStatus characterStatus = characters[i].GetComponent<CharacterStatus>();
                    characterStatus.isInTraining = true;
                    currentState = GameState.VillageTurn; // skips Wolf Turn if Wolf is in training
                }
                // Select character for training
                else if (i < characters.Length)
                {
                    CharacterStatus characterStatus = characters[i].GetComponent<CharacterStatus>();
                    characterStatus.isInTraining = true;
                    characterStatus.UpdateProOrAntiStat(10 + playerInfluence); // TODO handle influence effect better
                    currentState = GameState.WolfTurn;
                }
                else
                {
                    currentState = GameState.WolfTurn;
                }
            }
        }
    }
    void HandleWolfTurn()
    {
        List<int> potentialTargets = new List<int>();

        for (int i = 0; i < characters.Length; i++)
        {
            CharacterStatus characterStatus = characters[i].GetComponent<CharacterStatus>();
            if (i != wolfIndex && !characterStatus.isInfected && !characterStatus.isInTraining)
            {
                potentialTargets.Add(i);
            }
        }
        if (potentialTargets.Count > 0)
        {
            int randomIndex = Random.Range(0, potentialTargets.Count);
            int newTargetIndex = potentialTargets[randomIndex];

            // Check if the infection is successful
            bool infect = wolfBehaviour.infectSuccess;

            // Set isInfected for the new target if infection is successful
            if (infect) 
            { 
                characters[newTargetIndex].GetComponent<CharacterStatus>().UpdateProOrAntiStat(-10f - wolfInfluence);
            }
            
        }
        currentState = GameState.VillageTurn;
    }
    void HandleVillageTurn() 
    {
        List<int> potentialTargets = new List<int>();
        List<int> antiAgents = new List<int>();

        // iterate through characters, ignoring wolfIndex and alreadyInfectedTargets
        for (int i = 0; i < characters.Length; i++)
        {
            CharacterStatus characterStatus = characters[i].GetComponent<CharacterStatus>();

            if (characterStatus.isInfected && !characterStatus.isInTraining) 
            {
                characterStatus.ModifyInfectionChance(infectionChanceIncrease);
            }
            if (characterStatus.isInfected && !characterStatus.isWolf && !characterStatus.isInTraining)
            {
                antiAgents.Add(i);
            }
            if (!characterStatus.isInfected && !characterStatus.isWolf && !characterStatus.isInTraining) 
            { 
                potentialTargets.Add(i);
            }
            
        }
        if(antiAgents.Count > 0) 
        {
            foreach (int i in antiAgents) 
            {
                CharacterStatus characterStatus = characters[i].GetComponent<CharacterStatus>();
                bool infect = characterStatus.InfectSuccess();

                if (potentialTargets.Count > 0)
                {
                    int randomIndex = Random.Range(0, potentialTargets.Count);
                    int newTargetIndex = potentialTargets[randomIndex];

                    // Modify the "pro or anti" stat for the new target if infection is successful
                    if (infect) 
                    {
                        characters[newTargetIndex].GetComponent<CharacterStatus>().UpdateProOrAntiStat(-10f);
                        potentialTargets.Remove(newTargetIndex);
                    }
                }
            }
        }
        
        // check if wolf has won
        bool allInfected = true;

        foreach (var character in characters)
        {
            CharacterStatus characterStatus = character.GetComponent<CharacterStatus>();

            if (!characterStatus.isInfected && !characterStatus.isWolf)
            {
                allInfected = false;
                break;
            }
        }

        if (allInfected)
        {
            currentState = GameState.Defeat;
        }
        else
            currentState = GameState.NextRound;
    }
    void HandleWolfGuess() 
    {
        foreach (var character in characters)
        {
            CharacterStatus characterStatus = character.GetComponent<CharacterStatus>();
            characterStatus.isInTraining = false;
        }

        for (int i = 0; i <= 9; i++)
        {
            KeyCode keyCode = KeyCode.Alpha0 + i;

            if (Input.GetKeyDown(keyCode))
            {
                // Check if the entered number is equal to the wolfIndex
                if (i == wolfIndex)
                {
                    currentState = GameState.Victory;
                }
                else
                {
                    currentState = GameState.Defeat;
                }
            }
        }

    }
    void HandleVictory(GameObject wolf) 
    {
        CharacterStatus wolfStatus = wolf.GetComponent<CharacterStatus>();
        wolfStatus.isWolfRevealed = true;
        if (!storyCoroutineRunning)
            StartCoroutine(VictoryCoroutine());
    }
    void HandleDefeat() 
    {
        CharacterStatus wolfStatus = wolf.GetComponent<CharacterStatus>();
        wolfStatus.isWolfRevealed = true;
        if (!storyCoroutineRunning)
            StartCoroutine(DefeatCoroutine());
    }
    void DisplayPlayerStats() 
    {
        playerStatsText.text = "Player Influence: " + playerInfluence.ToString();
    }
    void DisplayWolfStats()
    {
        wolfStatsText.text = "Wolf Influence: " + wolfInfluence.ToString();
    }
}
