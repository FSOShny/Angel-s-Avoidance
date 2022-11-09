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
        // イメージコンポーネントを取得する
        image = GetComponent<Image>();

        // オープニングディレクターを取得する
        director = GameObject.Find("Opening Director").GetComponent<OpeningDirector>();
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        // アニメーション時間外にボタンを押した場合は
        if (director.AnimTime == 0f)
        {
            // ボタンを灰色に変化させる
            image.color = new Color(0.5f, 0.5f, 0.5f, 1.0f);

            // パソコンボタンかスマホボタンであれば
            if (name == "PC Button" || name == "Smart Phone Button")
            {
                // オープニング画面への遷移判定を有効にする
                director.Opening = true;
            }

            // プレイボタンであれば
            if (name == "Playing Button")
            {
                // ゲームプレイへの遷移判定を有効にする
                director.Playing = true;
            }

            // オプションボタンであれば
            if (name == "Options Button")
            {
                // ゲームオプションへの遷移判定を有効にする
                director.Options = true;
            }
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        // アニメーション時間外にボタンにカーソルを当てた場合は
        if (director.AnimTime == 0f)
        {
            // ボタンを白っぽい灰色に変化させる
            image.color = new Color(0.9f, 0.9f, 0.9f, 1.0f);
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        // アニメーション時間外にボタンからカーソルを外した場合は
        if (director.AnimTime == 0f)
        {
            // ボタンを白色に変化させる（元の色に戻す）
            image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
    }
}
