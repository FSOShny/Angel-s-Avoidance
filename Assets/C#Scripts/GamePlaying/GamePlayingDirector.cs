using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GamePlayingDirector : MonoBehaviour
{
    // UIオブジェクト
    [SerializeField] private GameObject startUi;
    [SerializeField] private GameObject smartPhoneLeftUi;
    [SerializeField] private GameObject smartPhoneRightUi;
    [SerializeField] private GameObject pauseUi;
    [SerializeField] private GameObject platformUi;
    [SerializeField] private GameObject gameOverUi;
    [SerializeField] private GameObject gameClearUi;

    // スプライト
    [SerializeField] private Sprite energy;
    [SerializeField] private Sprite fatigue;

    // コンポーネント（イメージ、テキスト、ディレクター）
    private Image noLivesBar;
    private Image livesBar;
    private Image energyBar;
    private Image upDown;
    private Image forwardBack;
    private TextMeshProUGUI timerText;
    private GameEndingDirector nextDirector;

    // 現在の残り時間
    private float nowTimeLeft;

    // （整数化した）現在の残り時間
    private int iTimeLeft;

    // インタフェースが使用可能かどうかのフラグ変数
    private bool canUseInterf = false;
    public bool CanUseInterf
    {
        get { return canUseInterf; }
    }

    // ボタンが使用可能かどうかのフラグ変数
    private bool canUseButton = false;
    public bool CanUseButton
    {
        get { return canUseButton; }
    }

    // 現在のプレイヤーの体力
    private int nowPlayerLives;
    public int NowPlayerLives
    {
        get { return nowPlayerLives; }
        set { nowPlayerLives = value; }
    }

    // ポーズ画面を有効にするかどうかのフラグ変数
    private bool pauseSwitch = false;
    public bool PauseSwitch
    {
        get { return pauseSwitch; }
        set { pauseSwitch = value; }
    }

    // ポーズ画面を無効にするかどうかのフラグ変数
    private bool continueSwitch = false;
    public bool ContinueSwitch
    {
        get { return continueSwitch; }
        set { continueSwitch = value; }
    }

    // ゲームプレイを再開始するかどうかのフラグ変数
    private bool restartSwitch = false;
    public bool RestartSwitch
    {
        get { return restartSwitch; }
        set { restartSwitch = value; }
    }

    // プラットフォーム画面を有効にするかどうかのフラグ変数
    private bool platformSwitch = false;
    public bool PlatformSwitch
    {
        get { return platformSwitch; }
        set { platformSwitch = value; }
    }

    // オープニングへ移動するかどうかのフラグ変数
    private bool openingSwitch = false;
    public bool OpeningSwitch
    {
        get { return openingSwitch; }
        set { openingSwitch = value; }
    }

    // 移動モードが「上下」かどうかのフラグ変数
    private bool modeChange = false;
    public bool ModeChange
    {
        get { return modeChange; }
        set { modeChange = value; }
    }

    // プレイヤーが疲労状態かどうかのフラグ変数
    private bool fatigueSwitch = false;
    public bool FatigueSwitch
    {
        get { return fatigueSwitch; }
        set { fatigueSwitch = value; }
    }

    // 気力回復時間
    private float chargeTime = 0f;
    public float ChargeTime
    {
        get { return chargeTime; }
        set { chargeTime = value; }
    }

    // 疲労状態回数
    private int fatigueCnt = 0;
    public int FatigueCnt
    {
        get { return fatigueCnt; }
        set { fatigueCnt = value; }
    }

    private void Start()
    {
        // 各コンポーネントを取得する
        noLivesBar = GameObject.Find("No Lives Bar").GetComponent<Image>();
        livesBar = GameObject.Find("Lives Bar").GetComponent<Image>();
        energyBar = GameObject.Find("Energy Bar").GetComponent<Image>();
        upDown = GameObject.Find("UpDown Mode Image").GetComponent<Image>();
        forwardBack = GameObject.Find("ForwardBack Mode Image").GetComponent<Image>();
        timerText = GameObject.Find("Timer Text").GetComponent<TextMeshProUGUI>();

        // スタート画面を有効にする
        startUi.SetActive(true);

        // スタート画面以外を無効にする
        smartPhoneLeftUi.SetActive(false);
        smartPhoneRightUi.SetActive(false);
        pauseUi.SetActive(false);
        platformUi.SetActive(false);
        gameOverUi.SetActive(false);
        gameClearUi.SetActive(false);

        // 現在のプレイヤーの体力を設定する
        nowPlayerLives = StaticUnits.MaxPlayerLives;

        // 上下モード画像を無効にする
        upDown.enabled = false;

        // 現在の残り時間を設定する
        nowTimeLeft = StaticUnits.GameTime;

        // ゲームプレイを開始する
        StartCoroutine(GameStart(3.0f));
    }

    private void Update()
    {
        if (StaticUnits.SmartPhone)
        {
            /* 現在のプラットフォームがスマホであれば
               スマホUI表示を有効にする               */

            smartPhoneLeftUi.SetActive(true);
            smartPhoneRightUi.SetActive(true);
        }
        else
        {
            /* 現在のプラットフォームがパソコンであれば
               スマホUI表示を無効にする                 */

            smartPhoneLeftUi.SetActive(false);
            smartPhoneRightUi.SetActive(false);
        }

        // （インタフェースが使用可能であるときは有効）
        if (canUseInterf)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                /* Xキーを入力すると
                   ポーズ画面を有効にする */

                pauseSwitch = true;
            }
        }

        if (pauseSwitch)
        {
            /* ポーズ画面を有効にする */

            // ゲームの一時停止を実行する
            Time.timeScale = 0f;

            // インタフェースを使用不可にする
            canUseInterf = false;

            // （フラグをオフにしてから処理を行う）
            pauseSwitch = false;
            pauseUi.SetActive(true);
        }

        if (continueSwitch)
        {
            /* ポーズ画面を無効にする */

            // （フラグをオフにしてから処理を行う）
            continueSwitch = false;
            pauseUi.SetActive(false);

            // インタフェースを使用可能にする
            canUseInterf = true;

            // ゲームの一時停止を解除する
            Time.timeScale = 1.0f;
        }

        if (restartSwitch)
        {
            /* ゲームプレイを再開始する */

            // ボタンを使用不可にする
            canUseButton = false;

            // （フラグをオフにしてから処理を行う）
            restartSwitch = false;
            StartCoroutine(GameRestart(0.3f));
        }

        if (platformSwitch)
        {
            /* プラットフォーム画面を有効にする */

            platformUi.SetActive(true);
        }
        else
        {
            /* プラットフォーム画面を無効にする */

            platformUi.SetActive(false);
        }

        if (openingSwitch)
        {
            /* オープニングへ移動する */

            // ボタンを使用不可にする
            canUseButton = false;

            // （フラグをオフにしてから処理を行う）
            openingSwitch = false;
            StartCoroutine(ToOpening(0.3f));
        }

        if (modeChange)
        {
            /* 移動モードが「上下」であれば
               上下モードイメージを有効にする */

            forwardBack.enabled = false;
            upDown.enabled = true;
        }
        else
        {
            /* 移動モードが「前後」であれば
               前後モードイメージを有効にする */

            forwardBack.enabled = true;
            upDown.enabled = false;
        }

        // 体力バー（黒色）を表示する
        noLivesBar.fillAmount = StaticUnits.MaxPlayerLives / 5.0f;

        // 体力バー（緑色）を表示する
        livesBar.fillAmount = nowPlayerLives / 5.0f;

        if (nowPlayerLives <= 0)
        {
            /* （体力がゼロになると）
               ゲームオーバーになる   */

            // インタフェース、ボタンを使用不可にする
            canUseInterf = false;
            canUseButton = false;

            // ゲームエンディングへ移動する
            StartCoroutine(GameOver(2.0f));
        }

        if (!fatigueSwitch)
        {
            /* プレイヤーが疲労状態でなければ
               気力バーの色を黄色にする       */

            energyBar.sprite = energy;

            // 気力バーを表示する
            energyBar.fillAmount = 1.0f - chargeTime / 3.0f;
        }
        else
        {
            /* プレイヤーが疲労状態であれば
               気力バーの色を水色にする     */
            energyBar.sprite = fatigue;

            // 気力バーを表示する
            energyBar.fillAmount = 1.0f - chargeTime / 5.0f;
        }

        // 現在の残り時間を経過させる
        nowTimeLeft -= Time.deltaTime;

        // 現在の残り時間を整数化する
        iTimeLeft = (int)nowTimeLeft;

        // タイマーを更新する
        timerText.text = iTimeLeft.ToString();

        if (nowTimeLeft < 0f)
        {
            /* （残り時間がゼロになると）
               ゲームクリアになる         */

            // インタフェース、ボタンを使用不可にする
            canUseInterf = false;
            canUseButton = false;

            // ゲームエンディングへ移動する
            StartCoroutine(GameClear(5.0f));
        }
    }

    private IEnumerator GameStart(float fWT)
    {
        // 待機処理（3.0秒）
        yield return new WaitForSecondsRealtime(fWT);

        // インタフェースを使用可能にする
        canUseInterf = true;

        // ボタンを使用可能にする
        canUseButton = true;

        // スタート画面を無効にする
        startUi.SetActive(false);

        // ゲームの一時停止を解除する
        Time.timeScale = 1.0f;
    }

    private IEnumerator GameRestart(float fWT)
    {
        // 待機処理（0.3秒）
        yield return new WaitForSecondsRealtime(fWT);

        // 自身（ゲームプレイシーン）をロードする
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator ToOpening(float fWT)
    {
        // 待機処理（0.3秒）
        yield return new WaitForSecondsRealtime(fWT);

        // ゲームの一時停止を解除する
        Time.timeScale = 1.0f;

        // オープニングシーンをロードする
        SceneManager.LoadScene("OpeningScene");
    }

    private IEnumerator GameOver(float fWT)
    {
        // ゲームオーバー画面を有効にし、
        // ゲームの一時停止を実行する
        gameOverUi.SetActive(true);
        Time.timeScale = 0f;

        // 待機処理（2.0秒）
        yield return new WaitForSecondsRealtime(fWT);

        // ボタンを使用可能にする
        canUseButton = true;
    }

    private IEnumerator GameClear(float fWT)
    {
        // ゲームクリア画面を有効にし、
        // ゲームの一時停止を実行する
        gameClearUi.SetActive(true);
        Time.timeScale = 0f;

        // 待機処理（5.0秒）
        yield return new WaitForSecondsRealtime(fWT);

        // 次のシーンへの変数引き渡しを開始する
        SceneManager.sceneLoaded += GameEndingLoaded;

        // ゲームの一時停止を解除する
        Time.timeScale = 1.0f;

        // ゲームエンディングシーンをロードする
        SceneManager.LoadScene("GameEndingScene");
    }

    private void GameEndingLoaded(Scene scene, LoadSceneMode mode)
    {
        // ゲームエンディングディレクターを取得する
        nextDirector = GameObject.FindGameObjectWithTag("Director").GetComponent<GameEndingDirector>();

        // 被弾した回数、疲労状態になった回数を設定する
        nextDirector.Damaged = StaticUnits.MaxPlayerLives - nowPlayerLives;
        nextDirector.Fatigued = FatigueCnt;

        // 次のシーンへの変数引き渡しを終了する
        SceneManager.sceneLoaded -= GameEndingLoaded;
    }
}
