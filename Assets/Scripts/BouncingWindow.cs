using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BouncingWindow : MonoBehaviour
{
    private Vector2 velocity;
    private RectTransform rectTransform;
    private RectTransform bounceAreaRect;

    private int[] directions = { -1, 1 };
    private float randomVelocity => Random.Range(100f, 300f) * directions[Random.Range(0, directions.Length)];

    void Start()
    {
        velocity = new Vector2(randomVelocity, randomVelocity); // pixels per second
        rectTransform = GetComponent<RectTransform>();

        Events.SetSpeedMultiplier += SetSpeedMultiplier;
    }

    private void OnDestroy()
    {
        Events.SetSpeedMultiplier -= SetSpeedMultiplier;
    }

    private void SetSpeedMultiplier(float speedMultiplier)
    {
        velocity *= speedMultiplier;
    }

    public void SetBounceArea(RectTransform bounceArea)
    {
        bounceAreaRect = bounceArea;
    }

    void Update()
    {
        if (!bounceAreaRect)
        {
            return;
        }

        Vector2 position = rectTransform.anchoredPosition;
        Vector2 size = rectTransform.rect.size;
        Vector2 bounceAreaSize = bounceAreaRect.rect.size;

        // Move the window
        position += velocity * Time.deltaTime;

        // Check for collisions with edges and reflect direction
        if (position.x < 0)
        {
            position.x = 0;
            velocity.x *= -1;
        }
        else if (position.x + size.x > bounceAreaSize.x)
        {
            position.x = bounceAreaSize.x - size.x;
            velocity.x *= -1;
        }

        if (position.y < 0)
        {
            position.y = 0;
            velocity.y *= -1;
        }
        else if (position.y + size.y > bounceAreaSize.y)
        {
            position.y = bounceAreaSize.y - size.y;
            velocity.y *= -1;
        }

        rectTransform.anchoredPosition = position;
    }
}
