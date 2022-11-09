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

    private Image lifeBar;
    private TextMeshProUGUI timerText;
    private float nowTimeLim; // 現在の制限時間
    private int iTimeLim; // 整数化した制限時間

    private float nowPlayerLife; // 現在のプレイヤーの体力

    public float NowPlayerLife
    {
        get { return nowPlayerLife; }
        set { nowPlayerLife = value; }
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

    private bool opening = false; // オープニングへ遷移するかどうか

    public bool Opening
    {
        get { return opening; }
        set { opening = value; }
    }

    private bool canUseButton = false; // ボタンが使える状態かどうか

    public bool CanUseButton
    {
        get { return canUseButton; }
    }

    private bool canUseInterf = false; // インタフェースが使える状態かどうか

    public bool CanUseInterf
    {
        get { return canUseInterf; }
    }

    private void Start()
    {
        // 体力バーのイメージコンポーネントを取得する
        lifeBar = GameObject.Find("5Life Bar").GetComponent<Image>();

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

        // 現在の制限時間を格納する
        nowTimeLim = StaticUnits.GameTimeLim;

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

        // インタフェースが使える状態でありEscキーを押した場合は
        if (canUseInterf)
        {
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

            // ボタン使用可能状態を無効にする
            canUseButton = false;

            // インタフェース使用可能状態を無効にする
            canUseInterf = false;

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
        if (opening)
        {
            opening = false;

            // ボタン使用可能状態を無効にする
            canUseButton = false;

            // インタフェース使用可能状態を無効にする
            canUseInterf = false;

            StartCoroutine(ToOpening(0.3f));
        }

        // 体力バーの表示を更新する
        lifeBar.fillAmount = nowPlayerLife / StaticUnits.MaxPlayerLife;

        // 体力がゼロになれば
        if (nowPlayerLife == 0f)
        {
            // ボタン使用可能状態を無効にする
            canUseButton = false;

            // インタフェース使用可能状態を無効にする
            canUseInterf = false;

            // 敗北者になる（1回の待機あり）
            StartCoroutine(Loser(2.5f));
        }

        // 現在の制限時間を経過させる
        nowTimeLim -= Time.deltaTime;

        // 制限時間を整数化する
        iTimeLim = (int)nowTimeLim;

        // タイマーの表示を更新する
        timerText.text = iTimeLim.ToString();

        // 制限時間がゼロになれば
        if (nowTimeLim < 0f)
        {
            // 時間を初期化する（正常な処理のため）
            nowTimeLim = 0f;

            // ボタン使用可能状態を無効にする
            canUseButton = false;

            // インタフェース使用可能状態を無効にする
            canUseInterf = false;

            // 勝利者になる（1回の待機あり）
            StartCoroutine(Winner(5.5f));
        }
    }

    private IEnumerator GameStart(float fWT)
    {
        // 1回目の待機
        yield return new WaitForSecondsRealtime(fWT);

        // ボタン使用可能状態を有効にする
        canUseButton = true;

        // インタフェース使用可能状態を有効にする
        canUseInterf = true;

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

        // ゲームの一時停止を解除する
        Time.timeScale = 1.0f;

        // ゲームエンディングへ遷移する
        SceneManager.LoadScene("GameEndingScene");
    }
}
