using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class StartController : MonoBehaviour
{
    public PlayTalkingAnimation playTalkingAnimation;
    public Button startButton;
    public GameObject TalkingJack;
    
    void Start()
    {
        startButton.onClick.AddListener(StartGameCoroutine);
    }
    
    void StartGameCoroutine()
    {
        StartCoroutine(StartGame());
    }
    
    // Update is called once per frame
    private IEnumerator StartGame()
    { 
        TalkingJack.SetActive(true); 
        playTalkingAnimation.PlayIntroClip(); 
        yield return new WaitForSeconds(9f); // Wait for the intro clip to finish
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
}
