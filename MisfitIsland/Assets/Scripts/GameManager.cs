using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Pause,
        PlayerGuess,
        WolfInfectAttempt,
        Victory,
        Defeat
    }
    private GameState currentState;
    private bool storyCoroutineRunning;

    [SerializeField]
    private GameObject[] characters;

    [SerializeField]
    private WolfBehaviour wolfBehaviour;
    private GameObject wolf;
    private int wolfIndex;
    
    private bool infect;

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
        currentState = GameState.Pause;
        StartCoroutine(StoryCoroutine());
    }

    void Update()
    {
        // == What happens in a round? == //
        switch (currentState)
        {

            case GameState.Pause:
                if (!storyCoroutineRunning) // Check if coroutine is not already running
                {
                    StartCoroutine(StoryCoroutine());
                }
                break;

            case GameState.PlayerGuess:
                HandlePlayerTurn();
                break;

            case GameState.WolfInfectAttempt:
                HandleWolfTurn();
                break;
            case GameState.Victory:
                HandleVictory();
                break;
            case GameState.Defeat:
                HandleDefeat();
                break;

        }
    }
    IEnumerator StoryCoroutine()
    {
        storyCoroutineRunning = true;
        yield return new WaitForSeconds(1f); // Adjust as needed

        Debug.Log("There is a wolf in your midst.");
        yield return new WaitForSeconds(2f); // Adjust as needed

        Debug.Log("You must guess who it is.");
        storyCoroutineRunning = false;
        currentState = GameState.PlayerGuess;
    }
    void HandlePlayerTurn() 
    {
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
                    currentState = GameState.WolfInfectAttempt;
                }
            }
        }
    }
    void HandleWolfTurn()
    {
        int infectionTargetIndex;

        do
        {
            infectionTargetIndex = Random.Range(0, characters.Length);
        } while (infectionTargetIndex == wolfIndex);

        GameObject infectionTarget = characters[infectionTargetIndex];

        // check if infection is successful
        infect = wolfBehaviour.infectSuccess;

        for (int i = 0; i < characters.Length; i++)
        {
            CharacterStatus characterStatus = characters[i].GetComponent<CharacterStatus>();
            characterStatus.isInfected = (i == infectionTargetIndex && infect);
            Debug.Log(i + ": " + characterStatus.isInfected);
        }
        // check if wolf has won
        bool allInfected = true;

        foreach (var character in characters)
        {
            CharacterStatus characterStatus = character.GetComponent<CharacterStatus>();

            if (!characterStatus.isInfected)
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
            currentState = GameState.Pause;
    }
    void HandleVictory() 
    {
        Debug.Log("You have found the wolf!");
    }
    void HandleDefeat() 
    {
        Debug.Log("Everyone's infected. GAME OVER");
    }
}
