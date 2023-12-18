using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSceneManager : MonoBehaviour
{
    // Menu Buttons
    public Button startButton;
    // can add more buttons when it is more fleshed out and set up
    // e.g. public Button exitButton;

    private void OnEnable()
    {
        startButton.onClick.AddListener(OnStartButtonClick);
    }
    private void OnDisable()
    {
        startButton.onClick.RemoveListener(OnStartButtonClick);
    }

    private void OnStartButtonClick()
    {
        SceneManager.LoadScene("IntroScene");
    }
}
