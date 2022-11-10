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
        // アニメーション時間外でボタンを押した場合は
        if (director.AnimTime == 0f)
        {
            // ボタンを灰色に変化させる
            image.color = Color.gray;

            // パソコンボタンかスマホボタンであれば
            if (name == "PC Button")
            {
                // 現在のプラットフォームをパソコンに更新する
                StaticUnits.SmartPhone = false;

                // オープニング画面への遷移判定を有効にする
                director.OpeningSwitch = true;
            }
            else if (name == "Smart Phone Button")
            {
                // 現在のプラットフォームをスマホに更新する
                StaticUnits.SmartPhone = true;

                // オープニング画面への遷移判定を有効にする
                director.OpeningSwitch = true;
            }
            // プレイボタンであれば
            else if (name == "Playing Button")
            {
                // ゲームプレイへの遷移判定を有効にする
                director.PlayingSwitch = true;
            }
            // オプションボタンであれば
            else if (name == "Options Button")
            {
                // ゲームオプションへの遷移判定を有効にする
                director.OptionsSwitch = true;
            }
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        // アニメーション時間外でボタンにカーソルを当てた場合は
        if (director.AnimTime == 0f)
        {
            // ボタンを白っぽい灰色に変化させる
            image.color = new Color(0.9f, 0.9f, 0.9f, 1.0f);
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        // アニメーション時間外でボタンからカーソルを外した場合は
        if (director.AnimTime == 0f)
        {
            // ボタンを白色に変化させる（元の色に戻す）
            image.color = Color.white;
        }
    }
}
