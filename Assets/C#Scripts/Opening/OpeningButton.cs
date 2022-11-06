using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OpeningButton : MonoBehaviour, IPointerClickHandler, IPointerDownHandler,
    IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    private float delta = 0f;
    private int term = 3;

    private void Update()
    {
        if (delta < term)
        {
            delta += Time.deltaTime;
        }
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (delta > term)
        {
            Debug.Log(name);
        }
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (delta > term)
        {
            Debug.Log(name);
        }
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        if (delta > term)
        {
            Debug.Log(name);
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (delta > term)
        {
            Debug.Log(name);
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (delta > term)
        {
            Debug.Log(name);
        }
    }
}
