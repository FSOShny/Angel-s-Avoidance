using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float playerMoveSpeed = 10f; // プレイヤーの移動量係数

    [SerializeField] private GameObject playerAura;

    private Rigidbody rigid;
    private Animator anim;
    private Material playerMat;
    private Transform came;
    private GamePlayingDirector director;
    private float invTime = 0f; // 無敵時間
    private int damage = 1; // プレイヤーの被弾係数
    private int penaSpeed = 1; // プレイヤーの移動速度ペナルティ
    private float stuckTime = 0f; // 行動不能時間
    private float hPlayerVelo = 0f; // プレイヤーの左右の加速度
    private float vPlayerVelo = 0f; // プレイヤーの前後（上下）の加速度
    private float hPlayerMove; // プレイヤーの左右の移動量
    private float vPlayerMove; // プレイヤーの前後（上下）の移動量

    // プレイヤーが動ける状態かどうか
    private bool canMove = true;

    public bool CanMove
    {
        get { return canMove; }
        set { canMove = value; }
    }

    // 右矢印ボタンを入力しているかどうか
    private bool rightMove = false;

    public bool RightMove
    {
        get { return rightMove; }
        set { rightMove = value; }
    }

    // 左矢印ボタンを入力しているかどうか
    private bool leftMove = false;

    public bool LeftMove
    {
        get { return leftMove; }
        set { leftMove = value; }
    }

    // 上矢印ボタンを入力しているかどうか
    private bool upMove = false;

    public bool UpMove
    {
        get { return upMove; }
        set { upMove = value; }
    }

    // 下矢印ボタンを入力しているかどうか
    private bool downMove = false;

    public bool DownMove
    {
        get { return downMove; }
        set { downMove = value; }
    }

    // ガードアクションを実行できる状態かどうか
    private bool guard = false;

    public bool Guard
    {
        get { return guard; }
        set { guard = value; }
    }

    private void Start()
    {
        // リジッドボディーコンポーネントを取得する
        rigid = GetComponent<Rigidbody>();

        // プレイヤーのアニメーターを取得する
        anim = GameObject.Find("MorroMan_Idle_MoCap").GetComponent<Animator>();

        // プレイヤーの色を取得する
        playerMat = GameObject.Find("MHuman").GetComponent<Renderer>().material;

        // カメラの角度を取得
        came = GameObject.Find("Camera").transform;

        // ゲームプレイディレクターを取得する
        director = GameObject.FindGameObjectWithTag("Director").GetComponent<GamePlayingDirector>();

        // プレイヤーオーラを無効にする
        playerAura.SetActive(false);

        // プレイヤーのアニメーターを止める
        anim.speed = 0f;
    }

    private void Update()
    {
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

            // 疲労状態でなければ
            if (!director.FatigueSwitch)
            {
                // プレイヤーの色を元に戻す
                playerMat.color = Color.white;
            }

            // プレイヤーバリアを無効にする
            playerAura.SetActive(false);
        }

        // 気力回復時間中は
        if (director.ChargeTime > 0f)
        {
            // 時間を経過させる
            director.ChargeTime -= Time.deltaTime;
        }
        // 気力回復時間終了時は
        else if (director.ChargeTime < 0f)
        {
            // 時間を初期化する（正常な処理のため）
            director.ChargeTime = 0f;

            // ペナルティを無効にする
            damage = 1;
            penaSpeed = 1;

            // 通常状態にする
            director.FatigueSwitch = false;

            // 無敵時間外であれば
            if (invTime == 0)
            {
                // プレイヤーの色を元に戻す
                playerMat.color = Color.white;
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

            // プレイヤーが動ける状態にする
            canMove = true;
        }

        // インタフェースが使える状態であり、プレイヤーが動ける状態で
        if (director.CanUseInterf && canMove)
        {
            // 右インタフェース（Dキー, 右矢印ボタン）を入力すると
            if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) || rightMove)
            {
                // 右へ加速する
                hPlayerVelo = 1.0f;
                vPlayerVelo = 0f;
            }
            // 左インタフェース（Aキー, 左矢印ボタン）を入力すると
            else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) || leftMove)
            {
                // 左へ加速する
                hPlayerVelo = -1.0f;
                vPlayerVelo = 0f;
            }
            // 前（上）インタフェース（Wキー, 上矢印ボタン）を入力すると
            else if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) || upMove)
            {
                // 前（上）へ加速する
                vPlayerVelo = 1.0f;
                hPlayerVelo = 0f;
            }
            // 後（下）インタフェース（Sキー, 下矢印ボタン）を入力すると
            else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) || downMove)
            {
                // 後（下）へ加速する
                vPlayerVelo = -1.0f;
                hPlayerVelo = 0f;
            }
            // インタフェースを入力しないと
            else
            {
                // 減速する
                hPlayerVelo *= 0.5f;
                vPlayerVelo *= 0.5f;
            }

            // プレイヤーの左右の移動量を決める
            hPlayerMove = hPlayerVelo * playerMoveSpeed / penaSpeed;

            // プレイヤーの前後（上下）の移動量を決める
            vPlayerMove = vPlayerVelo * playerMoveSpeed / penaSpeed;

            // Eキーを入力すると
            if (Input.GetKeyDown(KeyCode.E))
            {
                // 移動モードをチェンジする
                director.ModeChange = !director.ModeChange;
            }

            // 通常状態で
            if (!director.FatigueSwitch)
            {
                // スペースキーを入力すると
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    // ガードアクションを実行できる状態にする
                    guard = true;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        // プレイヤー動ける状態であり
        if (canMove)
        {
            // 移動モードが「前後」であれば
            if (!director.ModeChange)
            {
                // プレイヤーを前後左右に移動させる
                NormalAct(0, 1);
            }
            // 移動モードが「上下」であれば
            else
            {
                // プレイヤーを上下左右に移動させる
                NormalAct(1, 0);
            }

            // ガードアクションを実行できる状態であり
            if (guard)
            {
                guard = false;

                // 気力回復時間外であれば
                if (director.ChargeTime == 0f)
                {
                    // ガードアクションを実行する
                    GuardAct();

                    // 気力回復時間を設定する
                    director.ChargeTime = 3.0f;
                }
                // 気力回復時間外でなければ
                else
                {
                    // 疲労状態にする
                    director.FatigueSwitch = true;

                    // ペナルティを有効にする
                    damage = 2;
                    penaSpeed = 4;

                    // プレイヤーの色を水色に変化させる
                    playerMat.color = Color.cyan;

                    // 疲労状態回数を増やす
                    director.FatigueCnt += 1;

                    // 気力回復時間を設定する
                    director.ChargeTime = 5.0f;
                }
            }
        }

        // プレイヤーの角度をカメラの角度に合わせる
        rigid.rotation = came.rotation;

        // プレイヤーがゾーンから出ないようにする
        rigid.position = new(
            Mathf.Clamp(rigid.position.x, -13f, 13f), 
            Mathf.Clamp(rigid.position.y, 1.0f, 27f), 
            Mathf.Clamp(rigid.position.z, -13f, 13f)
            );
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 無敵時間外に
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

    private void NormalAct(int Y, int Z)
    {
        rigid.MovePosition(transform.position +
             transform.right * hPlayerMove * Time.fixedDeltaTime +
             transform.up * vPlayerMove * Time.fixedDeltaTime * Y +
             transform.forward * vPlayerMove * Time.fixedDeltaTime * Z);
    }

    private void GuardAct()
    {
        // プレイヤーが動けない状態にする
        canMove = false;

        // プレイヤーオーラを有効にする
        playerAura.SetActive(true);

        // 無敵時間を設定する
        invTime = 1.0f;

        // 行動不能時間を設定する
        stuckTime = 1.0f;
    }

    private void KnockBack(int X, int Y, int Z)
    {
        rigid.AddForce(X, Y, Z, ForceMode.VelocityChange);

        // プレイヤーが動けない状態にする
        canMove = false;

        // 疲労状態でなければ
        if (!director.FatigueSwitch)
        {
            // プレイヤーの色を紫色に変化させる
            playerMat.color = Color.magenta;
        }

        // プレイヤーの体力を減らす
        director.NowPlayerLives -= damage;

        // 無敵時間を設定する
        invTime = 3.0f;

        // 行動不能時間を設定する
        stuckTime = 2.0f;
    }
}
