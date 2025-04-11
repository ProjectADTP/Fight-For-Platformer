using System.Collections;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    
    private Transform[] _points;
    private Transform _target;

    private Coroutine _movementCoroutine;

    private int _currentPoint;

    public void GoToPoints(Transform[] points)
    {
        StopActiveCoroutine();

        _currentPoint = 0;
        _points = points;

        _movementCoroutine = StartCoroutine(MoveToPoints());
    }

    public void Initialise(Player target)
    {
        StopActiveCoroutine();

        _target = target.transform;

        _movementCoroutine = StartCoroutine(MoveToTarget());
    }

    private IEnumerator MoveToPoints()
    {
        float distanceToTarget = 0.01f;

        while (_currentPoint < _points.Length)
        {
            if (transform.position.IsEnoughClose(_points[_currentPoint].transform.position, distanceToTarget))
                _currentPoint = ++_currentPoint % _points.Length;

            transform.position = Vector3.MoveTowards(transform.position, _points[_currentPoint].transform.position, _speed * Time.deltaTime);

            yield return null;
        }

        _currentPoint = 0;
    }

    private IEnumerator MoveToTarget()
    {
        while (_target != null)
        {
            Vector3 direction = (_target.position - transform.position).normalized;
            transform.Translate(direction * _speed * Time.deltaTime, Space.World);

            yield return null;
        }
    }

    private void StopActiveCoroutine()
    {
        if (_movementCoroutine != null)
        {
            StopCoroutine(_movementCoroutine);
            _movementCoroutine = null;
        }
    }
}
