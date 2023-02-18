using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    // カメラの回転速度
    public float cameRotSpeed = 0.1f;

    // プレイヤーの変位
    private Transform player;

    // ディレクターコンポーネント
    private GamePlayingDirector director;

    // 以前のマウスの位置
    private Vector2 lastMousePos;

    // 現在のカメラの角度
    private Vector2 newCameAng;

    private void Start()
    {
        // 各コンポーネントを取得する
        player = GameObject.Find("Player").transform;
        director = GameObject.FindGameObjectWithTag("Director").GetComponent<GamePlayingDirector>();
    }

    private void LateUpdate()
    {
        // カメラの位置をプレイヤーの位置に合わせる
        transform.position = player.position;

        // （インタフェースが使用可能である、かつ
        //   現在のプラットフォームがパソコンであるときは有効）
        if (director.CanUseInterf && !StaticUnits.SmartPhone)
        {
            if (Input.GetMouseButtonDown(0))
            {
                /* マウスを左クリックし始めると
                   現在のカメラの角度を設定する */

                newCameAng = transform.localEulerAngles;

                // 現在のマウスの位置を設定する
                lastMousePos = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                /* そのままマウスを左クリックし続けると
                   以前のマウスの位置と現在のマウスの位置からカメラの回転量を求める */

                newCameAng.x += (lastMousePos.y - Input.mousePosition.y) * cameRotSpeed * StaticUnits.Reverse;
                newCameAng.y += (Input.mousePosition.x - lastMousePos.x) * cameRotSpeed * StaticUnits.Reverse;

                // カメラを回転させる
                transform.localEulerAngles = newCameAng;

                // 現在のマウスの位置を設定する
                lastMousePos = Input.mousePosition;
            }
        }
    }
}
