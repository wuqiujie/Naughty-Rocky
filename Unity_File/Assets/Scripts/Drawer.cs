using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour
{
    public AudioClip drawInteract;

    public void PlaySound()
    {
        MusicManager.Instance.PlayClip(drawInteract);
    }
}
