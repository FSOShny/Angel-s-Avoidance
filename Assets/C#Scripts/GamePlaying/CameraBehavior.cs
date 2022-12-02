using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public float cameRotSpeed = 0.1f; // カメラの回転速度

    private Transform player;
    private GamePlayingDirector director;
    private Vector2 lastMousePos; // 以前のマウス位置
    private Vector2 newCameAng; // カメラの角度

    private void Start()
    {
        // プレイヤーの位置を取得する
        player = GameObject.Find("Player").transform;

        // ゲームプレイディレクターを取得する
        director = GameObject.FindGameObjectWithTag("Director").GetComponent<GamePlayingDirector>();
    }

    private void LateUpdate()
    {
        // カメラの位置をプレイヤーの位置に合わせる
        transform.position = player.position;

        // 現在のプラットフォームがパソコンであり
        if (!StaticUnits.SmartPhone)
        {
            // インタフェースを使える状態で
            if (director.CanUseInterf)
            {
                // マウスを左クリックし始めると
                if (Input.GetMouseButtonDown(0))
                {
                    // 最新のカメラの角度を設定する
                    newCameAng = transform.localEulerAngles;

                    // 現在のマウス位置を設定する
                    lastMousePos = Input.mousePosition;
                }
                // そのまま左クリックし続けると
                else if (Input.GetMouseButton(0))
                {
                    // 以前のマウス位置と現在のマウス位置からカメラの回転量を求める
                    newCameAng.x += (lastMousePos.y - Input.mousePosition.y) * cameRotSpeed * StaticUnits.Reverse;
                    newCameAng.y += (Input.mousePosition.x - lastMousePos.x) * cameRotSpeed * StaticUnits.Reverse;

                    // カメラを回転させる
                    transform.localEulerAngles = newCameAng;

                    // 現在のマウス位置を設定する
                    lastMousePos = Input.mousePosition;
                }
            }
        }
    }
}
