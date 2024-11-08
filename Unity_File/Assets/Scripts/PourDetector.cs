﻿using System.Collections;
using UnityEngine;

public class PourDetector : MonoBehaviour
{
    public int pourThreshold = 45;
    public Transform origin = null;
    public GameObject streamPrefab = null;

    public bool isPouring = false;
    private Stream currentStream = null;


    private void Update()
    {
        bool pourCheck = CalculatePourAngle() < pourThreshold;

        if(isPouring != pourCheck)
        {
            isPouring = pourCheck;

            if (isPouring)
            {
                StartPour();
            }
            else
            {
                EndPour();
            }
        }
    }
    private void StartPour()
    {
        print("StartPouring");
        currentStream = CreateStream();
        currentStream.Begin();
    }

    private void EndPour()
    {
        print("EndPouring");
        currentStream.End();
        currentStream = null;
    }

    private float CalculatePourAngle()
    {
        return transform.forward.z * Mathf.Rad2Deg;
    }

    private Stream CreateStream()
    {
        GameObject streamObject = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform);
        return streamObject.GetComponent<Stream>();
    }

}