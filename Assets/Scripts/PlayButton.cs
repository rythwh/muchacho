using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    [SerializeField] private string gameScene = "Game";
    [SerializeField] private Button button;

    void Start()
    {
        button.onClick.AddListener(ButtonClicked);
    }

    private void ButtonClicked()
    {
        SceneManager.LoadScene(gameScene);
    }
}
