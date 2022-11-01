using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public float rotateSpeed = 0.2f; // カメラの回転速度

    private Transform player;
    private Vector2 lastMousePos;
    private Vector2 newAngle;

    // Start is called before the first frame update
    private void Start()
    {
        // プレイヤーの位置を取得する
        player = GameObject.Find("Player").transform;
    }

    private void LateUpdate()
    {
        // カメラの位置をプレイヤーの位置に合わせる
        transform.position = player.position;

        if (Input.GetMouseButtonDown(0)) // マウスを左クリックすると
        {
            // 現在のカメラの角度を格納する
            newAngle = transform.localEulerAngles;

            // 現在のマウスの位置を格納する
            lastMousePos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0)) // そのまま左クリックを続けていると
        {
            // 格納されているマウスの位置と現在のマウスの位置からカメラの回転量を求める
            newAngle.x += (lastMousePos.y - Input.mousePosition.y) * rotateSpeed;
            newAngle.y += (Input.mousePosition.x - lastMousePos.x) * rotateSpeed;

            // カメラを回転させる
            transform.localEulerAngles = newAngle;

            // 格納するマウスの位置を更新する
            lastMousePos = Input.mousePosition;
        }
    }
}
