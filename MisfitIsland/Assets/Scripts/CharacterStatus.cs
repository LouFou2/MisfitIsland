using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    public bool isWolf;
    public bool isWolfRevealed;
    public bool isInfected = false;

    private Renderer cubeRenderer;

    private void Start()
    {
        cubeRenderer = GetComponent<Renderer>();
        cubeRenderer.material.SetColor("_CubeColor", Color.green);
    }
    void Update()
    {
        Color infectedColor = Color.yellow;
        Color wolfColor = Color.red;
        if (isInfected) UpdateCubeColor(infectedColor);
        if (isWolfRevealed) UpdateCubeColor(wolfColor);
    }
    void UpdateCubeColor(Color changedColor)
    {
        cubeRenderer.material.SetColor("_CubeColor", changedColor);
    }
}
