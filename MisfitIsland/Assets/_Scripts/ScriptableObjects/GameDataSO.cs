using UnityEngine;

[CreateAssetMenu(fileName = "NewGameData", menuName = "Game Data")]
public class GameDataSO : ScriptableObject
{
    public bool newGame;
    public int currentRound;
}