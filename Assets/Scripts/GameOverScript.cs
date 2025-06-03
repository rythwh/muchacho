using UnityEngine;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    public PlayTalkingAnimation playTalkingAnimation;
    public Button restartButton;
    void Start()
    {
        playTalkingAnimation.PlayDeathClip();
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame);
        }
    }

    void RestartGame()
    {
        gameObject.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
}
