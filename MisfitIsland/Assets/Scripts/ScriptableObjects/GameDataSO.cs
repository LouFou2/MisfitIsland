using UnityEngine;

[CreateAssetMenu(fileName = "NewGameData", menuName = "Game Data")]
public class GameDataSO : ScriptableObject
{
    public int numberOfRounds;
    public float playerInfluence;
    public int wolfIndex;
}