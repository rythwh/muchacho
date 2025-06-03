using UnityEngine;
using UnityEngine.UI;

public class PromptController : MonoBehaviour
{
    public PlayTalkingAnimation playTalkingAnimation;
    public Button talkButton;
    
    public void Awake()
    {
        if (talkButton != null)
        {
            talkButton.onClick.AddListener(Talk);
        }
    }

    public void Talk()
    {
        playTalkingAnimation.PlayAnimation();
    }
    
    public void StopTalking()
    {
        playTalkingAnimation.StopAnimation();
    }
}
