using System;
using UnityEngine;

public class GameManager : StaticInstance<GameManager>
{
    [SerializeField]
    private GameDataSO gameData;

    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;
    
    [Serializable]
    public enum GameState
    {
        Intro           = 0,
        Event           = 1,
        Selection       = 2,
        Interview       = 3,
        Confrontation   = 4,
        Victory         = 5,
        Defeat          = 6,
    }

    // Listeners for GameEvents:
    private void OnEnable()
    {
        GameEvents.Instance.OnSetupComplete.AddListener(OnSetupComplete);
    }

    private void OnDisable()
    {
        GameEvents.Instance.OnSetupComplete.RemoveListener(OnSetupComplete);
    }
    public GameState State { get; private set; }
    public void ChangeState(GameState newState)
    {
        OnBeforeStateChanged?.Invoke(newState);

        State = newState;
        switch (newState)
        {
            case GameState.Intro:
                HandleIntro();
                break;
            case GameState.Event:
                HandleEvent();
                break;
            case GameState.Selection:
                HandleSelection();
                break;
            case GameState.Interview:
                HandleInterview();
                break;
            case GameState.Confrontation:
                HandleConfrontation();
                break;
            case GameState.Victory:
                HandleVictory();
                break;
            case GameState.Defeat:
                HandleDefeat();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnAfterStateChanged?.Invoke(newState);

        Debug.Log($"New state: {newState}");
    }
    private void OnSetupComplete()
    {
        ChangeState(GameState.Intro);
    }
    private void HandleIntro() 
    {
        // intro stuff...
        bool introFinished = false;

        // when intro stuff is finished:
        if(introFinished)
            ChangeState(GameState.Event);
    }
    private void HandleEvent()
    {
        // event stuff...
        bool eventFinished = false;

        // when event stuff is finished:
        if (eventFinished) 
        {
            // in most cases (if not last round), next is Selection:
            ChangeState(GameState.Selection);

            // if its the last round:
            ChangeState(GameState.Confrontation);
        }
        
    }
    private void HandleSelection() 
    {
        // selection stuff...
        bool selectionFinished = false;

        // when selection stuff is finished:
        if (selectionFinished)
            ChangeState(GameState.Interview);
    }
    private void HandleInterview() 
    {
        // interview stuff...
        bool interviewFinished = false;

        // when interview stuff is finished:
        if (interviewFinished)
        ChangeState(GameState.Event); // loop back to Event (this main loop will count a number of rounds)
    }
    private void HandleConfrontation() 
    {
        // confrontation stuff...
        bool confrontFinished = false;

        // when interview stuff is finished:
        if (confrontFinished)
        { 
            //if victory condition:
            ChangeState(GameState.Victory);

            //if defeat condition:
            ChangeState(GameState.Defeat);
        }
        
    }
    private void HandleVictory() 
    {
        
    }
    private void HandleDefeat() 
    {
        
    }

}
