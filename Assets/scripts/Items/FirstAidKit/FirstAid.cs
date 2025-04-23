using System;
using UnityEngine;

public class FirstAid : MonoBehaviour, ICollectibleItem
{
    [SerializeField] private int _healAmount = 30;
    public int HealAmount => _healAmount;

    public void Accept(IItemTaker taker) => taker.Take(this);

    public void Remove()
    {
        Destroy(gameObject);
    }
}
