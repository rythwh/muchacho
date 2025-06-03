using UnityEngine;

public class PlayTalkingAnimation : MonoBehaviour
{
    private Animator animator;
    
    void Awake ()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void PlayAnimation()
    {
        animator.SetTrigger("PlayTalk");
    }
    
    public void StopAnimation()
    {
        animator.SetTrigger("StopTalk");
    }
}
