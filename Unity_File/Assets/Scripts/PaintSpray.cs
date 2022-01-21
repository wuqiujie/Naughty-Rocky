using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintSpray : MonoBehaviour
{
    public GameObject spray;

    public AudioClip onTriggerClip;

    bool isTouched;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            MusicManager.Instance.PlayClip(onTriggerClip);
            spray.gameObject.SetActive(true);

            if(!isTouched)
            {
                GameManager.Instance.ChangeMood(1);
                isTouched = true;
            }
        }
    }
}
