using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    
    private void OnEnable()
    {
        // Subscribe to events
    }

    private void OnDisable()
    {
        // Unsubscribe from events
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
    public void SwitchToInterviewScene(int interviewNumber)
    {
        SceneManager.LoadScene("InterviewScene" + interviewNumber);
    }

    public void SwitchToVictoryScene()
    {
        SceneManager.LoadScene("VictoryScene");
    }

    public void SwitchToDefeatScene()
    {
        SceneManager.LoadScene("DefeatScene");
    }
}
