using System;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private int _enteredCount = 0;

    public bool IsGround { get; private set; }
    public bool IsDead { get; private set; }

    public event Action<Enemy> TakedDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ground>(out _))
        {
            _enteredCount++;
            IsGround = true;
        }

        if (collision.gameObject.TryGetComponent<DeadZone>(out _))
            IsDead = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
            TakedDamage?.Invoke(enemy);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ground>(out _))
            _enteredCount--;

        if (collision.gameObject.TryGetComponent<DeadZone>(out _) || collision.gameObject.TryGetComponent<Enemy>(out _))
            IsDead = false;

        if (_enteredCount <= 0)
        { 
            _enteredCount = 0;
            IsGround = false;
        }
    }
}