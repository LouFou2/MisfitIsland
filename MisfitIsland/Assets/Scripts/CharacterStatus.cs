using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    [SerializeField]
    private CharacterDataSO characterData;

    public bool isWolf = false;
    public bool isWolfRevealed;
    [Range(-100f, 100f)]
    public float proOrAntiSpectrum = 0f;
    public bool isInfected = false;
    public bool isInTraining = false;

    [Range(0, 100)] public float infectSuccessChance;

    private Renderer cubeRenderer; // cubes and cube colours are only for prototype testing, remove later

    private void Start()
    {
        proOrAntiSpectrum = 0f;
        infectSuccessChance = 0f; // find appropriate initial value

        cubeRenderer = GetComponent<Renderer>();
        if (cubeRenderer != null )
            cubeRenderer.material.SetColor("_CubeColor", Color.green);
    }
    void Update()
    {
        UpdateCharacterData();

        Color neutralColor = Color.green;
        Color infectedColor = Color.yellow;
        Color wolfColor = Color.red;
        Color inTrainingColor = Color.cyan;

        isInfected = (proOrAntiSpectrum < 0) ? true : false;

        if (cubeRenderer != null) 
        {
            if (isInTraining) UpdateCubeColor(inTrainingColor);
            if (!isInfected && !isInTraining) UpdateCubeColor(neutralColor);
            if (isInfected && !isInTraining) UpdateCubeColor(infectedColor);
            if (isWolfRevealed) UpdateCubeColor(wolfColor);
        }
        
    }

    private void UpdateCharacterData()
    {
        characterData.isWolf = isWolf;
        characterData.isWolfRevealed = isWolfRevealed;
        characterData.proOrAntiSpectrum = proOrAntiSpectrum;
        characterData.isInfected = isInfected;
        characterData.isInTraining = isInTraining;
        characterData.infectSuccessChance = infectSuccessChance;
    }

    void UpdateCubeColor(Color changedColor)
    {
        cubeRenderer.material.SetColor("_CubeColor", changedColor);
    }
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
