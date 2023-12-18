using UnityEngine;

[CreateAssetMenu(fileName = "NewAllCharsData", menuName = "All Characters Data")]
public class AllCharactersDataSO : ScriptableObject
{
    public CharacterDataSO[] characterDataSOs;
}
