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
        director = GameObject.FindGameObjectWithTag("Director").GetComponent<OpeningDirector>();
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        // アニメーション時間外にボタンを押すと
        if (director.AnimTime == 0f)
        {
            // ボタンを灰色に変化させる
            image.color = Color.gray;

            // パソコンボタンであれば
            if (name == "PC Button")
            {
                // 現在のプラットフォームをパソコンに更新する
                StaticUnits.SmartPhone = false;

                // オープニング画面への遷移を有効にする
                director.OpeningSwitch = true;
            }
            // スマホボタンであれば
            else if (name == "Smart Phone Button")
            {
                // 現在のプラットフォームをスマホに更新する
                StaticUnits.SmartPhone = true;

                // オープニング画面への遷移を有効にする
                director.OpeningSwitch = true;
            }
            // チュートリアルボタンであれば
            else if (name == "Tutorials Button")
            {
                // ゲームチュートリアルへの遷移を有効にする
                director.TutorialsSwitch = true;
            }
            // プレイボタンであれば
            else if (name == "Playing Button")
            {
                // ゲームプレイへの遷移を有効にする
                director.PlayingSwitch = true;
            }
            // オプションボタンであれば
            else if (name == "Options Button")
            {
                // ゲームオプションへの遷移を有効にする
                director.OptionsSwitch = true;
            }
            // シャットダウンボタンであれば
            else if (name == "Shutdown Button")
            {
                // （エディターでの反応確認）
                Debug.Log("Shutdown !!!");

                // アプリケーションをシャットダウンする
                Application.Quit();
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
        // アニメーション時間外でボタンからカーソルを外すと
        if (director.AnimTime == 0f)
        {
            // ボタンを白色に変化させる（元の色に戻す）
            image.color = Color.white;
        }
    }
}
