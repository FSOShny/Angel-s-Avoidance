using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameTutorialsButton : MonoBehaviour,
    IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Image image;
    private GameTutorialsDirector director;

    private void Start()
    {
        // イメージコンポーネントを取得する
        image = GetComponent<Image>();

        // オープニングディレクターを取得する
        director = GameObject.FindGameObjectWithTag("Director").GetComponent<GameTutorialsDirector>();
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        // ボタンを入力すると

        // ボタンを灰色に変化させる
        image.color = Color.gray;

        // オープニングボタンであれば
        if (name == "Opening Button")
        {
            // オープニングへの遷移を有効にする
            director.OpeningSwitch = true;
        }
        // ネクストボタンであれば
        else if (name == "Next Button")
        {
            // 次のページへの遷移を有効にする
            director.NextSwitch = true;
        }
        // プリビアスボタンであれば
        else if (name == "Previous Button")
        {
            // 前のページへの遷移を有効にする
            director.PrevSwitch = true;
        }

        // ボタンを白色に変化させる（元の色に戻す）
        image.color = Color.white;
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        // ボタンにカーソルを当てると

        // ボタンを白っぽい灰色に変化させる
        image.color = new Color(0.9f, 0.9f, 0.9f, 1.0f);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        // ボタンからカーソルを外すと

        // ボタンを白色に変化させる（元の色に戻す）
        image.color = Color.white;
    }
}
