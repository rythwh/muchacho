using UnityEngine;

public class PlayTalkingAnimation : MonoBehaviour
{
    private Animator animator;
    public AudioClip courseAudioClip;
    public AudioClip longCourseAudioClip;
    public AudioClip felizNavidadAudioClip;
    public float startDeathTime = 13f;
    public float endDeathTime = 16.5f;
    public float startFelizNavidadTime = 105f;
    public float endFelizNavidadTime = 108f;
    public float introStartTime = 0f;
    public float introEndTime = 8f;
    
    private AudioSource audioSource;
    
    void Awake ()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponentInChildren<Animator>();
    }

    public void PlayDeathClip()
    {
        animator.SetTrigger("PlayTalk");
        StartCoroutine(PlayClip(startDeathTime, endDeathTime, courseAudioClip));
    }
    
    public void PlayFelizNavidadClip()
    {
        animator.SetTrigger("PlayTalk");
        StartCoroutine(PlayClip(startFelizNavidadTime, endFelizNavidadTime, felizNavidadAudioClip));
    }
    
    public void PlayIntroClip()
    {
        animator.SetTrigger("PlayTalk");
        StartCoroutine(PlayClip(introStartTime, introEndTime, longCourseAudioClip));
    }
    
    private System.Collections.IEnumerator PlayClip(float startTime, float endTime, AudioClip audioClip = null)
    {
        if (audioClip != null)
        {
            audioSource.clip = audioClip;
            audioSource.time = startTime;
            audioSource.Play();
            
            yield return new WaitForSeconds(endTime - startTime);
            
            audioSource.Stop();
            animator.SetTrigger("StopTalk");
        }
    }
    

    public void PlayAnimation()
    {
        animator.SetTrigger("PlayTalk");
        PlayDeathClip();
    }
    
    public void StopAnimation()
    {
        animator.SetTrigger("StopTalk");
    }
}
