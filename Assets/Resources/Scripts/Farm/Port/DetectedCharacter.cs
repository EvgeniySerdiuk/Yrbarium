using System;
using UnityEngine;

public class DetectedCharacter : MonoBehaviour
{
    public event Action<bool> IsCharacterHere;

    private void OnTriggerEnter()
    {
        IsCharacterHere?.Invoke(true);
    }

    private void OnTriggerExit()
    {
        IsCharacterHere?.Invoke(false);
    }
}
