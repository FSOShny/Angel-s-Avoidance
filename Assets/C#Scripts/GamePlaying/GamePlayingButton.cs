using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GamePlayingButton : MonoBehaviour,
    IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    // コンポーネント（イメージ、ディレクター、プレイヤー）
    private Image image;
    private GamePlayingDirector director;
    private PlayerBehavior player;

    private void Start()
    {
        // 各コンポーネントを取得する
        image = GetComponent<Image>();
        director = GameObject.FindGameObjectWithTag("Director").GetComponent<GamePlayingDirector>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>();
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        // ボタンを入力すると
        // そのボタンの明るさを暗くする（程度：中）
        // （ボタンが使用可能であるときは有効）
        if (director.CanUseButton)
        {
            image.color = Color.gray;

            // （インタフェースが使用可能である、かつ
            //   プレイヤーが移動可能であるときは有効）
            if (director.CanUseInterf && player.CanMove)
            {
                // （ボタンごとに振る舞いを変える）
                if (name == "Up Arrow Button")
                {
                    /* 前（上）への加速を有効にする */

                    player.UpMove = true;
                }
                else if (name == "Left Arrow Button")
                {
                    /* 左への加速を有効にする */

                    player.LeftMove = true;
                }
                else if (name == "Down Arrow Button")
                {
                    /* 後（下）への加速を有効にする */

                    player.DownMove = true;
                }
                else if (name == "Right Arrow Button")
                {
                    /* 右への加速を有効にする */

                    player.RightMove = true;
                }
                else if (name == "Mode Change Button")
                {
                    /* 移動モードをチェンジする */

                    director.ModeChange = !director.ModeChange;
                }

                // （プレイヤーが疲労状態であるときは無効）
                if (!director.FatigueSwitch)
                {
                    if (name == "Guard Action Button")
                    {
                        /* ガードアクションを行う（準備をする） */

                        player.Guard = true;
                    }
                }
            }

            // （ボタンごとに振る舞いを変える）
            if (name == "Pause Button")
            {
                /* ポーズ画面を有効にする */

                director.PauseSwitch = true;
            }
            else if (name == "Continue Button")
            {
                /* ポーズ画面を無効にする */

                director.ContinueSwitch = true;

                // ボタンの明るさを元に戻す
                image.color = Color.white;
            }
            else if (name == "Restart Button")
            {
                /* ゲームプレイを再開始する */

                director.RestartSwitch = true;
            }
            // プラットフォームボタンであれば
            else if (name == "Platform Button")
            {
                /* プラットフォーム画面を有効にする */

                director.PlatformSwitch = true;

                // ボタンの明るさを元に戻す
                image.color = Color.white;
            }
            else if (name == "Opening Button")
            {
                /* オープニングへ移動する */

                director.OpeningSwitch = true;
            }
            else if (name == "PC Button")
            {
                /* 現在のプラットフォームをパソコンにする */

                StaticUnits.SmartPhone = false;

                // プラットフォーム画面を無効にし、
                // ボタンの明るさを元に戻す
                director.PlatformSwitch = false;
                image.color = Color.white;
            }
            else if (name == "Smart Phone Button")
            {
                /* 現在のプラットフォームをスマホにする */

                StaticUnits.SmartPhone = true;

                // プラットフォーム画面を無効にし、
                // ボタンの明るさを元に戻す
                director.PlatformSwitch = false;
                image.color = Color.white;
            }
        }
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        // ボタンを離すと
        // そのボタンの明るさを元に戻す
        image.color = Color.white;

        // （ボタンが使用可能であるときは有効）
        if (director.CanUseButton)
        {
            // （ボタンごとに振る舞いを変える）
            if (name == "Up Arrow Button")
            {
                /* 前（上）への加速を無効にする */

                player.UpMove = false;
            }
            else if (name == "Left Arrow Button")
            {
                /* 左への加速を無効にする */

                player.LeftMove = false;
            }
            else if (name == "Down Arrow Button")
            {
                /* 後（下）への加速を無効にする */

                player.DownMove = false;
            }
            else if (name == "Right Arrow Button")
            {
                /* 右への加速を無効にする */

                player.RightMove = false;
            }
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        // ボタンにカーソルを当てると
        // そのボタンの明るさを暗くする（程度：小）
        // （ボタンが使用可能であるときは有効）
        if (director.CanUseButton)
        {
            image.color = new Color(0.9f, 0.9f, 0.9f, 1.0f);
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        // ボタンからカーソルを外すと
        // そのボタンの明るさを元に戻す
        image.color = Color.white;
    }
}
