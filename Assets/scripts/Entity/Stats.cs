using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _damage;

    private int _minHealth = 0;
    private int _maxHealth = 100;

    public int Damage { get; private set; }
    public int Health { get; private set; }

    private void Awake()
    {
        Damage = _damage;
        Health = _health;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }

    public void RestoreHealth(int health)
    {
        if (health != 0)
            Health = Mathf.Clamp(health + Health, _minHealth, _maxHealth);
        else
            Health = _maxHealth;
    }
}