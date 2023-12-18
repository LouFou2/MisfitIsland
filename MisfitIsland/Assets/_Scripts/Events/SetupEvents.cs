using UnityEngine;
using UnityEngine.Events;

public class SetupEvents : StaticInstance<SetupEvents>
{
    public UnityEvent OnSetupEnd = new UnityEvent();
    public CharacterSetupEvent OnCharacterSetup = new CharacterSetupEvent();
    
    [System.Serializable]
    public class CharacterSetupEvent : UnityEvent<int, CharacterDataSO> { }
}
