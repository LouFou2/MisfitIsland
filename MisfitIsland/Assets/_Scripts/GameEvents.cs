using UnityEngine;
using UnityEngine.Events;

public class GameEvents : MonoBehaviour
{
    public UnityEvent OnGameStart = new UnityEvent();
    public UnityEvent OnVictory = new UnityEvent();
    public UnityEvent OnDefeat = new UnityEvent();

    /*public IntEvent OnScoreUpdated = new IntEvent();

    // Custom event class for passing additional data
    [System.Serializable]
    public class IntEvent : UnityEvent<int> { }*/
}