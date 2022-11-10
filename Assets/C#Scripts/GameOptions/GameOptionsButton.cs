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

    private void Start()
    {
        // イメージコンポーネントを取得する
        image = GetComponent<Image>();

        // オープニングディレクターを取得する
        director = GameObject.Find("Game Options Director").GetComponent<GameOptionsDirector>();
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        // ボタンを押した場合は

        // ボタンを灰色に変化させる
        image.color = Color.gray;

        // オープニングボタンであれば
        if (name == "Opening Button")
        {
            // オープニングへの遷移判定を有効にする
            director.OpeningSwitch = true;
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        // ボタンにカーソルを当てた場合は

        // ボタンを白っぽい灰色に変化させる
        image.color = new Color(0.9f, 0.9f, 0.9f, 1.0f);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        // ボタンからカーソルを外した場合は

        // ボタンを白色に変化させる（元の色に戻す）
        image.color = Color.white;
    }
}
