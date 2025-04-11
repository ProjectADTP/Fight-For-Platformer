using System;
using UnityEngine;

public class Vision : MonoBehaviour
{
    [SerializeField] private float rayLength;

    private Vector3 _lastPosition;
    private Vector3 _movementDirection;
    private Vector3 _movementTargetDirection;

    private RaycastHit2D hit;

    private bool _isTargetSeed = false;

    public event Action<Player> SeeTheTarget;
    public event Action LostTheTarget;

    private void Awake()
    {
        _movementTargetDirection = _lastPosition;
        _lastPosition = transform.position;
    }

    private void FixedUpdate()
    {
        CalculateMovementDirection();
        PerformRaycast();
    }

    private void CalculateMovementDirection()
    {
        Vector3 currentPosition = transform.position;
        _movementDirection = (currentPosition - _lastPosition).normalized;
        _lastPosition = currentPosition;
    }

    private void CalculateTargetDirection(Player target)
    {
        Vector3 currentPosition = transform.position;
        _movementTargetDirection = (target.transform.position - currentPosition).normalized;
    }

    private void PerformRaycast()
    {
        if (_movementDirection == Vector3.zero) return;

        Vector3 startPosition = transform.position + (_movementDirection / 2);

        if (_isTargetSeed == false)
        {
            Debug.DrawRay(startPosition, _movementDirection * rayLength, Color.red);

            hit = Physics2D.Raycast(startPosition, _movementDirection, rayLength);
        }
        else 
        {
            Debug.DrawRay(startPosition, _movementTargetDirection * rayLength, Color.red);

            hit = Physics2D.Raycast(startPosition, _movementTargetDirection, rayLength);
        }

        if (hit.collider != null)
        {
            HandleRaycastHit(hit);
        }
        else
        {
            if (_isTargetSeed)
            {
                _isTargetSeed = false;
                LostTheTarget?.Invoke();
            }
        }
    }

    private void HandleRaycastHit(RaycastHit2D hit)
    {
        if (hit.collider.TryGetComponent<Player>(out Player target))
        {
            if (_isTargetSeed == false)
            {
                _isTargetSeed = true;
                SeeTheTarget?.Invoke(target);
            }

            CalculateTargetDirection(target);
        }
    }
}