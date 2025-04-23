using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _health;

    private int _minHealth = 0;
    private int _maxHealth = 100;

    public int Health { get; private set; }

    private void Awake()
    {
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
