using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterData", menuName = "Character Data")]
public class CharacterDataSO : ScriptableObject
{
    public bool isWolf = false;
    public bool isWolfRevealed;
    [Range(-100f, 100f)]
    public float proOrAntiSpectrum = 0f;
    public bool isInfected = false;
    public bool isInTraining = false;

    [Range(0, 100)] public float infectSuccessChance;
}
