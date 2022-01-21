using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToMainScene : MonoBehaviour
{
    public VideoPlayer VideoPlayer;
    public bool isPlayerStarted = false;
    void Update()
    {
        if (isPlayerStarted == false && VideoPlayer.isPlaying == true)
        {
            isPlayerStarted = true;
        }
        if (isPlayerStarted == true && VideoPlayer.isPlaying == false)
        {
            SceneManager.LoadScene("Main");
        }     
    }
}
