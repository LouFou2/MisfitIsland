using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    IEnumerator DisplayText(TextMeshProUGUI textObject, string textInput, float pauseDelay) 
    {
        textObject.text = textInput;
        yield return new WaitForSeconds(pauseDelay);
    }
}
