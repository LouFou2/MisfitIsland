using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    public bool isWolf = false;
    public bool isWolfRevealed;
    [Range(-100f,100f)]
    public float proOrAntiSpectrum = 0f;
    public bool isInfected = false;
    public bool isInTraining = false;

    [Range(0, 100)] public float infectSuccessChance;

    private Renderer cubeRenderer; // cube colours are only for testing, remove later

    private void Start()
    {
        proOrAntiSpectrum = 0f;
        infectSuccessChance = 0f; // find appropriate initial value

        cubeRenderer = GetComponent<Renderer>();
        cubeRenderer.material.SetColor("_CubeColor", Color.green);
    }
    void Update()
    {
        Color neutralColor = Color.green;
        Color infectedColor = Color.yellow;
        Color wolfColor = Color.red;
        Color inTrainingColor = Color.cyan;

        isInfected = (proOrAntiSpectrum < 0) ? true : false;

        if (isInTraining) UpdateCubeColor(inTrainingColor);
        if (!isInfected && !isInTraining) UpdateCubeColor(neutralColor);
        if (isInfected && !isInTraining) UpdateCubeColor(infectedColor);
        if (isWolfRevealed) UpdateCubeColor(wolfColor);
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
