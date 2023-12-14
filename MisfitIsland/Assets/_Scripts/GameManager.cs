using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameDataSO gameData;
    [SerializeField]
    private int currentRound;
    [SerializeField]
    private enum GameState 
    {
        Setup,
        Intro,
        PlayEvent,
        Selection,
        Interview,
        Defeat,
        Victory
    }
    private GameState currentGameState;

    private void OnEnable()
    {
        // Subscribe to events if needed
    }

    private void OnDisable()
    {
        // Unsubscribe from events if needed
    }


    // Add other game-related functions here
}
