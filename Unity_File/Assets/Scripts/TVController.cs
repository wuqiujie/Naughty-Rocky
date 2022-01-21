using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVController : MonoBehaviour
{
    public Television television;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            television.ChangeTvStatus();
        }
    }
}
