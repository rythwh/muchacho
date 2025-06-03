using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class QRSpawner : MonoBehaviour
{
    [SerializeField] private Animal animalPrefab;
    [SerializeField] private List<Sprite> sprites;
    
    private SpriteRenderer _spriteRenderer;
    private List<Animal> _animals = new();

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space)) 
            SpawnAnimal();
    }

    private void SpawnAnimal()
    {
        var animal = Instantiate(animalPrefab, transform);
        _animals.Add(animal);
        var target = Random.Range(0, 2) == 0 ? animal.GoodAnimals : animal.BadAnimals ;
        var type = target.GetRandomElement();
        animal.Initialize(type, SpriteFromType(type));
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
}