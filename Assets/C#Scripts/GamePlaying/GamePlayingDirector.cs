using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GamePlayingDirector : MonoBehaviour
{
    public float gameTimeLim = 45f; // ゲームの制限時間

    [SerializeField] private GameObject startUI;
    [SerializeField] private GameObject smartPhoneUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject platformUI;
    [SerializeField] private GameObject loserUI;
    [SerializeField] private GameObject winnerUI;

    private Image lifeBar;
    private Image upDown;
    private Image forwardBack;
    private TextMeshProUGUI timerText;
    private GameEndingDirector nextDirector;
    private float nowTimeLim; // 現在の制限時間
    private int iTimeLim; // 整数化した制限時間

    private int nowPlayerLife; // 現在のプレイヤーの体力

    public int NowPlayerLife
    {
        get { return nowPlayerLife; }
        set { nowPlayerLife = value; }
    }

    private bool modeChange = false; // 移動モードが「上下」かどうか

    public bool ModeChange
    {
        get { return modeChange; }
        set { modeChange = value; }
    }

    private bool pauseSwitch = false; // ポーズ画面へ遷移するかどうか

    public bool PauseSwitch
    {
        get { return pauseSwitch; }
        set { pauseSwitch = value; }
    }

    private bool continueSwitch = false; // ゲームプレイを続行するかどうか

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

    private bool canUseInterf = false; // インタフェースが使える状態かどうか

    public bool CanUseInterf
    {
        get { return canUseInterf; }
    }

    private bool canUseButton = false; // ボタンが使える状態かどうか

    public bool CanUseButton
    {
        get { return canUseButton; }
    }

    private void Start()
    {
        // 体力バーのイメージコンポーネントを取得する
        lifeBar = GameObject.Find("5Life Bar").GetComponent<Image>();

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

        // 現在のプレイヤーの体力を格納する
        nowPlayerLife = StaticUnits.MaxPlayerLife;

        // 上下モード画像を無効にする
        upDown.enabled = false;

        // 現在の制限時間を格納する
        nowTimeLim = gameTimeLim;

        /* ゲームプレイを開始する（1回の待機あり） */
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

        // インタフェースが使える状態で
        if (canUseInterf)
        {
            // Escキーを入力すると
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // ポーズ画面への遷移判定を有効にする
                pauseSwitch = true;
            }
        }

        /* ポーズ画面へ遷移する */
        if (pauseSwitch)
        {
            pauseSwitch = false;

            // ゲームの一時停止を実行する
            Time.timeScale = 0f;

            // インタフェース使用可能状態を無効にする
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

            // インタフェース使用可能状態を有効にする
            canUseInterf = true;

            // ゲームの一時停止を解除する
            Time.timeScale = 1.0f;
        }

        /* ゲームプレイを再開始する（1回の待機あり） */
        if (restartSwitch)
        {
            restartSwitch = false;

            // インタフェース使用可能状態を無効にする
            canUseInterf = false;

            // ボタン使用可能状態を無効にする
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

            // インタフェース使用可能状態を無効にする
            canUseInterf = false;

            // ボタン使用可能状態を無効にする
            canUseButton = false;

            StartCoroutine(ToOpening(0.3f));
        }

        // 体力バーを更新する
        lifeBar.fillAmount = (float)nowPlayerLife / StaticUnits.MaxPlayerLife;

        // 体力がゼロになれば
        if (nowPlayerLife == 0)
        {
            // インタフェース使用可能状態を無効にする
            canUseInterf = false;

            // ボタン使用可能状態を無効にする
            canUseButton = false;

            // 敗北者になる（1回の待機あり）
            StartCoroutine(Loser(2.5f));
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

            // インタフェース使用可能状態を無効にする
            canUseInterf = false;

            // ボタン使用可能状態を無効にする
            canUseButton = false;

            // 勝利者になる（1回の待機あり）
            StartCoroutine(Winner(5.5f));
        }
    }

    private IEnumerator GameStart(float fWT)
    {
        // 1回目の待機
        yield return new WaitForSecondsRealtime(fWT);

        // インタフェース使用可能状態を有効にする
        canUseInterf = true;

        // ボタン使用可能状態を有効にする
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

        // ボタン使用可能状態を有効にする
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

        // 被弾回数を格納する
        nextDirector.Attacked = StaticUnits.MaxPlayerLife - nowPlayerLife;

        // 次のシーンへの変数引き渡しを終了する
        SceneManager.sceneLoaded -= GameEndingLoaded;
    }
}
