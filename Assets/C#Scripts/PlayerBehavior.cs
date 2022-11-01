using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float moveSpeed = 10f; // プレイヤーの移動速度

    private new Camera camera;
    private Rigidbody rb;
    private float hInput;
    private float vInput;
    private bool depthSwitch = true;

    // Start is called before the first frame update
    private void Start()
    {
        // カメラの角度を取得
        camera = Camera.main;
        // リジッドボディーコンポーネントを取得する
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        // プレイヤーの前後（上下）の移動量を求める
        hInput = Input.GetAxis("Horizontal") * moveSpeed;
        // プレイヤーの左右の移動量を求める
        vInput = Input.GetAxis("Vertical") * moveSpeed;

        // 移動タイプを変更する
        if (Input.GetKeyDown(KeyCode.E))
        {
            depthSwitch = !depthSwitch;
        }
    }

    private void FixedUpdate()
    {
        if (depthSwitch) // 移動タイプが「前後」である場合
        {
            NormalMove(0, 1);
        }
        else // 移動タイプが「上下」である場合
        {
            NormalMove(1, 0);
        }

        // プレイヤーの角度をカメラの角度に合わせる
        transform.rotation = camera.transform.rotation;
    }

    public void NormalMove(int Y, int Z)
    {
        // プレイヤーを移動させる
        rb.MovePosition(transform.position +
                transform.right * hInput * Time.fixedDeltaTime +
                transform.up * vInput * Time.fixedDeltaTime * Y +
                transform.forward * vInput * Time.fixedDeltaTime * Z);
    }
}
