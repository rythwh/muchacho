using UnityEngine;

public class Temp : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private const int ForceMultiplier = 500;
    private Vector3 _position;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _position = transform.position;
        Reset();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            Shoot();
            Debug.Log($"x {_rigidbody.linearVelocityX}, {_rigidbody.linearVelocityY}");
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            Reset();
        }
    }
    
    private void Shoot()
    {
        var force = new Vector2(1.5f, 2.5f) * (ForceMultiplier * Random.Range(1f, 2f));
        _rigidbody.AddForce(force, ForceMode2D.Impulse);
    }

    private void Reset()
    {
        transform.position = _position;
        _rigidbody.linearVelocity = Vector2.zero;
        _rigidbody.linearDamping = 0.05f;
        _rigidbody.gravityScale = 250f;
        Shoot();
    }
}
