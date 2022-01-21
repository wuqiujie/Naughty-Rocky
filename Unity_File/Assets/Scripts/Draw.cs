using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    public GameObject paw;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag=="painting" )
        {
            GameObject.Instantiate(paw);
            paw.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }
}
