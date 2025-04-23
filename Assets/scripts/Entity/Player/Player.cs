using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(CollisionDetector))]
[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(Teleporter))]
[RequireComponent(typeof(Knockback))]
[RequireComponent(typeof(Jumper))]
[RequireComponent(typeof(ItemPickupDetector))]

public class Player : MonoBehaviour
{
    private PlayerHealth _health;
    private CollisionDetector _collisionDetector;
    private InputReader _inputReader;
    private Mover _mover;
    private PlayerAnimator _playerAnimator;
    private Teleporter _teleporter;
    private Knockback _knockback;
    private Jumper _jumper;

    private Vector3 _basePosition;

    private void Awake()
    {
        _health = GetComponent<PlayerHealth>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _inputReader = GetComponent<InputReader>();
        _mover = GetComponent<Mover>();
        _jumper = GetComponent<Jumper>();
        _collisionDetector = GetComponent<CollisionDetector>();
        _teleporter = GetComponent<Teleporter>();
        _knockback = GetComponent<Knockback>();

        _basePosition = transform.position;
    }

    private void OnEnable()
    {
        _collisionDetector.TakedDamage += TakeDamage;
    }

    private void OnDisable()
    {
        _collisionDetector.TakedDamage -= TakeDamage;
    }

    private void FixedUpdate()
    {
        if (_inputReader.Direction != 0)
            _mover.Move(_inputReader.Direction);

        if (_inputReader.GetIsJump() && _collisionDetector.IsGround)
            _jumper.Jump();
    }

    private void Update()
    {
        _playerAnimator.SetIsMoving(_inputReader.Direction != 0);
        _playerAnimator.SetIsGrounded(_collisionDetector.IsGround);

        if (_collisionDetector.IsDead)
            Respawn();
    }

    private void TakeDamage(Enemy enemy)
    {
        _health.TakeDamage(enemy.GetDamage());

        if (_health.Health <= 0)
            Respawn();
        else
            _knockback.ApplyKnockback(enemy.GetPosition());
    }

    private void Respawn()
    {
        _teleporter.TeleportToStart(_basePosition);

        _health.RestoreHealth(0);
    }
}
