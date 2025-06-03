using System.Collections.Generic;
using UnityEngine;

public class QRSpawner : MonoBehaviour
{
    [SerializeField] private Animal animalPrefab;
    [SerializeField] private List<Sprite> goodSprites;
    [SerializeField] private List<Sprite> badSprites;
    
    private SpriteRenderer _spriteRenderer;
    private List<Animal> _animals = new();
    
    void Awake()
    {
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            // instantiate animal prefab
            var animal = Instantiate(animalPrefab, transform);
            _animals.Add(animal);
            var target = Random.Range(0, 2) == 0 ? goodSprites : badSprites;
            animal.image.sprite = target[Random.Range(0, target.Count)];
        }
    }
}
