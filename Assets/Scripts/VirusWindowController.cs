using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class VirusWindowController : MonoBehaviour
{
    [SerializeField] private RectTransform canvasRt;
    [SerializeField] private GameObject virusWindowPrefab;
    [SerializeField] private List<Sprite> virusSprites;
    [SerializeField] private RectTransform bounceArea;

    private void Start()
    {
        Events.OnShotBadAnimal += OnShotBadAnimal;
    }

    private void OnDestroy()
    {
        Events.OnShotBadAnimal -= OnShotBadAnimal;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Events.OnShotBadAnimal?.Invoke();
        }
    }

    private void OnShotBadAnimal()
    {
        GameObject virusWindow = Instantiate(virusWindowPrefab, transform, false);
        Sprite sprite = virusSprites[Random.Range(0, virusSprites.Count)];
        virusWindow.GetComponent<Image>().sprite = sprite;
        virusWindow.GetComponent<AspectRatioFitter>().aspectRatio = sprite.rect.width / sprite.rect.height;

        RectTransform windowRt = (RectTransform)virusWindow.transform;
        windowRt.anchoredPosition = new Vector3(Random.Range(0f, 1f) * canvasRt.rect.width, Random.Range(0f, 1f) * canvasRt.rect.height, 0);
        windowRt.sizeDelta = new Vector2(Random.Range(200, 400), 0);

        BouncingWindow bouncingWindow = virusWindow.GetComponent<BouncingWindow>();
        bouncingWindow.SetBounceArea(bounceArea);
    }
}
