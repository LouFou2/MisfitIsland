using System;
using UnityEngine;

public class GameManager : StaticInstance<GameManager>
{
    [SerializeField]
    private GameDataSO gameData;
    [SerializeField]
    private int totalNumberOfRounds = 6;

    // Listeners for Events:
    private void OnEnable()
    {
        SetupEvents.Instance.OnSetupEnd.AddListener(OnSetupEnd);
    }

    private void OnDisable()
    {
        SetupEvents.Instance.OnSetupEnd.RemoveListener(OnSetupEnd);
    }
    private void Start() ///***TEMPORARY
    {
        HandleSetup();
    }
    private void HandleSetup() 
    {
        GameEvents.Instance.OnSetupGame.Invoke();
    }
    private void OnSetupEnd()
    {
        HandleIntro();
    }
    private void HandleIntro() 
    {
        GameEvents.Instance.OnStartIntro.Invoke();
    }
    private void OnIntroEnd() 
    {
        HandleEvent(); 
    }
    private void HandleEvent()
    {
        int currentRound = gameData.currentRound;
        GameEvents.Instance.OnNextEventScene.Invoke(currentRound);
    }
    private void OnEventEnd()
    {
        int currentRound = gameData.currentRound;

        // in most cases (if not last round), next is Selection:
        if (currentRound < totalNumberOfRounds)
            HandleSelection();

        // the last round goes into Confrontation Scene:
        else
            HandleConfrontation();
    }
    private void HandleSelection() 
    {
        int currentRound = gameData.currentRound;
        GameEvents.Instance.OnNextSelectionScene.Invoke(currentRound);
    }
    private void OnSelectionEnd() 
    {
        HandleInterview(); 
    }
    private void HandleInterview() 
    {
        int currentRound = gameData.currentRound;
        GameEvents.Instance.OnNextInterviewScene.Invoke(currentRound);
    }
    private void OnInterviewEnd() 
    {
        gameData.currentRound += 1;
        HandleEvent(); 
    }
    private void HandleConfrontation() 
    {
        GameEvents.Instance.OnStartConfrontation.Invoke();
    }
    private void OnConfrontationEnd(bool isVictory) 
    {
        //if victory condition:
        if (isVictory)
            HandleVictory();

        //if defeat condition:
        else
            HandleDefeat();
    }
    private void HandleVictory() 
    {
        GameEvents.Instance.OnVictory.Invoke();
    }
    private void HandleDefeat() 
    {
        GameEvents.Instance.OnDefeat.Invoke();
    }

}
