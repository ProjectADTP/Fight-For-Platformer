using UnityEngine;

[RequireComponent(typeof(Rotator))]
[RequireComponent(typeof(Stats))]
[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(Vision))]

public class Enemy : MonoBehaviour
{
    [SerializeField] private Vulnerability _vulnerability;

    private Stats _stats;
    private Patrol _patrol;
    private EnemyMover _mover;
    private Vision _vision;

    private void Awake()
    {
        _stats = GetComponent<Stats>();
        _patrol = GetComponent<Patrol>();
        _mover = GetComponent<EnemyMover>();
        _vision = GetComponent<Vision>();
    }

    private void OnEnable()
    {
        _vision.SeeTheTarget += GoToTarget;
        _vision.LostTheTarget += StartPatrol;

        _vulnerability.TakedDamage += TakeDamage;
    }

    private void OnDestroy()
    {
        _vision.LostTheTarget -= StartPatrol;
        _vision.SeeTheTarget -= GoToTarget;

        _vulnerability.TakedDamage -= TakeDamage;
    }

    private void Start()
    {
        StartPatrol();
    }

    private void Update()
    {
        if (_stats.Health <= 0)
            Destroy(gameObject);
    }

    private void StartPatrol()
    {
        _mover.GoToPoints(_patrol.GivePoints());
    }

    private void GoToTarget(Player target)
    {
        _mover.Initialise(target);
    }

    private void TakeDamage(int damage)
    {
        _stats.TakeDamage(damage);

        if (_stats.Health <= 0)
            Destroy(gameObject);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public int GetDamage()
    {
        return _stats.Damage;
    }
}