using UnityEngine;

public class TraitorBehaviour : MonoBehaviour
{
    [Range(0, 100)] 
    public float infectSuccessChance;
    public bool infectSuccess;

    private void Start()
    {
        infectSuccessChance = 10f;
    }
    void Update()
    {
        // check success of infecting another character
        float infectCheck = Random.Range(0.0f, 100f);
        infectSuccess = (infectCheck <= infectSuccessChance) ? true : false;
    }
}
