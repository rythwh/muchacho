using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Flash : MonoBehaviour
{
    [SerializeField] private float flashStartDuration = 0.1f;
    [SerializeField] private float flashEndDuration = 1f;
    [SerializeField] private Image image;

    private Color flashColor = new Color(1f, 1f, 1f, 0.9f);

    public void Start()
    {
        image.color = Color.clear;
        Events.Flash += OnFlash;
    }

    private void OnDestroy()
    {
        Events.Flash -= OnFlash;
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
        image.color = flashColor;
        Events.OnFlashMax?.Invoke();

        // Short delay for "flash in"
        await Task.Delay((int)(flashStartDuration * 1000));

        // Fade out over time
        float elapsed = 0f;
        Color startColor = flashColor;

        while (elapsed < flashEndDuration)
        {
            await Task.Yield(); // Wait until the next frame
            elapsed += Time.deltaTime;
            float t = elapsed / flashEndDuration;
            image.color = Color.Lerp(startColor, Color.clear, t);
        }

        // Ensure it's fully transparent at the end
        image.color = Color.clear;
    }
}
