using UnityEngine;

public class NarrationManager : MonoBehaviour
{
    [SerializeField]
    private StoryEventSO[] storyEventData;
    private int[] _storyEventIndex;

    void Start()
    {
        // Set Up indexes for story events, and make the indexes reflect in the scriptable objects
        _storyEventIndex = new int[storyEventData.Length];

        for (int i = 0; i < storyEventData.Length; i++) 
        {
            _storyEventIndex[i] = i;
            storyEventData[i].storyEventIndex = i; 
        }
    }
    void OnNewStoryEvent(int _storyEventIndex) 
    {
        string titleText = storyEventData[_storyEventIndex].eventTitle;
        string descriptionText = storyEventData[_storyEventIndex].eventDescription;
        HandleNarration(titleText, descriptionText);

    }
    void HandleNarration(string title, string description)
    {
        // pass title and description to UI manager (or via Event Manager)
        // so UI Manager can: UIManager.StartCoroutine(DisplayText( ));
    }
}
