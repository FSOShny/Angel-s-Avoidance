using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameEndingButton : MonoBehaviour,
    IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    // コンポーネント（イメージ、ディレクター、オーディオシステム）
    private Image image;
    private GameEndingDirector director;
    private AudioSystem audioSystem;

    private void Start()
    {
        // 各コンポーネントを取得する
        image = GetComponent<Image>();
        director = GameObject.FindGameObjectWithTag("Director").GetComponent<GameEndingDirector>();
        audioSystem = GameObject.FindGameObjectWithTag("AudioSystem").GetComponent<AudioSystem>();
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        // ボタンを入力すると
        // そのボタンの明るさを暗くする（程度：中）
        // （アニメーションのときは無効）
        if (director.AnimTime == 0f)
        {
            image.color = Color.gray;

            // （効果音再生あり）
            audioSystem.Music = 0;

            if (name == "Opening Button")
            {
                /* オープニングへ移動する */

                director.OpeningSwitch = true;
            }
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        // ボタンにカーソルを当てると
        // そのボタンの明るさを暗くする（程度：小）
        // （アニメーションのときは無効）
        if (director.AnimTime == 0f)
        {
            image.color = new Color(0.9f, 0.9f, 0.9f, 1.0f);
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        // ボタンからカーソルを外すと
        // そのボタンの明るさを元に戻す
        // （アニメーションのときは無効）
        if (director.AnimTime == 0f)
        {
            image.color = Color.white;
        }
    }
}
