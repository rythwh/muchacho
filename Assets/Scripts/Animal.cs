using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Animal : MonoBehaviour
{
    private const int ForceMultiplier = 1500;

    private Image _image;
    private Rigidbody2D _rigidbody;
    [SerializeField] private EventTrigger eventTrigger;

    public bool IsGood { get; private set; }

    public AnimalType Type { get; private set; } = AnimalType.None;

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
        _image = GetComponent<Image>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void ScanAnimal()
    {
        GameManager.Instance.ScanAnimal(this);
    }

    private void Shoot(Vector2 direction)
    {
        // Scale direction directly with a vertical boost factor
        Vector2 biasedDirection = Vector2.Lerp(direction, Vector2.up, 0.25f).normalized;

        // Slightly stronger random force range
        float forceStrength = ForceMultiplier * Random.Range(1.2f, 1.6f);

        Vector2 force = biasedDirection * forceStrength;
        _rigidbody.AddForce(force, ForceMode2D.Impulse);
    }

    public void SpawnFromBottomAndShoot(RectTransform canvasRect)
    {
        RectTransform rect = GetComponent<RectTransform>();
        Vector2 canvasSize = canvasRect.rect.size;

        // Spawn just below the canvas at a random horizontal position
        float spawnX = Random.Range(-canvasSize.x / 2f, canvasSize.x / 2f);
        float spawnY = -canvasSize.y / 2f - 100f;
        Vector2 spawnPos = new Vector2(spawnX, spawnY);

        rect.localPosition = spawnPos;

        // Target a point above the canvas center
        Vector2 targetPos = new Vector2(0f, canvasSize.y / 2f + 200f); // Above center

        // Direction from spawn to target
        Vector2 direction = (targetPos - spawnPos).normalized;

        // Add a bit of random angle variation
        float angleOffset = Random.Range(-10f, 10f);
        direction = Quaternion.Euler(0, 0, angleOffset) * direction;

        Shoot(direction);
    }

    public void Update()
    {
        if (transform.position.y < -100)
        {
            if (IsGood)
            {
                // Events.AddScore(-1);
                Events.OnLifeLost();
            }
            
            Destroy(gameObject);
        }
    }
}