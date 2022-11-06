using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float moveSpeed = 10f; // プレイヤーの移動速度

    private Rigidbody rigid;
    private new Camera camera;
    private GameDirector game;
    private float hInput;
    private float vInput;
    private bool depthSwitch = true;
    private bool guardSwitch = false;
    private bool moveCan = true;
    private float invTime = 0f;
    private float stuckTime = 0f;

    private void Start()
    {
        // リジッドボディーコンポーネントを取得する
        rigid = GetComponent<Rigidbody>();

        // カメラの角度を取得
        camera = Camera.main;

        // ゲームディレクターを有効にする
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

        // ガードを「有効」にする
        if (Input.GetKeyDown(KeyCode.Space))
        {
            guardSwitch = true;
        }

        if (invTime > 0f) // 無敵時間中は
        {
            // 無敵時間を更新する
            invTime -= Time.deltaTime;
        }
        else if (invTime <= 0f) // 無敵時間終了時は
        {
            // 衝突判定を正常にするためにゼロを代入する
            invTime = 0f;
        }

        if (stuckTime > 0f) // 行動不能時間中は
        {
            // 行動不能時間を更新する
            stuckTime -= Time.deltaTime;
        }
        else if (stuckTime <= 0f) // 行動不能時間終了時は
        {
            // プレイヤーにかかっている速度をゼロにする
            rigid.velocity = Vector3.zero;

            // 行動可能にする
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

            if (guardSwitch) // ガードが「有効」である場合は
            {
                // ガードアクションを実行させる
                GuardMove();
            }
        }

        // プレイヤーの角度をカメラの角度に合わせる
        rigid.rotation = camera.transform.rotation;
    }

    public void NormalMove(int Y, int Z)
    {
        rigid.MovePosition(transform.position + 
             transform.right * hInput * Time.fixedDeltaTime + 
             transform.up * vInput * Time.fixedDeltaTime * Y + 
             transform.forward * vInput * Time.fixedDeltaTime * Z);
    }

    public void GuardMove()
    {
        invTime = 1f;
        stuckTime = 1f;
        moveCan = false;
        guardSwitch = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (invTime == 0) // 無敵時間がゼロである場合に
        {
            // ダメージゾーンやエネミーに衝突するとプレイヤーがノックバックする
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
        rigid.AddForce(X, Y, Z, ForceMode.VelocityChange);
        game.Life -= 1f;
        invTime = 4f;
        stuckTime = 2f;
        moveCan = false;
    }
}
