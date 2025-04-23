using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Rotator))]
public class Mover : MonoBehaviour
{
    private float _moveSpeed = 4f;

    private Rotator _rotation;

    private void Awake()
    {
        _rotation = GetComponent<Rotator>();
    }

    public void Move(float direction)
    {
        transform.Translate(new Vector3(direction, 0, 0) * _moveSpeed * Time.deltaTime, Space.World);
        _rotation.Rotate(direction);
    }
}
