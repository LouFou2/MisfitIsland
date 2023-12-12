using System.Collections.Generic;
using UnityEngine;

public class SetupManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _characters;
    [SerializeField]
    private CharacterDataSO[] _characterData;
    void Start()
    {
        SetupWolfCharacter();
        SetupEventScenarios();
    }

    void SetupWolfCharacter() 
    {
        // Clear all indexes of "isWolf"
        for(int i = 0; i < _characters.Length; i++) 
        {
            _characterData[i].isWolf = false;
        }
        int wolfIndex = Random.Range(0, _characters.Length);
        int wolfProfile = Random.Range(0, _characters.Length);
        CharacterDataSO wolfData = _characterData[wolfProfile];
        wolfData.isWolf = true;
        _characters[wolfIndex].GetComponent<CharacterStatus>().SetupCharacterProfile(wolfData);

        SetupCharacterProfiles(wolfIndex, wolfProfile);
    }
    void SetupCharacterProfiles( int wolfIndex, int wolfProfile ) 
    {
        List<CharacterDataSO> availableProfiles = new List<CharacterDataSO>(_characterData);
        // Remove the wolf's profile from the available profiles
        availableProfiles.RemoveAt(wolfProfile);

        for (int i = 0; i < _characters.Length; i++) 
        {
            if (i != wolfIndex) 
            {
                int randomIndex = Random.Range(0, availableProfiles.Count);
                CharacterDataSO randomProfile = availableProfiles[randomIndex];

                // Remove the chosen profile from the list to avoid duplicates
                availableProfiles.RemoveAt(randomIndex);
                _characters[i].GetComponent<CharacterStatus>().SetupCharacterProfile(randomProfile);
            }
        }

    }
    void SetupEventScenarios() 
    {
        // Set up randomised events for game rounds 
    }
}
