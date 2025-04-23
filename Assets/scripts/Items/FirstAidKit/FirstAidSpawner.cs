using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class FirstAidSpawner : MonoBehaviour
{
    [SerializeField] private List<SpawnerPoint> _spawnerPoints;
    [SerializeField] private FirstAid _firstAid;

    private void Start()
    {
        SpawnFirstAid();
    }

    private void DestroyFirstAid(FirstAid firstAid)
    {
        firstAid.Taked -= DestroyFirstAid;
        Destroy(firstAid.gameObject);
    }

    private void SpawnFirstAid()
    {
        FirstAid firstAid = Instantiate(_firstAid, _spawnerPoints[Random.Range(0, _spawnerPoints.Count - 1)].transform.position, transform.rotation);
        firstAid.Taked += DestroyFirstAid;
    }
}