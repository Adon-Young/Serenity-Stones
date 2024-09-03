using UnityEngine;
using UnityEngine.Video;
using UnityEngine.EventSystems; // Needed for IPointerEnterHandler and IPointerExitHandler

public class VideoHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private VideoPlayer videoPlayer;
    public GameObject BackgroundAudio;
    private void Awake()
    {
        // Get the VideoPlayer component attached to this GameObject
        videoPlayer = GetComponent<VideoPlayer>();

        if (videoPlayer != null)
        {
            // Set the video to loop
            videoPlayer.isLooping = true;

            // Pause the video to ensure it doesn't play on start
            videoPlayer.Pause();
        }
    }

    // This method is called when the pointer enters the GameObject's area
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (videoPlayer != null)
        {
            videoPlayer.Play();
            BackgroundAudio.SetActive(false);
        }
    }

    // This method is called when the pointer exits the GameObject's area
    public void OnPointerExit(PointerEventData eventData)
    {
        if (videoPlayer != null)
        {
            videoPlayer.Pause();
            BackgroundAudio.SetActive(true);
        }
    }
}
