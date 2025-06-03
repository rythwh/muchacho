using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Animal : MonoBehaviour
{
    private const int ForceMultiplier = 500;

    private Image _image;
    private Rigidbody2D _rigidbody;
    private Vector3 _position;
    [SerializeField] private Button button;

    public bool IsGood { get; private set; }

    public AnimalType Type { get; private set; } = AnimalType.None;

    private UnityEngine.Events.UnityAction _scanAction;

    public enum AnimalType
    {
        None = 0,
        Bear = 1,
        Boar = 2,
        Doe = 3,
        Fish = 4,
        Hare = 5,
        Horse = 6,
        Worm = 7
    }

    public readonly HashSet<AnimalType> GoodAnimals = new()
    {
        AnimalType.Bear,
        AnimalType.Boar,
        AnimalType.Doe,
        AnimalType.Hare
    };

    public readonly HashSet<AnimalType> BadAnimals = new()
    {
        AnimalType.Fish,
        AnimalType.Horse,
        AnimalType.Worm
    };

    public void Initialize(AnimalType type, Sprite sprite)
    {
        Type = type;
        IsGood = GoodAnimals.Contains(type);
        _image.sprite = sprite;
    }

    private void Awake()
    {
        _scanAction = () => GameManager.Instance.ScanAnimal(this);
        button.onClick?.AddListener(_scanAction);
        _image = GetComponent<Image>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _position = transform.position;
        // Reset();
    }

    private void OnDestroy()
    {
        button.onClick?.RemoveListener(_scanAction);
    }

    private void Update()
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

    private void Shoot(Vector2 direction)
    {
        var force = direction.normalized * (ForceMultiplier * Random.Range(1f, 2f));
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

    public void SpawnFromBottomAndShoot(RectTransform canvasRect)
    {
        Vector2 canvasSize = canvasRect.sizeDelta;

        // Random X along bottom
        var spawnX = Random.Range(0f, canvasSize.x);
        var spawnY = -100f; // Just below bottom edge

        var spawnPos = new Vector2(spawnX, spawnY);
        _position = spawnPos;

        var rect = GetComponent<RectTransform>();
        rect.anchoredPosition = spawnPos;

        // Direction toward center of canvas
        var center = canvasSize / 2f;
        var direction = (center - spawnPos).normalized;

        // Slight spread for variety
        var angleOffset = Random.Range(-15f, 15f);
        direction = Quaternion.Euler(0, 0, angleOffset) * direction;

        Shoot(direction);
    }
}