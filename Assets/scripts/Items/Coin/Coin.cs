using System;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectibleItem
{
    public void Accept(IItemTaker taker) => taker.Take(this);

    public void Remove()
    {
        Destroy(gameObject);
    }
}