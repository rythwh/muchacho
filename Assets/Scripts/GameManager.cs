using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private bool debugRaycast = false;
    [SerializeField] private GameObject endScreen;

    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // optional: persist across scenes
        DontDestroyOnLoad(endScreen);
        Events.OnGameOver += ShowEndScreen;
    }

    public void ScanAnimal(Animal animal)
    {
        Debug.Log($"Destroying animal {animal.Type}!");
        if (animal.IsGood)
        {
            Events.AddScore(1);
        }
        else
        {
            Events.OnShotBadAnimal?.Invoke();
        }

        Destroy(animal.gameObject);
    }

    public void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Events.Flash?.Invoke();
        }

        if (!debugRaycast)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            foreach (var result in results)
            {
                Debug.Log("Raycast hit: " + result.gameObject.name);
            }

            if (results.Count == 0)
                Debug.Log("Raycast hit nothing.");
        }
    }

    private void ShowEndScreen()
    {
        endScreen.SetActive(true);
        //Time.timeScale = 0f; // Pause the game
        Debug.Log("Game Over! Showing end screen.");
    }
}
