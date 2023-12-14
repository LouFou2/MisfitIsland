using UnityEngine;
using UnityEngine.Events;

public class SceneChangeEvents : MonoBehaviour
{
    public UnityEvent OnIntroEnd = new UnityEvent();
    public UnityEvent OnGameOver = new UnityEvent();

    /*public IntEvent OnScoreUpdated = new IntEvent();

    // Custom event class for passing additional data
    [System.Serializable]
    public class IntEvent : UnityEvent<int> { }*/
}
