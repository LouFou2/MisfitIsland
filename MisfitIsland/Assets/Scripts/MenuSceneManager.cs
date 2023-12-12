using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSceneManager : MonoBehaviour
{
    // Reference to your button in the Unity Editor
    public Button startButton;

    private void Start()
    {
        // Add a listener to the button click event
        startButton.onClick.AddListener(OnStartButtonClick);
    }

    // This method will be called when the button is clicked
    private void OnStartButtonClick()
    {
        // Load the main scene
        SceneManager.LoadScene("Event1Scene");
    }
}
