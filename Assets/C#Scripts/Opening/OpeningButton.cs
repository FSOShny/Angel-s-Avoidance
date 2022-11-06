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
    private float animTime = 0f;

    private void Start()
    {
        image = GetComponent<Image>();
        director = GameObject.Find("Opening Director").GetComponent<OpeningDirector>();
    }

    private void Update()
    {
        if (animTime > 0f)
        {
            animTime = Time.deltaTime;
        }
        else if (animTime < 0f)
        {
            animTime = 0f;
        }
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (animTime == 0f)
        {
            image.color = new Color(0.5f, 0.5f, 0.5f, 1.0f);

            if (name == "PC Button" || name == "Smart Phone Button")
            {
                director.Opening = true;

                animTime = 3.0f;
            }

            if (name == "Playing Button")
            {
                director.Playing = true;
            }
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (animTime == 0f)
        {
            image.color = new Color(0.9f, 0.9f, 0.9f, 1.0f);
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (animTime == 0f)
        {
            image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
    }
}
