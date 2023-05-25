using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoPlayer : MonoBehaviour
{
    public GameObject videoPlayer;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartVideo", 2);
    }

    void StartVideo()
    {
        videoPlayer.SetActive(true);
    }

}
