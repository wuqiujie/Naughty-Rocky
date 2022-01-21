using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class Television : MonoBehaviour
{
    public bool isTurnedOn;
    public bool GetTvStatus() => isTurnedOn;

    public Material onMaterial;
    public Material offMaterial;

    public AudioClip switchOnClip;
    public AudioClip switchOffClip;

    private VideoPlayer videoPlayer;

    private void Awake()
    {
        isTurnedOn = false;
        videoPlayer = GetComponent<VideoPlayer>();
    }

    public void ChangeTvStatus()
    {
        isTurnedOn = !isTurnedOn;
        ChangeScreenStatus(isTurnedOn);
    }

    private void ChangeScreenStatus(bool isTurnedOn)
    {
        if (isTurnedOn)
        {
            GetComponent<MeshRenderer>().material = onMaterial;
            videoPlayer.Play();
            MusicManager.Instance.PlayClip(switchOnClip);
        }
        else
        {
            GetComponent<MeshRenderer>().material = offMaterial;
            videoPlayer.Stop();
            MusicManager.Instance.PlayClip(switchOffClip);
        }
    }
}
