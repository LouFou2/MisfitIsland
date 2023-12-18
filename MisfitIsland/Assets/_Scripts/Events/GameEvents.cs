using UnityEngine;
using UnityEngine.Events;

public class GameEvents : StaticInstance<GameEvents>
{
    // Events that other managers will subscribe to:
    public UnityEvent OnSetupGame = new UnityEvent();

    public UnityEvent OnStartIntro = new UnityEvent();
    public IntEvent OnNextEventScene = new IntEvent();
    public IntEvent OnNextSelectionScene = new IntEvent();
    public IntEvent OnNextInterviewScene = new IntEvent();
    public UnityEvent OnStartConfrontation = new UnityEvent();

    public UnityEvent OnVictory = new UnityEvent();
    public UnityEvent OnDefeat = new UnityEvent();

    

    //Custom event class for passing additional data
    [System.Serializable]
    public class IntEvent : UnityEvent<int> { }
}
