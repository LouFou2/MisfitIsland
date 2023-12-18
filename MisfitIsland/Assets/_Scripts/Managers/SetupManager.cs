using System.Collections.Generic;
using UnityEngine;

public class SetupManager : StaticInstance<SetupManager>
{
    [SerializeField]
    private GameDataSO gameData;
    [SerializeField]
    private GameObject[] _characters;
    [SerializeField]
    private CharacterDataSO[] _characterData;
    [SerializeField]
    private AllCharactersDataSO _allCharactersData;

    private void OnEnable()
    {
        GameEvents.Instance.OnSetupGame.AddListener(SetupGame);
    }
    private void OnDisable()
    {
        GameEvents.Instance.OnSetupGame.RemoveListener(SetupGame);
    }
    void SetupGame()
    {
        ResetGameData();
        ResetCharacterData();
        //other resets...
        SetupWolfCharacter();
        EndSetup();
    }
    void ResetGameData() 
    {
        gameData.newGame = true;
        gameData.currentRound = 0;
    }
    void ResetCharacterData()
    {
        // Initialize the "All Characters" array with the correct length
        _allCharactersData.characterDataSOs = new CharacterDataSO[_characters.Length];

        for (int i = 0; i < _characters.Length; i++)
        {
            _characterData[i].isWolf = false;
            _characterData[i].isWolfRevealed = false;
            _characterData[i].proOrAntiSpectrum = 0f;
            _characterData[i].isInfected = false;
            _characterData[i].isInTraining = false;

            _allCharactersData.characterDataSOs[i] = _characterData[i];
        }
    }
    void SetupWolfCharacter() 
    {
        int wolfIndex = Random.Range(0, _characters.Length); // the wolfindex is the chosen character gameObject
        int wolfProfile = Random.Range(0, _characters.Length); // the wolfprofile index is the chosen scriptable object

        CharacterDataSO wolfData = _characterData[wolfProfile];
        wolfData.isWolf = true;

        //_characters[wolfIndex].GetComponent<CharacterBehaviour>().SetupCharacterProfile(wolfData);
        SetupEvents.Instance.OnCharacterSetup.Invoke(wolfIndex, wolfData);

        SetupCharacterProfiles(wolfIndex, wolfProfile);
    }
    void SetupCharacterProfiles( int wolfIndex, int wolfProfile) // this method is called from the SetupWolf method
    {
        List<CharacterDataSO> availableProfiles = new List<CharacterDataSO>(_characterData);
        // Remove the wolf's profile from the available profiles
        availableProfiles.RemoveAt(wolfProfile);

        for (int i = 0; i < _characters.Length; i++)
        {
            if (i != wolfIndex) // ignore the wolf Index
            {
                int randomIndex = Random.Range(0, availableProfiles.Count);
                CharacterDataSO randomProfile = availableProfiles[randomIndex];

                // Remove the chosen profile from the list to avoid duplicates
                availableProfiles.RemoveAt(randomIndex);

                //_characters[i].GetComponent<CharacterBehaviour>().SetupCharacterProfile(randomProfile);
                SetupEvents.Instance.OnCharacterSetup.Invoke(i, randomProfile);
            }
        }
    }
    void EndSetup()
    {
        // tell SetupEvents that Setup is Done
        SetupEvents.Instance.OnSetupEnd.Invoke();
    }
}
