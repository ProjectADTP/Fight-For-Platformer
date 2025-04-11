using UnityEngine;

public class CoinMover : MonoBehaviour
{
    [SerializeField] private float _speed = 4f; 
    [SerializeField] private float _distance = 0.25f;

    private Vector3 _startPosition;
    private float _timer;

    private void Awake()
    {
        _startPosition = transform.position; 
    }

    private void Update()
    {
        _timer += Time.deltaTime * _speed;

        transform.position = _startPosition + new Vector3(0, Mathf.Sin(_timer) * _distance, 0); 
    }
}
