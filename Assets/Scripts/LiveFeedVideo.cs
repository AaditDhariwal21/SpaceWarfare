using UnityEngine;
using UnityEngine.Video;

public class LiveFeedVideo : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;

    private void Start()
    {
        videoPlayer.source = VideoSource.Url;
        videoPlayer.url =
            System.IO.Path.Combine(
                Application.streamingAssetsPath,
                "LiveFeed.mp4"
            );

        videoPlayer.isLooping = true;
        videoPlayer.playOnAwake = false;

        videoPlayer.errorReceived += (player, message) =>
            Debug.LogError("Live feed error: " + message);

        videoPlayer.prepareCompleted += player =>
        {
            Debug.Log("Live feed prepared. Starting playback.");
            player.Play();
        };

        videoPlayer.Prepare();
    }
}