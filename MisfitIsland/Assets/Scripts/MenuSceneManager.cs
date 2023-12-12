using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSceneManager : MonoBehaviour
{
    // Menu Buttons
    public Button startButton;
    // can add more buttons when it is more fleshed out and set up

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
        SceneSwitcher.Instance.SwitchToIntroScene();
    }
}
