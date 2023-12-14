using System.Collections.Generic;
using UnityEngine;

public class SetupManager : MonoBehaviour
{
    [SerializeField]
    private GameDataSO gameData;
    [SerializeField]
    private GameObject[] _characters;
    [SerializeField]
    private CharacterDataSO[] _characterData;

    void Start()
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
        for (int i = 0; i < _characters.Length; i++)
        {
            _characterData[i].isWolf = false;
            _characterData[i].isWolfRevealed = false;
            _characterData[i].proOrAntiSpectrum = 0f;
            _characterData[i].isInfected = false;
            _characterData[i].isInTraining = false;
        }
    }
    void SetupWolfCharacter() 
    {
        int wolfIndex = Random.Range(0, _characters.Length);
        int wolfProfile = Random.Range(0, _characters.Length);

        CharacterDataSO wolfData = _characterData[wolfProfile];
        wolfData.isWolf = true;

        _characters[wolfIndex].GetComponent<CharacterStatus>().SetupCharacterProfile(wolfData);

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

                _characters[i].GetComponent<CharacterStatus>().SetupCharacterProfile(randomProfile);
            }
        }
    }
    void EndSetup() 
    {
        // tell GameEvents that Setup is Done
        GameEvents.Instance.OnSetupComplete.Invoke();
    }
}
