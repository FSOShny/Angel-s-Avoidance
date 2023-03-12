using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OpeningButton : MonoBehaviour, 
    IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    // コンポーネント（イメージ、ディレクター、オーディオシステム）
    private Image image;
    private OpeningDirector director;
    private AudioSystem audioSystem;

    private void Start()
    {
        // 各コンポーネントを取得する
        image = GetComponent<Image>();
        director = GameObject.FindGameObjectWithTag("Director").GetComponent<OpeningDirector>();
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

            // （ボタンごとに振る舞いを変える）
            if (name == "PC Button")
            {
                /* 現在のプラットフォームをパソコンにする（効果音再生あり） */

                audioSystem.Music = 0;
                StaticUnits.SmartPhone = false;

                // オープニング画面を有効にする
                director.OpeningSwitch = true;
            }
            else if (name == "Smart Phone Button")
            {
                /* 現在のプラットフォームをスマホにする（効果音再生あり） */

                audioSystem.Music = 0;
                StaticUnits.SmartPhone = true;

                // オープニング画面を有効にする
                director.OpeningSwitch = true;
            }
            else if (name == "Tutorials Button")
            {
                /* ゲームチュートリアルへ移動する（効果音再生あり） */

                audioSystem.Music = 1;
                director.TutorialsSwitch = true;
            }
            else if (name == "Playing Button")
            {
                /* ゲームプレイへ移動する（効果音再生あり） */

                audioSystem.Music = 2;
                director.PlayingSwitch = true;
            }
            else if (name == "Options Button")
            {
                /* ゲームオプションへ移動する（効果音再生あり） */

                audioSystem.Music = 3;
                director.OptionsSwitch = true;
            }
            else if (name == "Shutdown Button")
            {

                /* ゲームをシャットダウンする（効果音再生あり） */

                audioSystem.Music = 4;
                Application.Quit();

                // （エディターでの反応確認）
                Debug.Log("Shutdown !!!");
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
