using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private List<SpawnerPoint> _spawnerPoints;
    [SerializeField] private Coin _coin;

    private void Start()
    {
        SpawnCoins();
    }

    private void DestroyCoin(Coin coin)
    {
        coin.Taked -= DestroyCoin;
        Destroy(coin.gameObject);
    }

    private void SpawnCoins()
    {
        foreach (SpawnerPoint point in _spawnerPoints)
        {
            Coin coin = Instantiate(_coin, point.transform.position, point.transform.rotation);
            coin.Taked += DestroyCoin;
        }
    }
}
