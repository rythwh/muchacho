using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesController : MonoBehaviour
{
    private const int maxLives = 5;
    private int currentLives = maxLives;
    [SerializeField] private GameObject livesPrefab;
    [SerializeField] private Transform livesContainer;

    void Start()
    {
        for (int i = 0; i < maxLives; i++)
        {
            Instantiate(livesPrefab, livesContainer, false);
        }
        Events.OnGoodAnimalDied += OnLifeLost;
    }

    private void OnDestroy()
    {
        Events.OnLifeLost -= OnLifeLost;
    }

    private void OnLifeLost()
    {
        currentLives -= 1;

        Image child = livesContainer.GetChild(0).GetComponent<Image>();

        if (!child)
        {
            return;
        }

        child.transform.GetChild(0).gameObject.SetActive(true);
        child.transform.SetAsLastSibling();

        if (currentLives <= 0)
        {
            Debug.Log("Game Over");
            Events.OnGameOver?.Invoke();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Events.OnGoodAnimalDied?.Invoke();
        }
    }
}
