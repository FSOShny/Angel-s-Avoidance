using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GamePlayingButton : MonoBehaviour,
    IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Image image;
    private GamePlayingDirector director;

    private void Start()
    {
        // イメージコンポーネントを取得する
        image = GetComponent<Image>();

        // ゲームプレイディレクターを取得する
        director = GameObject.Find("Game Playing Director").GetComponent<GamePlayingDirector>();
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        // ボタンが使える状態でボタンを押すと
        if (director.CanUseButton)
        {
            // ボタンを灰色に変化させる
            image.color = Color.gray;

            // ポーズボタンであれば
            if (name == "Pause Button")
            {
                // ポーズ画面への遷移判定を有効にする
                director.PauseSwitch = true;
            }
            // コンティニューボタンであれば
            else if (name == "Continue Button")
            {
                // ゲームプレイの続行判定を有効にする
                director.ContinueSwitch = true;
            }
            // リスタートボタンであれば
            else if (name == "Restart Button")
            {
                // ゲームプレイの再開始判定を有効にする
                director.RestartSwitch = true;
            }
            // プラットフォームボタンであれば
            else if (name == "Platform Button")
            {
                // プラットフォーム画面への遷移判定を有効にする
                director.PlatformSwitch = true;
            }
            // パソコンボタンであれば
            else if (name == "PC Button")
            {
                // 現在のプラットフォームをパソコンに更新する
                StaticUnits.SmartPhone = false;

                // プラットフォーム画面への遷移判定を無効にする（ポーズ画面へ戻る）
                director.PlatformSwitch = false;
            }
            // スマホボタンであれば
            else if (name == "Smart Phone Button")
            {
                // 現在のプラットフォームをスマホに更新する
                StaticUnits.SmartPhone = true;

                // プラットフォーム画面への遷移判定を無効にする（ポーズ画面へ戻る）
                director.PlatformSwitch = false;
            }
            // クイットボタンであれば
            else if (name == "Quit Button")
            {
                // オープニングへの遷移判定を有効にする
                director.OpeningSwitch = true;
            }

            // ボタンを白色に変化させる（元の色に戻す）
            image.color = Color.white;
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        // ボタンが使える状態でボタンにカーソルを当てると
        if (director.CanUseButton)
        {
            // ボタンを白っぽい灰色に変化させる
            image.color = new Color(0.9f, 0.9f, 0.9f, 1.0f);
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        // ボタンが使える状態でボタンからカーソルを外すと
        if (director.CanUseButton)
        {
            // ボタンを白色に変化させる（元の色に戻す）
            image.color = Color.white;
        }
    }
}
