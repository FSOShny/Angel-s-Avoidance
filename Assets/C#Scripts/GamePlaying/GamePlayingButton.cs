using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GamePlayingButton : MonoBehaviour,
    IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Image image;
    private GamePlayingDirector director;
    private PlayerBehavior player;

    private void Start()
    {
        // イメージコンポーネントを取得する
        image = GetComponent<Image>();

        // ゲームプレイディレクターを取得する
        director = GameObject.FindGameObjectWithTag("Director").GetComponent<GamePlayingDirector>();

        // プレイヤービヘイビアを取得する
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>();
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        // ボタンを使える状態でボタンを入力すると
        if (director.CanUseButton)
        {
            // ボタンを灰色に変化させる
            image.color = Color.gray;

            // インタフェースが使える状態であり、プレイヤーが動ける状態であり
            if (director.CanUseInterf && player.CanMove)
            {
                // 上矢印ボタンであれば
            　　if (name == "Up Arrow Button")
                {
                    // 前（上）へ加速する
                    player.UpMove = true;
                }
                // 左矢印ボタンであれば
                else if (name == "Left Arrow Button")
                {
                    // 左へ加速する
                    player.LeftMove = true;
                }
                // 下矢印ボタンであれば
                else if (name == "Down Arrow Button")
                {
                    // 後（下）へ加速する
                    player.DownMove = true;
                }
                // 右矢印ボタンであれば
                else if (name == "Right Arrow Button")
                {
                    // 右へ加速する
                    player.RightMove = true;
                }
                // モードチェンジボタンであれば
                else if (name == "Mode Change Button")
                {
                    // 移動モードをチェンジする
                    director.ModeChange = !director.ModeChange;
                }

                // 疲労状態でなく
                if (!director.FatigueSwitch)
                {
                    // ガードアクションボタンであれば
                    if (name == "Guard Action Button")
                    {
                        // ガードアクションを実行できる状態にする
                        player.Guard = true;
                    }
                }
            }

            // ポーズボタンであれば
            if (name == "Pause Button")
            {
                // ポーズ画面への遷移を有効にする
                director.PauseSwitch = true;
            }
            // コンティニューボタンであれば
            else if (name == "Continue Button")
            {
                // ゲームプレイの続行を有効にする
                director.ContinueSwitch = true;

                // ボタンを白色に変化させる（元の色に戻す）
                image.color = Color.white;
            }
            // リスタートボタンであれば
            else if (name == "Restart Button")
            {
                // ゲームプレイの再開始を有効にする
                director.RestartSwitch = true;
            }
            // プラットフォームボタンであれば
            else if (name == "Platform Button")
            {
                // プラットフォーム画面への遷移を有効にする
                director.PlatformSwitch = true;

                // ボタンを白色に変化させる（元の色に戻す）
                image.color = Color.white;
            }
            // オープニングボタンであれば
            else if (name == "Opening Button")
            {
                // オープニングへの遷移を有効にする
                director.OpeningSwitch = true;
            }
            // パソコンボタンであれば
            else if (name == "PC Button")
            {
                // 現在のプラットフォームをパソコンに更新する
                StaticUnits.SmartPhone = false;

                // プラットフォーム画面への遷移を無効にする（ポーズ画面へ戻る）
                director.PlatformSwitch = false;

                // ボタンを白色に変化させる（元の色に戻す）
                image.color = Color.white;
            }
            // スマホボタンであれば
            else if (name == "Smart Phone Button")
            {
                // 現在のプラットフォームをスマホに更新する
                StaticUnits.SmartPhone = true;

                // プラットフォーム画面への遷移を無効にする（ポーズ画面へ戻る）
                director.PlatformSwitch = false;

                // ボタンを白色に変化させる（元の色に戻す）
                image.color = Color.white;
            }
        }
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        // ボタンを使える状態でボタンを離すと
        if (director.CanUseButton)
        {
            // 上矢印ボタンであれば
            if (name == "Up Arrow Button")
            {
                // 減速する
                player.UpMove = false;
            }
            // 左矢印ボタンであれば
            else if (name == "Left Arrow Button")
            {
                // 減速する
                player.LeftMove = false;
            }
            // 下矢印ボタンであれば
            else if (name == "Down Arrow Button")
            {
                // 減速する
                player.DownMove = false;
            }
            // 右矢印ボタンであれば
            else if (name == "Right Arrow Button")
            {
                // 減速する
                player.RightMove = false;
            }
        }

        // ボタンを白色に変化させる（元の色に戻す）
        image.color = Color.white;
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        // ボタンを使える状態でボタンにカーソルを当てると
        if (director.CanUseButton)
        {
            // ボタンを白っぽい灰色に変化させる
            image.color = new Color(0.9f, 0.9f, 0.9f, 1.0f);
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        // ボタンからカーソルを外すと

        // ボタンを白色に変化させる（元の色に戻す）
        image.color = Color.white;
    }
}
