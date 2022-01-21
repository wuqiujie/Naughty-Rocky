using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{

    public GameObject TV_Table;
    public GameObject TV;
    public GameObject Teapoy;
    public GameObject Sofa;
    public GameObject Table;
    public GameObject Romoter;
    public GameObject Carpet;
    public GameObject Catcus;
    public GameObject Painting;
    public GameObject Windows;
    public GameObject Milk;
    public GameObject Platte;



    static ObjectManager s_Instance;
    public static ObjectManager Instance => s_Instance;

    void Awake()
    {
        if (s_Instance != null)
        {
            Destroy(this);
            return;
        }

        s_Instance = this;
    }

    public void OpenTV()
    {
        if (TV.gameObject.GetComponent<Television>().isTurnedOn == true)
        {
            GameManager.Instance.ChangeMood(1);
        }
    }


}
