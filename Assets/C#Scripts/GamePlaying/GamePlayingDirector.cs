using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GamePlayingDirector : MonoBehaviour
{
    [SerializeField] private GameObject startUI;
    [SerializeField] private GameObject smartPhoneUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject platformUI;
    [SerializeField] private GameObject loserUI;
    [SerializeField] private GameObject winnerUI;
    [SerializeField] private Sprite energy;
    [SerializeField] private Sprite fatigue;

    private Image noLivesBar;
    private Image livesBar;
    private Image energyBar;
    private Image upDown;
    private Image forwardBack;
    private TextMeshProUGUI timerText;
    private GameEndingDirector nextDirector;
    private float nowTimeLim; // 現在の制限時間
    private int iTimeLim; // 整数化した制限時間

    private int nowPlayerLives; // 現在のプレイヤーの体力

    public int NowPlayerLives
    {
        get { return nowPlayerLives; }
        set { nowPlayerLives = value; }
    }

    private bool canUseInterf = false; // インタフェースを使える状態かどうか

    public bool CanUseInterf
    {
        get { return canUseInterf; }
    }

    private bool pauseSwitch = false; // ポーズ画面へ遷移するかどうか

    public bool PauseSwitch
    {
        get { return pauseSwitch; }
        set { pauseSwitch = value; }
    }

    private bool continueSwitch = true; // ゲームプレイを続行するかどうか

    public bool ContinueSwitch
    {
        get { return continueSwitch; }
        set { continueSwitch = value; }
    }

    private bool restartSwitch = false; // ゲームプレイを再開始するかどうか

    public bool RestartSwitch
    {
        get { return restartSwitch; }
        set { restartSwitch = value; }
    }

    private bool canUseButton = false; // ボタンを使える状態かどうか

    public bool CanUseButton
    {
        get { return canUseButton; }
    }

    private bool platformSwitch = false; // プラットフォーム画面へ遷移するかどうか

    public bool PlatformSwitch
    {
        get { return platformSwitch; }
        set { platformSwitch = value; }
    }

    private bool openingSwitch = false; // オープニングへ遷移するかどうか

    public bool OpeningSwitch
    {
        get { return openingSwitch; }
        set { openingSwitch = value; }
    }

    private bool modeChange = false; // 移動モードが「上下」かどうか

    public bool ModeChange
    {
        get { return modeChange; }
        set { modeChange = value; }
    }

    private bool fatigueSwitch = false; // 疲労状態かどうか

    public bool FatigueSwitch
    {
        get { return fatigueSwitch; }
        set { fatigueSwitch = value; }
    }

    private float chargeTime = 0f; // 気力回復時間

    public float ChargeTime
    {
        get { return chargeTime; }
        set { chargeTime = value; }
    }

    private int fatigueCnt = 0; // 疲労状態回数

    public int FatigueCnt
    {
        get { return fatigueCnt; }
        set { fatigueCnt = value; }
    }

    private void Start()
    {
        // 体力バー（黒色）のイメージコンポーネントを取得する
        noLivesBar = GameObject.Find("No Lives Bar").GetComponent<Image>();

        // 体力バー（緑色）のイメージコンポーネントを取得する
        livesBar = GameObject.Find("Lives Bar").GetComponent<Image>();

        // 気力バーのイメージコンポーネントを取得する
        energyBar = GameObject.Find("Energy Bar").GetComponent<Image>();

        // 上下モード画像のイメージコンポーネントを取得する
        upDown = GameObject.Find("UpDown Mode Image").GetComponent<Image>();

        // 前後モード画像のイメージコンポーネントを取得する
        forwardBack = GameObject.Find("ForwardBack Mode Image").GetComponent<Image>();

        // タイマーのテキストコンポーネントを取得する
        timerText = GameObject.Find("Timer Text").GetComponent<TextMeshProUGUI>();

        // スタート画面を有効にする
        startUI.SetActive(true);
        smartPhoneUI.SetActive(false);
        pauseUI.SetActive(false);
        platformUI.SetActive(false);
        loserUI.SetActive(false);
        winnerUI.SetActive(false);

        // 現在のプレイヤーの体力を設定する
        nowPlayerLives = StaticUnits.MaxPlayerLives;

        // 上下モード画像を無効にする
        upDown.enabled = false;

        // 現在の制限時間を設定する
        nowTimeLim = StaticUnits.GameTimeLim;

        // ゲームプレイを開始する（1回の待機あり）
        StartCoroutine(GameStart(3.0f));
    }

    private void Update()
    {
        // 現在のプラットフォームがスマホであれば
        if (StaticUnits.SmartPhone)
        {
            // スマホUI表示を有効にする
            smartPhoneUI.SetActive(true);
        }
        // 現在のプラットフォームがパソコンであれば
        else
        {
            // スマホUI表示を無効にする
            smartPhoneUI.SetActive(false);
        }

        // インタフェースを使える状態で
        if (canUseInterf)
        {
            // Escキーを入力すると
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // ポーズ画面への遷移を有効にする
                pauseSwitch = true;
            }
        }

        /* ポーズ画面へ遷移する */
        if (pauseSwitch)
        {
            pauseSwitch = false;

            // ゲームの一時停止を実行する
            Time.timeScale = 0f;

            // インタフェースを使えない状態にする
            canUseInterf = false;

            // ポーズ画面を有効にする
            pauseUI.SetActive(true);
        }
        
        /* ゲームプレイを続行する */
        if (continueSwitch)
        {
            continueSwitch = false;

            // ポーズ画面を無効にする
            pauseUI.SetActive(false);

            // インタフェースを使える状態にする
            canUseInterf = true;

            // ゲームの一時停止を解除する
            Time.timeScale = 1.0f;
        }

        /* ゲームプレイを再開始する（1回の待機あり） */
        if (restartSwitch)
        {
            restartSwitch = false;

            // インタフェースを使えない状態にする
            canUseInterf = false;

            // ボタンを使えない状態にする
            canUseButton = false;

            StartCoroutine(GameRestart(0.3f));
        }

        /* プラットフォーム画面へ遷移する */
        if (platformSwitch)
        {
            // プラットフォーム画面を有効にする
            platformUI.SetActive(true);
        }
        else
        {
            // プラットフォーム画面を無効にする
            platformUI.SetActive(false);
        }

        /* オープニングへ遷移する（1回の待機あり） */
        if (openingSwitch)
        {
            openingSwitch = false;

            // インタフェースを使えない状態にする
            canUseInterf = false;

            // ボタンを使えない状態にする
            canUseButton = false;

            StartCoroutine(ToOpening(0.3f));
        }

        // 移動モードが「上下」であれば
        if (modeChange)
        {
            // 前後モード画像を無効にする
            forwardBack.enabled = false;

            // 上下モード画像を有効にする
            upDown.enabled = true;
        }
        // 移動モードが「前後」であれば
        else
        {
            // 前後モード画像を有効にする
            forwardBack.enabled = true;

            // 上下モード画像を無効にする
            upDown.enabled = false;
        }

        // 体力バー（黒色）を表示する
        noLivesBar.fillAmount = StaticUnits.MaxPlayerLives / 5.0f;

        // 体力バー（緑色）を更新する
        livesBar.fillAmount = nowPlayerLives / 5.0f;

        // 体力がゼロになれば
        if (nowPlayerLives == 0 || nowPlayerLives == -1)
        {
            // インタフェースを使えない状態にする
            canUseInterf = false;

            // ボタンを使えない状態にする
            canUseButton = false;

            // 敗北者になる（1回の待機あり）
            StartCoroutine(Loser(2.5f));
        }

        // 通常状態であれば
        if (!fatigueSwitch)
        {
            // 気力バーの色を黄色にする
            energyBar.sprite = energy;

            // 気力バーを更新する
            energyBar.fillAmount = 1.0f - chargeTime / 3.0f;
        }
        // 疲労状態であれば
        else
        {
            // 気力バーの色を水色にする
            energyBar.sprite = fatigue;

            // 気力バーを更新する
            energyBar.fillAmount = 1.0f - chargeTime / 5.0f;
        }

        // 気力回復時間中は
        if (chargeTime > 0f)
        {
            // 時間を経過させる
            chargeTime -= Time.deltaTime;
        }
        // 気力回復時間終了時は
        else if (chargeTime < 0f)
        {
            // 時間を初期化する（正常な処理のため）
            chargeTime = 0f;

            // 通常状態にする
            fatigueSwitch = false;
        }

        // 現在の制限時間を経過させる
        nowTimeLim -= Time.deltaTime;

        // 制限時間を整数化する
        iTimeLim = (int)nowTimeLim;

        // タイマーを更新する
        timerText.text = iTimeLim.ToString();

        // 制限時間がゼロになれば
        if (nowTimeLim < 0f)
        {
            // 時間を初期化する（正常な処理のため）
            nowTimeLim = 0f;

            // インタフェースを使えない状態にする
            canUseInterf = false;

            // ボタンを使えない状態にする
            canUseButton = false;

            // 勝利者になる（1回の待機あり）
            StartCoroutine(Winner(5.5f));
        }
    }

    private IEnumerator GameStart(float fWT)
    {
        // 1回目の待機
        yield return new WaitForSecondsRealtime(fWT);

        // インタフェースを使える状態にする
        canUseInterf = true;

        // ボタンを使える状態にする
        canUseButton = true;

        // スタート画面を無効にする
        startUI.SetActive(false);

        // ゲームの一時停止を解除する
        Time.timeScale= 1.0f;
    }

    private IEnumerator GameRestart(float fWT)
    {
        // 1回目の待機
        yield return new WaitForSecondsRealtime(fWT);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator ToOpening(float fWT)
    {
        // 1回目の待機
        yield return new WaitForSecondsRealtime(fWT);

        // ゲームの一時停止を解除する
        Time.timeScale = 1.0f;

        SceneManager.LoadScene("OpeningScene");
    }

    private IEnumerator Loser(float fWT)
    {
        // 敗北者画面を有効にする
        loserUI.SetActive(true);

        // ゲームの一時停止を実行する
        Time.timeScale = 0f;

        // 1回目の待機
        yield return new WaitForSecondsRealtime(fWT);

        // ボタンを使える状態にする
        canUseButton = true;
    }

    private IEnumerator Winner(float fWT)
    {
        // 勝利者画面を有効にする
        winnerUI.SetActive(true);

        // ゲームの一時停止を実行する
        Time.timeScale = 0f;

        // 1回目の待機
        yield return new WaitForSecondsRealtime(fWT);

        // 次のシーンへの変数引き渡しを開始する
        SceneManager.sceneLoaded += GameEndingLoaded;

        // ゲームの一時停止を解除する
        Time.timeScale = 1.0f;

        // ゲームエンディングへ遷移する
        SceneManager.LoadScene("GameEndingScene");
    }

    private void GameEndingLoaded(Scene scene, LoadSceneMode mode)
    {
        // ゲームエンディングディレクターを取得する
        nextDirector = GameObject.Find("Game Ending Director").GetComponent<GameEndingDirector>();

        // 被弾回数を設定する
        nextDirector.Damaged = StaticUnits.MaxPlayerLives - nowPlayerLives;

        // 疲労状態回数を設定する
        nextDirector.Fatigued = FatigueCnt;

        // 次のシーンへの変数引き渡しを終了する
        SceneManager.sceneLoaded -= GameEndingLoaded;
    }
}
