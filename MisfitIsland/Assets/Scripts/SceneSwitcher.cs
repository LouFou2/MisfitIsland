using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    private static SceneSwitcher instance;

    public static SceneSwitcher Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("SceneSwitcher").AddComponent<SceneSwitcher>();
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        // Subscribe to events

        /*e.g:
        EventSystem.OnVictory.AddListener(SwitchToVictoryScene);
        EventSystem.OnDefeat.AddListener(SwitchToDefeatScene);*/

        // Subscribe to other events as needed
    }

    private void OnDisable()
    {
        // Unsubscribe from events

        /*EventSystem.OnVictory.RemoveListener(SwitchToVictoryScene);
        EventSystem.OnDefeat.RemoveListener(SwitchToDefeatScene);*/

        // Unsubscribe from other events as needed
    }

    public void SwitchToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void SwitchToIntroScene()
    {
        SceneManager.LoadScene("IntroScene");
    }

    public void SwitchToEventScene(int eventNumber)
    {
        SceneManager.LoadScene("EventScene" + eventNumber);
    }
    public void SwitchToSelectionScene(int selectionNumber)
    {
        SceneManager.LoadScene("SelectionScene" + selectionNumber);
    }

    public void SwitchToVictoryScene()
    {
        SceneManager.LoadScene("VictoryScene");
    }

    public void SwitchToDefeatScene()
    {
        SceneManager.LoadScene("DefeatScene");
    }

    // ... other scene switching methods ...
}
