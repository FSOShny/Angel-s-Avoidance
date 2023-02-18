using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GamePlayingPanel : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // カメラの回転速度
    public float cameRotSpeed = 0.3f;

    // カメラの変位
    private Transform came;

    // ディレクターコンポーネント
    private GamePlayingDirector director;

    // スワイプ係数
    private int swipe = 0;

    // 以前の指の位置
    private Vector2 lastFingerPos;

    // 現在のカメラの角度
    private Vector2 newCameAng;

    private void Start()
    {
        // 各コンポーネントを取得する
        came = GameObject.FindGameObjectWithTag("Camera").transform;
        director = GameObject.FindGameObjectWithTag("Director").GetComponent<GamePlayingDirector>();
    }

    private void LateUpdate()
    {
        if (swipe == 1)
        {
            /* スワイプ係数が1であるときは
               現在のカメラの角度を設定する */

            newCameAng = came.transform.localEulerAngles;
        }
        else if (swipe == 2)
        {
            /* スワイプ係数が2であるときは
               カメラを回転させる          */

            came.transform.localEulerAngles = newCameAng;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // （インタフェースが使用可能であるときは有効）
        if (director.CanUseInterf)
        {
            /* 画面をスワイプし始めると
               スワイプ係数を1に設定する */

            swipe = 1;

            // 現在の指の位置を設定する
            lastFingerPos = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        // （インタフェースが使用可能であるときは有効）
        if (director.CanUseInterf)
        {
            /* そのまま画面をスワイプし続けていると
               スワイプ係数を2に設定する            */

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
        // 画面をスワイプし終えると
        // スワイプ係数を0に設定する
        swipe = 0;
    }
}
