using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusHitTrigger : MonoBehaviour
{
    public AudioClip dogHitSFX;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            MusicManager.Instance.PlayClip(dogHitSFX);
        }
    }
}
