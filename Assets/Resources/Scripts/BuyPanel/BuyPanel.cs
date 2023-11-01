using System;
using UnityEngine;

public class BuyPanel : MonoBehaviour
{
    public event Action IsBuy;

    public void OnMouseDown()
    {       
        IsBuy?.Invoke();
    }
}
