﻿using UnityEngine;

public class Player : MonoBehaviour
{
    private Stats _stats;
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
        _stats = GetComponent<Stats>();
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
        _collisionDetector.TakedHealth += RestoreHealth;
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

    private void RestoreHealth(int health)
    {
        _stats.RestoreHealth(health);

        _collisionDetector.TakedHealth -= RestoreHealth;
    }

    private void TakeDamage(int damage,Vector3 enemyDirection)
    {
        _stats.TakeDamage(damage);

        if (_stats.Health <= 0)
            Respawn();
        else
            _knockback.ApplyKnockback(enemyDirection);
    }

    private void Respawn()
    {
        _teleporter.TeleportToStart(_basePosition);

        _stats.RestoreHealth(0);
    }
}
