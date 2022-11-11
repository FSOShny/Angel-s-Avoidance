using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameEndingButton : MonoBehaviour,
    IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Image image;
    private GameEndingDirector director;

    private void Start()
    {
        // イメージコンポーネントを取得する
        image = GetComponent<Image>();

        // オープニングディレクターを取得する
        director = GameObject.Find("Game Ending Director").GetComponent<GameEndingDirector>();
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        // アニメーション時間外にボタンを押すと
        if (director.AnimTime == 0f)
        {
            // ボタンを灰色に変化させる
            image.color = Color.gray;

            // オープニングボタンであれば
            if (name == "Opening Button")
            {
                // オープニングへの遷移判定を有効にする
                director.OpeningSwitch = true;
            }
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        // アニメーション時間外にボタンにカーソルを当てると
        if (director.AnimTime == 0f)
        {
            // ボタンを白っぽい灰色に変化させる
            image.color = new Color(0.9f, 0.9f, 0.9f, 1.0f);
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        // アニメーション時間外にボタンからカーソルを外すと
        if (director.AnimTime == 0f)
        {
            // ボタンを白色に変化させる（元の色に戻す）
            image.color = Color.white;
        }
    }
}
