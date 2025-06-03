using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class QRSpawner : MonoBehaviour
{
    [SerializeField] private Animal animalPrefab;
    [SerializeField] private List<Sprite> sprites;
    
    private SpriteRenderer _spriteRenderer;
    private List<Animal> _animals = new();
    private RectTransform _canvasRect;
    private const float InitialInterval = 3f;
    private float _interval;

    private void Awake()
    {
        _canvasRect = GetComponentInParent<RectTransform>();
        _interval = InitialInterval;
        StartCoroutine(SpawnAnimalCoroutine());
        Events.OnGoodAnimalDied += ResetInterval;
        Events.OnBadAnimalDied += SpeedUp;
        Events.AddScore += SpeedUp;
    }

    private void SpeedUp() => SpeedUp(0);
    private void SpeedUp(int _)
    {
        _interval *= 0.8f;
    }

    private void ResetInterval()
    {
        _interval = InitialInterval;   
    }

    private void SpawnAnimal()
    {
        var animal = Instantiate(animalPrefab, transform);
        _animals.Add(animal);
        var target = Random.Range(0, 2) == 0 ? animal.GoodAnimals : animal.BadAnimals ;
        var type = target.GetRandomElement();
        animal.Initialize(type, SpriteFromType(type));
        animal.SpawnFromBottomAndShoot(_canvasRect);
    }

    // hihi horrible code
    private Sprite SpriteFromType(Animal.AnimalType type)
    {
        var typeString = type.ToString().ToLowerInvariant();
        foreach (var sprite in sprites)
        {
            if (sprite.name.ToLower().Contains(typeString))
                return sprite;
        }
        
        throw new Exception($"Couldn't find sprite for type {typeString}");
    }
    
    private IEnumerator SpawnAnimalCoroutine()
    {
        while (true)
        {
            SpawnAnimal();
            yield return new WaitForSeconds(_interval);
        }
    }
}