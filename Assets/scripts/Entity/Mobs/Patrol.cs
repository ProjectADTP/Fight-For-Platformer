using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] private Transform[] _points;

    public Transform[] GivePoints()
    { 
        return _points;
    }
}