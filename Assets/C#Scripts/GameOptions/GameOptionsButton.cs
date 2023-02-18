using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameOptionsButton : MonoBehaviour,
    IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    // スプライト
    [SerializeField] private Sprite purpleButton;
    [SerializeField] private Sprite yellowButton;
    [SerializeField] private Sprite whiteButton;

    // コンポーネント（イメージ、ディレクター）
    private Image image;
    private GameOptionsDirector director;

    private void Start()
    {
        // 各コンポーネントを取得する
        image = GetComponent<Image>();
        director = GameObject.FindGameObjectWithTag("Director").GetComponent<GameOptionsDirector>();
    }

    private void Update()
    {
        if ((name == "30 Seconds Button" && StaticUnits.GameTime == 30f) || 
            (name == "45 Seconds Button" && StaticUnits.GameTime == 45f) || 
            (name == "60 Seconds Button" && StaticUnits.GameTime == 60f) || 
            (name == "Low Speed Button" && StaticUnits.EnemyMoveSpeed == 4) || 
            (name == "Normal Speed Button" && StaticUnits.EnemyMoveSpeed == 8) || 
            (name == "High Speed Button" && StaticUnits.EnemyMoveSpeed == 12) || 
            (name == "5 Lives Button" && StaticUnits.MaxPlayerLives == 5) || 
            (name == "3 Lives Button" && StaticUnits.MaxPlayerLives == 3) || 
            (name == "2 Lives Button" && StaticUnits.MaxPlayerLives == 2) || 
            (name == "Reverse On Button" && StaticUnits.Reverse == -1) || 
            (name == "Reverse Off Button" && StaticUnits.Reverse == 1))
        {
            /* 有効になっているオプションボタンは紫色にする */

            image.sprite = purpleButton;
        }
        else if (name == "Opening Button")
        {
            /* オープニングボタンは黄色にする */

            image.sprite = yellowButton;
        }
        else
        {
            /* 上記に該当しないボタンは白色にする */

            image.sprite = whiteButton;
        }
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        // ボタンを入力すると
        // そのボタンの明るさを暗くする（程度：中）
        image.color = Color.gray;

        // （ボタンごとに振る舞いを変える）
        if (name == "30 Seconds Button")
        {
            /* ゲームの時間を「30秒」にする */

            StaticUnits.GameTime = 30f;
        }
        else if (name == "45 Seconds Button")
        {
            /* ゲームの時間を「45秒」にする */

            StaticUnits.GameTime = 45f;
        }
        else if (name == "60 Seconds Button")
        {
            /* ゲームの時間を「60秒」にする */

            StaticUnits.GameTime = 60f;
        }
        else if (name == "Low Speed Button")
        {
            /* エネミーの移動速度を「低速」にする */

            StaticUnits.EnemyMoveSpeed = 4;
        }
        else if (name == "Normal Speed Button")
        {
            /* エネミーの移動速度を「通常」にする */

            StaticUnits.EnemyMoveSpeed = 8;
        }
        else if (name == "High Speed Button")
        {
            /* エネミーの移動速度を「高速」にする */

            StaticUnits.EnemyMoveSpeed = 12;
        }
        else if (name == "5 Lives Button")
        {
            /* プレイヤーの体力最大値を「5」にする */

            StaticUnits.MaxPlayerLives = 5;
        }
        else if (name == "3 Lives Button")
        {
            /* プレイヤーの体力最大値を「3」にする */

            StaticUnits.MaxPlayerLives = 3;
        }
        else if (name == "2 Lives Button")
        {
            /* プレイヤーの体力最大値を「2」にする */

            StaticUnits.MaxPlayerLives = 2;
        }
        else if (name == "Reverse On Button")
        {
            /* カメラの反転係数を「オン」にする */

            StaticUnits.Reverse = -1;
        }
        else if (name == "Reverse Off Button")
        {
            /* カメラの反転係数を「オフ」にする */

            StaticUnits.Reverse = 1;
        }
        else if (name == "Opening Button")
        {
            /* オープニングへ移動する */

            director.OpeningSwitch = true;
        }
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        // ボタンを離すと
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
