using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public enum CleaningType
{
    None,
    HideAndReturn
}

public class InteractableObjects : MonoBehaviour
{
    public int happinessPoints = 1;

    public CleaningType cleaningType;

    public AudioClip onSelectClip;

    public HideArea[] areas;

    bool isTouched;

    bool isStayInHideArea;

    bool isHover;
    bool isSelected;

    private void Start()
    {
        GetComponent<XRGrabInteractable>().selectEntered.AddListener(OnSelectEnter);
        GetComponent<XRGrabInteractable>().selectExited.AddListener(OnSelectExit);
        GetComponent<XRGrabInteractable>().firstHoverEntered.AddListener(OnHoverEnter);
        GetComponent<XRGrabInteractable>().lastHoverExited.AddListener(OnHoverExit);
        isStayInHideArea = false;

        if (cleaningType == CleaningType.HideAndReturn)
        {
            GameManager.Instance.InitCleanValue();
        }
    }

    void OnHoverEnter(HoverEnterEventArgs args)
    {
        isHover = true;
    }

    void OnHoverExit(HoverExitEventArgs args)
    {
        isHover = false;
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<HideArea>() != null && CheckArea(other.gameObject) && !isStayInHideArea && cleaningType == CleaningType.HideAndReturn && !isSelected)
        {
            isStayInHideArea = true;

            GameManager.Instance.ChangeClean(1);

            if (GameManager.Instance.state == GameState.CleaningState && !isSelected)
            {
                MusicManager.Instance.PlayClean();
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<HideArea>() != null && CheckArea(other.gameObject) && isStayInHideArea && cleaningType == CleaningType.HideAndReturn)
        {
            isStayInHideArea = false;

            GameManager.Instance.ChangeClean(-1);
        }
    }

    bool CheckArea(GameObject obj)
    {
        if(areas == null && areas.Length == 0)
        {
            return false;
        }

        foreach (HideArea area in areas)
        {
            if (area.gameObject == obj)
            {
                return true;
            }
        }

        return false;
    }

    public void OnSelectExit(SelectExitEventArgs interactor)
    {
        isSelected = false;
        if (cleaningType == CleaningType.HideAndReturn && GameManager.Instance.state == GameState.CleaningState)
        {
            GameManager.Instance.DisableHideAreaHint();
        }

        if (isTouched)
        {
            return;
        }

        isTouched = true;

        GameManager.Instance.ChangeMood(happinessPoints);
    }

    public void OnSelectEnter(SelectEnterEventArgs interactor)
    {
        isSelected = true;

        if (onSelectClip != null)
        {
            MusicManager.Instance.PlayClip(onSelectClip);
        }

        if (cleaningType == CleaningType.HideAndReturn && GameManager.Instance.state == GameState.CleaningState)
        {
            GameManager.Instance.ShowHideAreaHint();
        }
    }


}
