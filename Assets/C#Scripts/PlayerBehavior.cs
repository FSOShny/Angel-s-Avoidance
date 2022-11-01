using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float moveSpeed = 10f; // プレイヤーの移動速度

    private new Camera camera;
    private Rigidbody rigid;
    private float hInput;
    private float vInput;
    private bool depthSwitch = true;
    private bool moveCan = true;
    private float invTime = 0;

    private void Start()
    {
        // カメラの角度を取得
        camera = Camera.main;

        // リジッドボディーコンポーネントを取得する
        rigid = GetComponent<Rigidbody>();
    }

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

        if (invTime > 0) // 無敵時間中は
        {
            // 無敵時間を更新する
            invTime -= Time.deltaTime;
        }
        else if (invTime < 0) // 無敵時間終了時は
        {
            // プレイヤーにかかっている速度をゼロにする
            rigid.velocity = Vector3.zero;

            // 動ける状態にする
            moveCan = true;
        }
    }

    private void FixedUpdate()
    {
        if (moveCan) // 動ける状態であり
        {
            if (depthSwitch) // 移動タイプが「前後」である場合は
            {
                // プレイヤーを前後左右に移動させる
                NormalMove(0, 1);
            }
            else // 移動タイプが「上下」である場合は
            {
                // プレイヤーを上下左右に移動させる
                NormalMove(1, 0);
            }
        }

        // プレイヤーの角度をカメラの角度に合わせる
        transform.rotation = camera.transform.rotation;
    }

    public void NormalMove(int Y, int Z)
    {
         rigid.MovePosition(transform.position + 
             transform.right * hInput * Time.fixedDeltaTime + 
             transform.up * vInput * Time.fixedDeltaTime * Y + 
             transform.forward * vInput * Time.fixedDeltaTime * Z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ダメージゾーンに衝突するとプレイヤーがノックバックする
        if (collision.gameObject.name == "Front")
        {
            KnockBack(0, 0, -1);
        }
        else if (collision.gameObject.name == "Left")
        {
            KnockBack(1, 0, 0);
        }
        else if (collision.gameObject.name == "Back")
        {
            KnockBack(0, 0, 1);
        }
        else if (collision.gameObject.name == "Right")
        {
            KnockBack(-1, 0, 0);
        }
        else if (collision.gameObject.name == "Top")
        {
            KnockBack(0, -1, 0);
        }
        else if (collision.gameObject.name == "Enemy(Clone)")
        {
            KnockBack(0, 0, 0);
        }
    }

    public void KnockBack(int X, int Y, int Z)
    {
        rigid.AddForce(new Vector3(X, Y, Z), ForceMode.Impulse);
        invTime = 2;
        moveCan = false;
    }
}