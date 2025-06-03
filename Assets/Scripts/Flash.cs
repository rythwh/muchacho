using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Flash : MonoBehaviour
{
    [SerializeField] private float flashStartDuration = 0.1f;
    [SerializeField] private float flashEndDuration = 1f;
    [SerializeField] private Image image;

    public void Start()
    {
        image.color = Color.clear;
        Events.Flash += OnFlash;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            OnFlash();
        }
    }

    private async void OnFlash()
    {
        if (!image)
        {
            return;
        }

        // Flash to white instantly
        image.color = new Color(1, 1, 1, 1); // white, fully opaque

        // Short delay for "flash in"
        await Task.Delay((int)(flashStartDuration * 1000));

        // Fade out over time
        float elapsed = 0f;
        Color startColor = Color.white;

        while (elapsed < flashEndDuration)
        {
            await Task.Yield(); // Wait until the next frame
            elapsed += Time.deltaTime;
            float t = elapsed / flashEndDuration;
            image.color = Color.Lerp(startColor, new Color(1, 1, 1, 0), t);
        }

        // Ensure it's fully transparent at the end
        image.color = new Color(1, 1, 1, 0);
    }
}
