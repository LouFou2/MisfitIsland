using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    [SerializeField]
    public CharacterDataSO characterData;

    public bool isWolf = false;
    public bool isWolfRevealed;
    [Range(-100f, 100f)]
    public float proOrAntiSpectrum = 0f;
    public bool isInfected = false;
    public bool isInTraining = false;

    [Range(0, 100)] public float infectSuccessChance;

    // Method to set the character profile
    public void SetupCharacterProfile(CharacterDataSO data)
    {
        characterData = data;
    }

    private void Start()
    {
        proOrAntiSpectrum = 0f;
        infectSuccessChance = 0f; // find appropriate initial value
    }
    void Update()
    {
        //UpdateCharacterData();

        isInfected = (proOrAntiSpectrum < 0) ? true : false;
    }

    /*private void UpdateCharacterData()
    {
        characterData.isWolf = isWolf;
        characterData.isWolfRevealed = isWolfRevealed;
        characterData.proOrAntiSpectrum = proOrAntiSpectrum;
        characterData.isInfected = isInfected;
        characterData.isInTraining = isInTraining;
        characterData.infectSuccessChance = infectSuccessChance;
    }*/
    public void UpdateProOrAntiStat(float proOrAntiChange) 
    {
        proOrAntiSpectrum += proOrAntiChange;
    }
    public void ModifyInfectionChance(float infectionChanceIncrease) 
    {
        infectSuccessChance += infectionChanceIncrease; // TODO I would like the chanceincrease to factor in the "pro or anti" stat
    }
    public bool InfectSuccess() 
    {
        // check success of infecting another character
        float infectCheck = Random.Range(0.0f, 100f);
        if (infectCheck <= infectSuccessChance) return true;
        else return false;
    }
}
