using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public float cameraRotSpeed = 0.2f; // カメラの回転速度

    private Transform player;
    private GamePlayingDirector director;
    private Vector2 lastMousePos; // 以前のマウス位置
    private Vector2 newCameraAng; // カメラの角度

    private void Start()
    {
        // プレイヤーの位置を取得する
        player = GameObject.Find("Player").transform;

        // ゲームプレイディレクターを取得する
        director = GameObject.Find("Game Playing Director").GetComponent<GamePlayingDirector>();
    }

    private void LateUpdate()
    {
        // カメラの位置をプレイヤーの位置に合わせる
        transform.position = player.position;

        // カメラの回転方向を決める
        cameraRotSpeed *= StaticUnits.Direction;

        if (director.CanUseInterf)
        {
            // マウスを左クリックした場合は
            if (Input.GetMouseButtonDown(0))
            {
                // 最新のカメラの角度を格納する
                newCameraAng = transform.localEulerAngles;

                // 現在のマウス位置を格納する
                lastMousePos = Input.mousePosition;
            }
            // そのまま左クリックを続けた場合は
            else if (Input.GetMouseButton(0))
            {
                // 以前のマウス位置と現在のマウス位置からカメラの回転量を求める
                newCameraAng.x += (lastMousePos.y - Input.mousePosition.y) * cameraRotSpeed;
                newCameraAng.y += (Input.mousePosition.x - lastMousePos.x) * cameraRotSpeed;

                // カメラを回転させる
                transform.localEulerAngles = newCameraAng;

                // 現在のマウス位置を格納する
                lastMousePos = Input.mousePosition;
            }
        }
    }
}
