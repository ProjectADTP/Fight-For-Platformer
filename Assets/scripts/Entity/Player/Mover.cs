using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Rotator))]
public class Mover : MonoBehaviour
{
    private float _moveSpeed = 4f;
    private float _jumpForce = 700f;

    private Rigidbody2D _rigidbody;
    private Rotator _rotation;

    private void Awake()
    {
        _rotation = GetComponent<Rotator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
        _rigidbody.AddForce(new Vector2(0, _jumpForce));
    }

    public void Move(float direction)
    {
        transform.Translate(new Vector3(direction, 0, 0) * _moveSpeed * Time.deltaTime, Space.World);
        _rotation.Rotate(direction);
    }
}
