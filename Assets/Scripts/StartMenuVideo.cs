using UnityEngine;
using UnityEngine.Video;

public class StartMenuVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    void Start()
    {
        videoPlayer.Play();
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        vp.enabled = false;
    }
}
