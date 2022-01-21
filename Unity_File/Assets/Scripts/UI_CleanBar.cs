using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CleanBar : MonoBehaviour
{
    public Slider bar;

    static UI_CleanBar s_Instance;
    public static UI_CleanBar Instance => s_Instance;

    void Awake()
    {
        if (s_Instance != null)
        {
            Destroy(this);
            return;
        }

        s_Instance = this;
    }

    public void SetValue(float fillPercent)
    {
        bar.value = fillPercent;
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
