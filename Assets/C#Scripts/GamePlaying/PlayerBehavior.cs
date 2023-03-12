using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    // プレイヤーの移動量係数
    public float playerMoveSpeed = 10f;

    // プレイヤーオーラ
    [SerializeField] private GameObject playerAura;

    // コンポーネント（剛体、アニメーション、ディレクター、オーディオシステム）
    private Rigidbody rigid;
    private Animator anim;
    private GamePlayingDirector director;
    private AudioSystem audioSystem;

    // プレイヤーのマテリアル
    private Material playerMat;

    // カメラの変位
    private Transform came;

    // 無敵時間
    private float invTime = 0f;

    // プレイヤーの被弾係数
    private int damage = 1;

    // プレイヤーの移動速度ペナルティ
    private int penaSpeed = 1;

    // 行動不能時間
    private float stuckTime = 0f;

    // プレイヤーの左右の加速度
    private float hPlayerVelo = 0f;

    // プレイヤーの前後（上下）の加速度
    private float vPlayerVelo = 0f;

    // プレイヤーの左右の移動量
    private float hPlayerMove;

    // プレイヤーの前後（上下）の移動量
    private float vPlayerMove;

    // プレイヤーが移動可能かどうかのフラグ変数
    private bool canMove = true;
    public bool CanMove
    {
        get { return canMove; }
        set { canMove = value; }
    }

    // 右矢印ボタンを入力しているかどうかのフラグ変数
    private bool rightMove = false;
    public bool RightMove
    {
        get { return rightMove; }
        set { rightMove = value; }
    }

    // 左矢印ボタンを入力しているかどうかのフラグ変数
    private bool leftMove = false;
    public bool LeftMove
    {
        get { return leftMove; }
        set { leftMove = value; }
    }

    // 上矢印ボタンを入力しているかどうかのフラグ変数
    private bool upMove = false;
    public bool UpMove
    {
        get { return upMove; }
        set { upMove = value; }
    }

    // 下矢印ボタンを入力しているかどうかのフラグ変数
    private bool downMove = false;
    public bool DownMove
    {
        get { return downMove; }
        set { downMove = value; }
    }

    // ガードアクションを行うどうかのフラグ変数
    private bool guard = false;
    public bool Guard
    {
        get { return guard; }
        set { guard = value; }
    }

    private void Start()
    {
        // 各コンポーネントを取得する
        rigid = GetComponent<Rigidbody>();
        anim = GameObject.Find("MorroMan_Idle_MoCap").GetComponent<Animator>();
        director = GameObject.FindGameObjectWithTag("Director").GetComponent<GamePlayingDirector>();
        playerMat = GameObject.Find("MHuman").GetComponent<Renderer>().material;
        came = GameObject.Find("Camera").transform;
        audioSystem = GameObject.FindGameObjectWithTag("AudioSystem").GetComponent<AudioSystem>();

        // プレイヤーオーラを無効にし、
        // プレイヤーのアニメーターを止める
        playerAura.SetActive(false);
        anim.speed = 0f;
    }

    private void Update()
    {
        if (invTime > 0f)
        {
            /* 無敵時間のときは
               その時間を経過させる   */

            invTime -= Time.deltaTime;
        }
        else if (invTime < 0f)
        {
            /* 無敵時間が終了したときは
               時間を初期化する（無駄な処理を省くため） */
            invTime = 0f;

            // （プレイヤーが疲労状態であるときは無効）
            if (!director.FatigueSwitch)
            {
                // プレイヤーの色を元に戻す
                playerMat.color = Color.white;
            }

            // プレイヤーオーラを無効にする
            playerAura.SetActive(false);
        }

        if (director.ChargeTime > 0f)
        {
            /* 気力回復時間のときは
               その時間を経過させる   */

            director.ChargeTime -= Time.deltaTime;
        }
        else if (director.ChargeTime < 0f)
        {
            /* 無敵時間が終了したときは
               時間を初期化する（無駄な処理を省くため） */

            director.ChargeTime = 0f;

            // ペナルティを無効にし、
            // プレイヤーを通常状態にする
            damage = 1;
            penaSpeed = 1;
            director.FatigueSwitch = false;

            // （無敵時間のときは無効）
            if (invTime == 0)
            {
                // プレイヤーの色を元に戻す
                playerMat.color = Color.white;
            }
        }

        if (stuckTime > 0f)
        {
            /* 行動不能時間のときは
               その時間を経過させる */

            stuckTime -= Time.deltaTime;
        }
        else if (stuckTime < 0f)
        {
            /* 行動不能時間が終了したときは
               時間を初期化する（無駄な処理を省くため） */

            stuckTime = 0f;

            // プレイヤーにかかっている力をゼロにし、
            // プレイヤーを移動可能にする
            rigid.velocity = Vector3.zero;
            canMove = true;
        }

        // （インタフェースが使用可能である、かつ
        //   プレイヤーが移動可能であるときは有効）
        if (director.CanUseInterf && canMove)
        {
            if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) || rightMove)
            {
                /* 右インタフェース（Dキー, 右矢印ボタン）を入力すると
                   右へ加速する                                        */

                hPlayerVelo = 1.0f;
                vPlayerVelo = 0f;
            }
            else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) || leftMove)
            {
                /* 左インタフェース（Aキー, 左矢印ボタン）を入力すると
                   左へ加速する                                        */

                hPlayerVelo = -1.0f;
                vPlayerVelo = 0f;
            }
            else if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) || upMove)
            {
                /* 前（上）インタフェース（Wキー, 上矢印ボタン）を入力すると
                   前（上）へ加速する                                        */

                vPlayerVelo = 1.0f;
                hPlayerVelo = 0f;
            }
            else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) || downMove)
            {
                /* 後（下）インタフェース（Sキー, 下矢印ボタン）を入力すると
                   後（下）へ加速する                                        */

                vPlayerVelo = -1.0f;
                hPlayerVelo = 0f;
            }
            else
            {
                /* インタフェースを入力しないと
                   減速する                     */
                
                hPlayerVelo *= 0.5f;
                vPlayerVelo *= 0.5f;
            }

            // プレイヤーの左右、前後（上下）の移動量を設定する
            hPlayerMove = hPlayerVelo * playerMoveSpeed / penaSpeed;
            vPlayerMove = vPlayerVelo * playerMoveSpeed / penaSpeed;

            if (Input.GetKeyDown(KeyCode.E))
            {
                /* Eキーを入力すると
                   移動モードをチェンジする
                   （効果音再生あり）       */

                audioSystem.Music = 0;
                director.ModeChange = !director.ModeChange;
            }

            // （プレイヤーが疲労状態であるときは無効）
            if (!director.FatigueSwitch)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    /* スペースキーを入力すると
                       ガードアクションを行う（準備をする）   */
                    guard = true;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        // （プレイヤーが移動可能であるときは有効）
        if (canMove)
        {
            if (!director.ModeChange)
            {
                /* 移動モードが「前後」であれば
                   プレイヤーを前後左右に移動させる */

                NormalAct(0, 1);
            }
            else
            {
                /* 移動モードが「上下」であれば
                   プレイヤーを上下左右に移動させる */

                NormalAct(1, 0);
            }

            if (guard)
            {
                /* ガードアクションを行う準備をしていれば
                   フラグをオフにする                     */

                guard = false;

                if (director.ChargeTime == 0f)
                {
                    /* 気力回復時間でないときは
                       ガードアクションを行う
                       （効果音再生あり）       */

                    audioSystem.Music = 6;
                    GuardAct();

                    // 気力回復時間を設定する
                    director.ChargeTime = 3.0f;
                }
                else
                {
                    /* 気力回復時間のときは
                       プレイヤーを疲労状態にする
                       （効果音再生あり）         */

                    audioSystem.Music = 7;
                    director.FatigueSwitch = true;

                    // ペナルティを有効にし、
                    // プレイヤーの色を水色に変化させる
                    damage = 2;
                    penaSpeed = 4;
                    playerMat.color = Color.cyan;

                    // 疲労状態回数を増やし、
                    // 気力回復時間を設定する
                    director.FatigueCnt += 1;
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
        // （無敵時間のときは無効）
        if (invTime == 0)
        {
            // ゾーンやエネミーに衝突するとプレイヤーがノックバックする
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
        // プレイヤーを移動させる
        rigid.MovePosition(transform.position +
             transform.right * hPlayerMove * Time.fixedDeltaTime +
             transform.up * vPlayerMove * Time.fixedDeltaTime * Y +
             transform.forward * vPlayerMove * Time.fixedDeltaTime * Z
             );
    }

    private void GuardAct()
    {
        // プレイヤーを移動不可にし、
        // プレイヤーオーラを有効にする
        // （プレイヤーオーラがガードを行ってくれる）
        canMove = false;
        playerAura.SetActive(true);

        // 無敵時間、行動不能時間を設定する
        invTime = 1.0f;
        stuckTime = 1.0f;
    }

    private void KnockBack(int X, int Y, int Z)
    {
        // ノックバックの力を加え、
        // プレイヤーを移動不可にする
        // （効果音再生あり）
        audioSystem.Music = 8;
        rigid.AddForce(X, Y, Z, ForceMode.VelocityChange);
        canMove = false;

        // （疲労状態であるときは無効）
        if (!director.FatigueSwitch)
        {
            // プレイヤーの色を紫色に変化させる
            playerMat.color = Color.magenta;
        }

        // プレイヤーの体力を減らす
        director.NowPlayerLives -= damage;

        // 無敵時間、行動不能時間を設定する
        invTime = 3.0f;
        stuckTime = 2.0f;
    }
}
