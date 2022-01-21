using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideArea : MonoBehaviour
{
    public GameObject m_hintArea;

    private void Start()
    {
        GameManager.Instance.AddHideAreas(this);
        m_hintArea.SetActive(false);
    }

    public void ShowHint()
    {
        m_hintArea.SetActive(true);
    }

    public void DisableHint()
    {
        m_hintArea.SetActive(false);
    }
}
