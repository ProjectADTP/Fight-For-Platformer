using System;
using UnityEngine;

public class FirstAid : MonoBehaviour
{
    [SerializeField] private int _health;

    public event Action<FirstAid> Taked;

    public int Health { get; private set; }

    private void Awake()
    {
        Health = _health;
    }

    public void Remove()
    {
        Taked?.Invoke(this);
    }
}
