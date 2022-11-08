using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OpeningButton : MonoBehaviour, 
    IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Image image;
    private OpeningDirector director;

    private void Start()
    {
        image = GetComponent<Image>();
        director = GameObject.Find("Opening Director").GetComponent<OpeningDirector>();
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (director.AnimTime == 0f)
        {
            image.color = new Color(0.5f, 0.5f, 0.5f, 1.0f);

            if (name == "PC Button" || name == "Smart Phone Button")
            {
                director.Opening = true;
            }

            if (name == "Playing Button")
            {
                director.Playing = true;
            }

            if (name == "Options Button")
            {
                director.Options = true;
            }
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (director.AnimTime == 0f)
        {
            image.color = new Color(0.9f, 0.9f, 0.9f, 1.0f);
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (director.AnimTime == 0f)
        {
            image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
    }
}
