using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private bool debugRaycast = false;

    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // optional: persist across scenes
    }

    public void ScanAnimal(Animal animal)
    {
        Debug.Log($"Destroying animal {animal.Type}!");
        if (animal.IsGood)
        {
            // TODO: score go up
        }
        else
        {
            Events.OnShotBadAnimal?.Invoke();
        }

        Destroy(animal.gameObject);
    }

    public void Update()
    {
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
}