using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public event Action<Coin> Taked;

    public void Remove()
    {
        Taked?.Invoke(this);
    }
}