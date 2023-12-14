using UnityEngine;

[CreateAssetMenu(fileName = "NewStoryEventData", menuName = "Story Event Data")]
public class StoryEventSO : ScriptableObject
{
    public int storyEventIndex;
    public string eventTitle;
    public string eventDescription;
}
