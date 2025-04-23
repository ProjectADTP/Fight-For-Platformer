using UnityEngine;

public class EntityDamage : MonoBehaviour
{
    [SerializeField] private int _damage;

    public int Damage { get; private set; }

    private void Awake()
    {
        Damage = _damage;
    }
}