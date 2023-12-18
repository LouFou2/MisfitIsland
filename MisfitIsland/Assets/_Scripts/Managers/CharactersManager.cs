using UnityEngine;

public class CharactersManager : StaticInstance<CharactersManager>
{
    [SerializeField]
    private AllCharactersDataSO _allCharactersData;
    [SerializeField]
    private CharacterBehaviour[] _characterBehaviour;

    private void OnEnable()
    {
        SetupEvents.Instance.OnCharacterSetup.AddListener(SetCharacterData);
    }
    private void OnDisable()
    {
        SetupEvents.Instance.OnCharacterSetup.RemoveListener(SetCharacterData);
    }
    void SetCharacterData(int characterIndex, CharacterDataSO characterData)
    {
        _allCharactersData.characterDataSOs[characterIndex] = characterData; // storing scriptable objects by index order in Parent Scriptable object
        _characterBehaviour[characterIndex].characterData = characterData; // also assigning it to individual objects script components
    }
}
