using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float playerMoveSpeed = 10f; // プレイヤーの移動速度

    private Rigidbody rigid;
    private new Transform camera;
    private GamePlayingDirector director;
    private bool canMove = true; // 動ける状態であるかどうか
    private float hInput; // 前後（上下）の移動速度
    private float vInput; // 左右の移動速度
    private bool depthSwitch = true; // 移動タイプが「前後」かどうか
    private bool guardSwitch = false; // ガードアクションを実行するどうか
    private float stuckTime = 0f; // 行動不能時間
    private float invTime = 0f; // 無敵時間

    private void NormalAct(int Y, int Z)
    {
        rigid.MovePosition(transform.position +
             transform.right * hInput * Time.fixedDeltaTime +
             transform.up * vInput * Time.fixedDeltaTime * Y +
             transform.forward * vInput * Time.fixedDeltaTime * Z);
    }

    private void GuardAct()
    {
        // 行動不能時間を設定する
        stuckTime = 1.0f;

        // 行動可能状態を無効にする
        canMove = false;

        // 無敵時間を設定する
        invTime = 1.0f;
    }

    private void KnockBack(int X, int Y, int Z)
    {
        rigid.AddForce(X, Y, Z, ForceMode.VelocityChange);

        // プレイヤーの体力を減らす
        director.NowPlayerLife -= 1.0f;

        // 行動不能時間を設定する
        stuckTime = 2.0f;

        // 行動可能状態を無効にする
        canMove = false;

        // 無敵時間を設定する
        invTime = 4.0f;
    }

    private void Start()
    {
        // リジッドボディーコンポーネントを取得する
        rigid = GetComponent<Rigidbody>();

        // カメラの角度を取得
        camera = GameObject.Find("Main Camera").transform;

        // ゲームプレイディレクターを取得する
        director = GameObject.Find("Game Playing Director").GetComponent<GamePlayingDirector>();
    }

    private void Update()
    {
        // W, S, ↑, ↓キーでプレイヤーの前後（上下）の移動量を求める
        hInput = Input.GetAxis("Horizontal") * playerMoveSpeed;

        // A, D, ←, →キーでプレイヤーの左右の移動量を求める
        vInput = Input.GetAxis("Vertical") * playerMoveSpeed;

        if (director.CanUseInterf)
        {
            // Eキーで移動タイプを変更する
            if (Input.GetKeyDown(KeyCode.E))
            {
                depthSwitch = !depthSwitch;
            }

            // スペースキーでガードアクションを有効にする
            if (Input.GetKeyDown(KeyCode.Space))
            {
                guardSwitch = true;
            }
        }

        // 行動不能時間中は
        if (stuckTime > 0f)
        {
            // 時間を経過させる
            stuckTime -= Time.deltaTime;
        }
        // 行動不能時間終了時は
        else if (stuckTime < 0f)
        {
            // 時間を初期化する（正常な処理のため）
            stuckTime = 0f;

            // プレイヤーにかかっている速度をゼロにする
            rigid.velocity = Vector3.zero;

            // 行動可能判定を有効にする
            canMove = true;
        }

        // 無敵時間中は
        if (invTime > 0f)
        {
            // 時間を経過させる
            invTime -= Time.deltaTime;
        }
        // 無敵時間終了時は
        else if (invTime < 0f)
        {
            // 時間を初期化する（正常な処理のため）
            invTime = 0f;
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            // 移動タイプが「前後」であれば
            if (depthSwitch)
            {
                // プレイヤーを前後左右に移動させる
                NormalAct(0, 1);
            }
            // 移動タイプが「上下」であれば
            else
            {
                // プレイヤーを上下左右に移動させる
                NormalAct(1, 0);
            }

            // ガードアクションが有効である場合は
            if (guardSwitch)
            {
                guardSwitch = false;

                // ガードアクションを実行させる
                GuardAct();
            }
        }

        // プレイヤーの角度をカメラの角度に合わせる
        rigid.rotation = camera.rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 無敵時間外であり
        if (invTime == 0)
        {
            /* ゾーンやエネミーに衝突するとプレイヤーがノックバックする */
            if (collision.gameObject.name == "Bottom")
            {
                KnockBack(0, 1, 0);
            }
            else if (collision.gameObject.name == "Front")
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
            else if (collision.gameObject.name == "EnemyPrefab(Clone)")
            {
                KnockBack(0, 0, 0);
            }
        }
    }
}
