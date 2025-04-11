using System;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private int _enteredCount = 0;

    public bool IsGround { get; private set; }
    public bool IsDead { get; private set; }

    public event Action<int, Vector3> TakedDamage;
    public event Action<int> TakedHealth;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ground>(out _))
        {
            _enteredCount++;
            IsGround = true;
        }

        if (collision.gameObject.TryGetComponent<DeadZone>(out _))
            IsDead = true;

        if (collision.gameObject.TryGetComponent<Coin>(out Coin coin))
            coin.Remove();

        if (collision.gameObject.TryGetComponent<FirstAid>(out FirstAid firstAid))
        {
            TakedHealth?.Invoke(firstAid.Health);
            firstAid.Remove();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
            TakedDamage?.Invoke(enemy.GetComponent<Stats>().Damage, enemy.transform.position);
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