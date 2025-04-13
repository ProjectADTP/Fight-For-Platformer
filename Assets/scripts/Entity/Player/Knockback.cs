using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Renderer))]
public class Knockback : MonoBehaviour
{
    [Header("Visual Effects")]
    [SerializeField] private Color flashColor = Color.red;
    [SerializeField] private float knockbackForce = 200f;

    private Rigidbody2D _rigidbody;
    private Renderer _renderer;
    private Color _originalColor;
    private Coroutine _flashRoutine;
    private WaitForSeconds _wait;

    private float flashDuration = 5f;

    private void Awake()
    {
        _wait = new WaitForSeconds(0.1f);

        _rigidbody = GetComponent<Rigidbody2D>();
        _renderer = GetComponentInChildren<Renderer>();

        if (_renderer != null) 
            _originalColor = _renderer.material.color;
    }

    public void ApplyKnockback(Vector3 enemyPosition)
    {
        Vector3 direction = (enemyPosition - transform.position).normalized;

        if (direction.x > 0)
            direction = - direction;

        _rigidbody.velocity = Vector2.zero;
        _rigidbody.AddForce(new Vector2(direction.x * knockbackForce, 0));

        if (_renderer != null)
        {
            if (_flashRoutine != null)
            { 
                StopCoroutine(_flashRoutine);
                _flashRoutine = StartCoroutine(FlashEffect());
            }
        }
    }

    private IEnumerator FlashEffect()
    {
        float elapsedTime = 0f;
        bool isFlashing = true;

        while (elapsedTime < flashDuration)
        {
            _renderer.material.color = isFlashing ? flashColor : _originalColor;
            isFlashing = !isFlashing;

            yield return _wait;

            elapsedTime ++;
        }

        _renderer.material.color = _originalColor;
        _flashRoutine = null;
    }
}