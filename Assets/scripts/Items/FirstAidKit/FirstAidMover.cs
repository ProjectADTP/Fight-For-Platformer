using UnityEngine;
using System.Collections;

public class FirstAidMover : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;
    [SerializeField] private float _distance = 0.1f;
    [SerializeField] private float _pulseSpeed = 3f;
    [SerializeField] private float _maxScale = 1.2f;

    private Vector3 _originalScale;
    private Vector3 _startPosition;
    private float _timer;

    private void Awake()
    {
        _originalScale = transform.localScale;
        _startPosition = transform.position;
    }

    private void Start()
    {
        StartCoroutine(Pulse());
    }

    private void Update()
    {
        _timer += Time.deltaTime * _speed;

        transform.position = _startPosition + new Vector3(0, Mathf.Sin(_timer) * _distance, 0);
    }

    private IEnumerator Pulse()
    {
        while (enabled)
        {
            while (transform.localScale.x < _maxScale)
            {
                transform.localScale += Vector3.one * _pulseSpeed * Time.deltaTime;

                yield return null;
            }

            while (transform.localScale.x > _originalScale.x)
            {
                transform.localScale -= Vector3.one * _pulseSpeed * Time.deltaTime;

                yield return null;
            }

            transform.localScale = _originalScale;
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
