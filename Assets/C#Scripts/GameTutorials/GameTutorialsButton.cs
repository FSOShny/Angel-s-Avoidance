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

        // スキップテキストであり
        if (CompareTag("SkipText"))
        {
            // スキップテキスト(1)であれば
            if (name == "Skip Text (1)")
            {
                // 特定のページ数を設定する
                director.NowPage = 0;
            }
            // スキップテキスト(2)であれば
            else if (name == "Skip Text (2)")
            {
                // 特定のページ数を設定する
                director.NowPage = 2;
            }
            // スキップテキスト(3)であれば
            else if (name == "Skip Text (3)")
            {
                // 特定のページ数を設定する
                director.NowPage = 10;
            }
            // スキップテキスト(4)であれば
            else if (name == "Skip Text (4)")
            {
                // 特定のページ数を設定する
                director.NowPage = 13;
            }
            // スキップテキスト(5)であれば
            else if (name == "Skip Text (5)")
            {
                // 特定のページ数を設定する
                director.NowPage = 17;
            }
            // スキップテキスト(6)であれば
            else if (name == "Skip Text (6)")
            {
                // 特定のページ数を設定する
                director.NowPage = 20;
            }

            // ゲームプレイの続行を有効にする
            director.ContinueSwitch = true;

            // 特定のページへの遷移を有効にする
            director.PageSwitch = true;
        }
        // スキップテキストでなく
        else
        {
            // プリビアスボタンであれば
            if (name == "Previous Button")
            {
                // 前のページ数を設定する
                director.NowPage--;

                // 前のページへの遷移を有効にする
                director.PageSwitch = true;
            }
            // ネクストボタンであれば
            else if (name == "Next Button")
            {
                // 次のページ数を設定する
                director.NowPage++;

                // 次のページへの遷移を有効にする
                director.PageSwitch = true;
            }
            // ポーズボタンであれば
            else if (name == "Pause Button")
            {
                // ポーズ画面への遷移を有効にする
                director.PauseSwitch = true;
            }
            // コンティニューボタンであれば
            else if (name == "Continue Button")
            {
                // ゲームプレイの続行を有効にする
                director.ContinueSwitch = true;
            }
            // オープニングボタンであれば
            else if (name == "Opening Button")
            {
                // オープニングへの遷移を有効にする
                director.OpeningSwitch = true;
            }
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
