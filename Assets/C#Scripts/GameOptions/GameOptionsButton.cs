using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameOptionsButton : MonoBehaviour,
    IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Sprite purpleButton;
    [SerializeField] private Sprite yellowButton;
    [SerializeField] private Sprite whiteButton;

    private Image image;
    private GameOptionsDirector director;

    private void Start()
    {
        // イメージコンポーネントを取得する
        image = GetComponent<Image>();

        // オープニングディレクターを取得する
        director = GameObject.FindGameObjectWithTag("Director").GetComponent<GameOptionsDirector>();
    }

    private void Update()
    {
        // 有効になっているオプションボタンであれば
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
            // 紫色のボタンにする
            image.sprite = purpleButton;
        }
        // オープニングボタンであれば
        else if (name == "Opening Button")
        {
            // 黄色のボタンにする
            image.sprite = yellowButton;
        }
        // それ以外のボタンであれば
        else
        {
            // 白色のボタンにする
            image.sprite = whiteButton;
        }
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        // ボタンを入力すると

        // ボタンを灰色に変化させる
        image.color = Color.gray;

        // 30秒ボタンであれば
        if (name == "30 Seconds Button")
        {
            // ゲームの時間を「30秒」にする
            StaticUnits.GameTime = 30f;
        }
        // 45秒ボタンであれば
        else if (name == "45 Seconds Button")
        {
            // ゲームの時間を「45秒」にする
            StaticUnits.GameTime = 45f;
        }
        // 60秒ボタンであれば
        else if (name == "60 Seconds Button")
        {
            // ゲームの時間を「60秒」にする
            StaticUnits.GameTime = 60f;
        }
        // 低速度ボタンであれば
        else if (name == "Low Speed Button")
        {
            // エネミーの移動速度を「低速度」にする
            StaticUnits.EnemyMoveSpeed = 4;
        }
        // 通常速度ボタンであれば
        else if (name == "Normal Speed Button")
        {
            // エネミーの移動速度を「通常速度」にする
            StaticUnits.EnemyMoveSpeed = 8;
        }
        // 高速度ボタンであれば
        else if (name == "High Speed Button")
        {
            // エネミーの移動速度を「高速度」にする
            StaticUnits.EnemyMoveSpeed = 12;
        }
        // 5ライフボタンであれば
        else if (name == "5 Lives Button")
        {
            // プレイヤーの体力最大値を「5」にする
            StaticUnits.MaxPlayerLives = 5;
        }
        // 3ライフボタンであれば
        else if (name == "3 Lives Button")
        {
            // プレイヤーの体力最大値を「3」にする
            StaticUnits.MaxPlayerLives = 3;
        }
        // 2ライフボタンであれば
        else if (name == "2 Lives Button")
        {
            // プレイヤーの体力最大値を「2」にする
            StaticUnits.MaxPlayerLives = 2;
        }
        // 反転オンボタンであれば
        else if (name == "Reverse On Button")
        {
            // カメラの反転係数を「オン」にする
            StaticUnits.Reverse = -1;
        }
        // 反転オフボタンであれば
        else if (name == "Reverse Off Button")
        {
            // カメラの反転係数を「オフ」にする
            StaticUnits.Reverse = 1;
        }
        // オープニングボタンであれば
        else if (name == "Opening Button")
        {
            // オープニングへの遷移判定を有効にする
            director.OpeningSwitch = true;
        }
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        // ボタンを離すと

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
