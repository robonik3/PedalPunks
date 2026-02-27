using UnityEngine;
using UnityEngine.Video;
using System.IO;

public class VideoPlayerScript : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string fileName = "Opening.mp4";

    void Awake()
    {
        string path = Path.Combine(Application.streamingAssetsPath, fileName);
        videoPlayer.url = path;
        videoPlayer.Prepare();
    }
}