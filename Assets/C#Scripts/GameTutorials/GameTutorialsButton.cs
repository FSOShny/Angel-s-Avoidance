using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameTutorialsButton : MonoBehaviour,
    IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    // コンポーネント（イメージ、ディレクター、オーディオシステム）
    private Image image;
    private GameTutorialsDirector director;
    private AudioSystem audioSystem;

    private void Start()
    {
        // 各コンポーネントを取得する
        image = GetComponent<Image>();
        director = GameObject.FindGameObjectWithTag("Director").GetComponent<GameTutorialsDirector>();
        audioSystem = GameObject.FindGameObjectWithTag("AudioSystem").GetComponent<AudioSystem>();
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        // ボタンを入力すると
        // そのボタンの明るさを暗くする（程度：中）
        image.color = Color.gray;

        // （ボタンごとに振る舞いを変える）
        if (CompareTag("SkipText"))
        {
            // （効果音再生あり）
            audioSystem.Music = 0;

            if (name == "Skip Text (1)")
            {
                /* 現在のページを0に設定する */

                director.NowPage = 0;
            }
            else if (name == "Skip Text (2)")
            {
                /* 現在のページを2に設定する */

                director.NowPage = 2;
            }
            else if (name == "Skip Text (3)")
            {
                /* 現在のページを10に設定する */

                director.NowPage = 10;
            }
            else if (name == "Skip Text (4)")
            {
                /* 現在のページを13に設定する */

                director.NowPage = 13;
            }
            else if (name == "Skip Text (5)")
            {
                /* 現在のページを17に設定する */

                director.NowPage = 17;
            }
            else if (name == "Skip Text (6)")
            {
                /* 現在のページを20に設定する */

                director.NowPage = 20;
            }

            // ポーズ画面を無効にし、
            // 設定したページへ移動する
            director.ContinueSwitch = true;
            director.PageSwitch = true;
        }
        else
        {
            // （ボタンごとに振る舞いを変える）
            if (name == "Previous Button")
            {
                /* 前のページ数を設定する（効果音再生あり） */

                audioSystem.Music = 0;
                director.NowPage--;

                // 設定したページへ移動する
                director.PageSwitch = true;
            }
            else if (name == "Next Button")
            {
                /* 次のページ数を設定する（効果音再生あり） */

                audioSystem.Music = 0;
                director.NowPage++;

                // 設定したページへ移動する
                director.PageSwitch = true;
            }
            else if (name == "Pause Button")
            {
                /* ポーズ画面を有効にする（効果音再生あり） */

                audioSystem.Music = 1;
                director.PauseSwitch = true;
            }
            else if (name == "Continue Button")
            {
                /* ポーズ画面を無効にする（効果音再生あり） */

                audioSystem.Music = 2;
                director.ContinueSwitch = true;
            }
            else if (name == "Opening Button")
            {
                /* オープニングへ移動する（効果音再生あり） */

                audioSystem.Music = 3;
                director.OpeningSwitch = true;
            }
        }

        // そのボタンの明るさを元に戻す
        image.color = Color.white;
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        // ボタンにカーソルを当てると
        // そのボタンの明るさを暗くする（程度：小）
        image.color = new Color(0.9f, 0.9f, 0.9f, 1.0f);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        // ボタンからカーソルを外すと
        // そのボタンの明るさを元に戻す
        image.color = Color.white;
    }
}
