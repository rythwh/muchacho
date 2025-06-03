using System;
using UnityEngine;

public class LivesController : MonoBehaviour
{
    [SerializeField] private int lives = 5;
    [SerializeField] private GameObject livesPrefab;
    [SerializeField] private Transform livesContainer;

    void Start()
    {
        Events.OnLifeLost += OnLifeLost;
    }

    private void OnLifeLost()
    {

    }
}
