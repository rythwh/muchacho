using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesController : MonoBehaviour
{
    private const int maxLives = 5;
    private int currentLives = maxLives;
    [SerializeField] private GameObject livesPrefab;
    [SerializeField] private Transform livesContainer;
    private const int iframeSeconds = 2;

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
        Events.OnGoodAnimalDied -= OnLifeLost;
    }

    bool _hasIframes = false;
    private void OnLifeLost()
    {
        if (_hasIframes)
            return;
        
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

        StartCoroutine(ActivateIFrames());
    }

    private IEnumerator ActivateIFrames()
    {
        _hasIframes = true;
        yield return new WaitForSeconds(iframeSeconds);
        _hasIframes = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Events.OnGoodAnimalDied?.Invoke();
        }
    }
}
