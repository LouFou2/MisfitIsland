using UnityEngine;

public class WolfBehaviour : MonoBehaviour
{
    [Range(0,100)]
    public float infectSuccessChance;
    public bool infectSuccess;
    void Update()
    {
        // check success of infecting another character
        float infectCheck = Random.Range(0.0f, 100f);
        infectSuccess = (infectCheck <= infectSuccessChance) ? true : false;
    }
}
