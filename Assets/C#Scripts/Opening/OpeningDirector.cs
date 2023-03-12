using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningDirector : MonoBehaviour
{
    // UIオブジェクト
    [SerializeField] private GameObject platformUi;
    [SerializeField] private GameObject openingUi;

    // アニメーション時間
    private float animTime = 0f;
    public float AnimTime
    {
        get { return animTime; }
    }

    // オープニングへ移動するかどうかのフラグ変数
    private bool openingSwitch = false;
    public bool OpeningSwitch
    {
        get { return openingSwitch; }
        set { openingSwitch = value; }
    }

    // ゲームチュートリアルへ移動するかどうかのフラグ変数
    private bool tutorialsSwitch = false;
    public bool TutorialsSwitch
    {
        get { return tutorialsSwitch; }
        set { tutorialsSwitch = value; }
    }

    // ゲームプレイへ移動するかどうかのフラグ変数
    private bool playingSwitch = false;
    public bool PlayingSwitch
    {
        get { return playingSwitch; }
        set { playingSwitch = value; }
    }

    // ゲームオプションへ移動するかどうかのフラグ変数
    private bool optionsSwitch = false;
    public bool OptionsSwitch
    {
        get { return optionsSwitch; }
        set { optionsSwitch = value; }
    }

    private void Start()
    {
        if (StaticUnits.Startup)
        {
            /* ゲームの起動直後であれば
               プラットフォーム画面を有効にする */

            // （フラグをオフにしてから処理を行う）
            StaticUnits.Startup = false;
            platformUi.SetActive(true);
            openingUi.SetActive(false);
        }
        else
        {
            /* ゲームの起動直後であれば
               オープニング画面を有効にする */

            platformUi.SetActive(false);
            openingUi.SetActive(true);

            // アニメーション時間を設定する
            animTime = 3.0f;
        }
    }

    private void Update()
    {
        if (animTime > 0f)
        {
            /* アニメーションのときは
               その時間を経過させる   */

            animTime -= Time.deltaTime;
        }
        else if (animTime < 0f)
        {
            /* アニメーションが終了したときは
               時間を初期化する（無駄な処理を省くため） */

            animTime = 0f;
        }

        if (openingSwitch)
        {
            /* オープニング画面を有効にする */

            // （フラグをオフにしてから処理を行う）
            openingSwitch = false;
            StartCoroutine(ToOpening(0.3f, 0.5f));
        }

        if (tutorialsSwitch)
        {
            /* ゲームチュートリアルへ移動する */

            // （フラグをオフにしてから処理を行う）
            tutorialsSwitch = false;
            StartCoroutine(ToTutorials(0.5f));
        }

        if (playingSwitch)
        {
            /* ゲームプレイへ移動する */

            // （フラグをオフにしてから処理を行う）
            playingSwitch = false;
            StartCoroutine(ToPlaying(1.5f));
        }

        if (optionsSwitch)
        {
            /* ゲームオプションへ移動する */

            // （フラグをオフにしてから処理を行う）
            optionsSwitch = false;
            StartCoroutine(ToOptions(0.5f));
        }
    }

    private IEnumerator ToOpening(float fWT, float sWT)
    {
        // 待機処理（0.3秒）
        yield return new WaitForSeconds(fWT);

        // プラットフォーム画面を無効にする
        platformUi.SetActive(false);

        // 待機処理（0.5秒）
        yield return new WaitForSeconds(sWT);

        // オープニング画面を有効にし
        // アニメーション時間を設定する
        openingUi.SetActive(true);
        animTime = 3.0f;
    }

    private IEnumerator ToTutorials(float fWT)
    {
        // 待機処理（0.5秒）
        yield return new WaitForSeconds(fWT);

        // ゲームチュートリアルシーンをロードする
        SceneManager.LoadScene("GameTutorialsScene");
    }

    private IEnumerator ToPlaying(float fWT)
    {
        // 待機処理（1.5秒）
        yield return new WaitForSeconds(fWT);

        // ゲームの一時停止を実行する
        // （正確なゲームプレイの時間計測を行うため）
        Time.timeScale = 0f;

        // ゲームプレイシーンをロードする
        SceneManager.LoadScene("GamePlayingScene");
    }

    private IEnumerator ToOptions(float fWT)
    {
        // 待機処理（0.5秒）
        yield return new WaitForSeconds(fWT);

        // ゲームオプションシーンをロードする
        SceneManager.LoadScene("GameOptionsScene");
    }
}
