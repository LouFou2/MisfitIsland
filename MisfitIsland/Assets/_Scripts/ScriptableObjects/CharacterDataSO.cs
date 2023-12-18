using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterData", menuName = "Character Data")]
public class CharacterDataSO : ScriptableObject
{
    public bool isWolf = false;
    public bool isWolfRevealed = false;
    [Range(-100f, 100f)]
    public float proOrAntiSpectrum = 0f;
    public bool isInfected = false;
    public bool isInTraining = false;

    [Range(0, 100)] public float infectSuccessChance;

    [Header("Persona")]
    public PersonaType persona = PersonaType.None;

    public enum PersonaType
    {
        None,
        SuckUp,
        TwoFace,
        WholeSome,
        AirHead,
        ParaNoiac,
        TryHard
        // Add more persona types as needed
    }
}
