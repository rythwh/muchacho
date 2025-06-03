using UnityEngine;
using UnityEngine.UI;

public class Animal : MonoBehaviour
{
    public Image image;
    
    private void Awake()
    {
        image = GetComponent<Image>();
    }
}