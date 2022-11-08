using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameOptionsButton : MonoBehaviour, 
    IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Image image;
    private GameOptionsDirector director;
    private float animTime;

    private void Start()
    {
        image = GetComponent<Image>();
        director = GameObject.Find("Game Options Director").GetComponent<GameOptionsDirector>();
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (animTime == 0f)
        {
            image.color = new Color(0.5f, 0.5f, 0.5f, 1.0f);

            if (name == "Opening Button")
            {
                director.Opening = true;
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
