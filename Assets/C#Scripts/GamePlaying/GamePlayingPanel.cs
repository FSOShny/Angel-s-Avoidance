using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GamePlayingPanel : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public float cameRotSpeed = 0.3f; // カメラの回転速度

    private Transform came;
    private GamePlayingDirector director;
    private int swipe = 0; // スワイプ係数
    private Vector2 lastFingerPos; // 以前の指の位置
    private Vector2 newCameAng; // カメラの角度

    private void Start()
    {
        // カメラの角度を取得する
        came = GameObject.FindGameObjectWithTag("Camera").transform;

        // ゲームプレイディレクターを取得する
        director = GameObject.FindGameObjectWithTag("Director").GetComponent<GamePlayingDirector>();
    }

    private void LateUpdate()
    {
        // スワイプ係数が1であれば
        if (swipe == 1)
        {
            // 最新のカメラの角度を設定する
            newCameAng = came.transform.localEulerAngles;
        }
        // スワイプ係数が2であれば
        else if (swipe == 2)
        {
            // カメラを回転させる
            came.transform.localEulerAngles = newCameAng;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // インタフェースを使える状態で画面をスワイプしようとすると
        if (director.CanUseInterf)
        {
            // スワイプ係数を設定する
            swipe = 1;

            // 現在の指の位置を設定する
            lastFingerPos = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        // インタフェースを使える状態で画面をスワイプすると
        if (director.CanUseInterf)
        {
            // スワイプ係数を設定する
            swipe = 2;

            // 以前の指の位置と現在の指の位置からカメラの回転量を求める
            newCameAng.x += (lastFingerPos.y - eventData.position.y) * cameRotSpeed * StaticUnits.Reverse;
            newCameAng.y += (eventData.position.x - lastFingerPos.x) * cameRotSpeed * StaticUnits.Reverse;

            // 現在の指の位置を設定する
            lastFingerPos = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // スワイプ係数を初期化する
        swipe = 0;
    }
}
