using UnityEngine;

[CreateAssetMenu(fileName = "NewGameData", menuName = "Game Data")]
public class GameDataSO : ScriptableObject
{
    public GameManager.GameState gameState;
    public int numberOfRounds;
    public float playerInfluence;
}