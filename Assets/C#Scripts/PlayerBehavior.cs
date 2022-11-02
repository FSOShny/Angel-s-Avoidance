using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerBehavior : MonoBehaviour
{
    public float moveSpeed = 10f; // プレイヤーの移動速度

    private new Camera camera;
    private Rigidbody rigid;
    private GameDirector game;
    private float hInput;
    private float vInput;
    private bool depthSwitch = true;
    private bool avoidSwitch = false;
    private bool moveCan = true;
    private Vector3 moveDir = new Vector3(0f, 0f, 1f);
    private float invTime = 0f;

    private void Start()
    {
        // カメラの角度を取得
        camera = Camera.main;

        // リジッドボディーコンポーネントを取得する
        rigid = GetComponent<Rigidbody>();

        game = GameObject.Find("Game Director").GetComponent<GameDirector>();
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

        if (Input.GetKeyDown(KeyCode.F))
        {
            avoidSwitch = true;
        }

        if (invTime > 0f) // 無敵時間中は
        {
            // 無敵時間を更新する
            invTime -= Time.deltaTime;
        }
        else if (invTime <= 0f) // 無敵時間終了時は
        {
            invTime = 0f;

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

                if (avoidSwitch)
                {
                    AvoidMove(0, 1);
                }
            }
            else // 移動タイプが「上下」である場合は
            {
                // プレイヤーを上下左右に移動させる
                NormalMove(1, 0);

                if (avoidSwitch)
                {
                    AvoidMove(1, 0);
                }
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

    public void AvoidMove(int Y, int Z)
    {
        rigid.MovePosition(transform.position +
             transform.right * hInput * Time.fixedDeltaTime * 10 +
             transform.up * vInput * Time.fixedDeltaTime * Y  * 10 +
             transform.forward * vInput * Time.fixedDeltaTime * Z * 10 );
        invTime = 1f;
        moveCan = false;
        avoidSwitch = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (invTime == 0) // 無敵時間がゼロである場合に
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
    }

    public void KnockBack(int X, int Y, int Z)
    {
        rigid.AddForce(new Vector3(X, Y, Z), ForceMode.Impulse);
        game.Life -= 1f;
        invTime = 2;
        moveCan = false;
    }
}