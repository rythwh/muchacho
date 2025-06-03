using System.Collections.Generic;
using UnityEngine;

public class QRSpawner : MonoBehaviour
{
    [SerializeField] private List<Sprite> goodSprites;
    [SerializeField] private List<Sprite> badSprites;
    
    private SpriteRenderer _spriteRenderer;
    void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            var target = Random.Range(0, 2) == 0 ? goodSprites : badSprites;
            _spriteRenderer.sprite = target[Random.Range(0, target.Count)];
        }
    }
}
