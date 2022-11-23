using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningDirector : MonoBehaviour
{
    [SerializeField] private GameObject platformUI;
    [SerializeField] private GameObject openingUI;

    // アニメーション時間
    private float animTime = 0f;

    public float AnimTime
    {
        get { return animTime; }
    }

    // オープニング画面へ遷移するかどうか
    private bool openingSwitch = false;

    public bool OpeningSwitch
    {
        get { return openingSwitch; }
        set { openingSwitch = value; }
    }

    // ゲームチュートリアルへ遷移するかどうか
    private bool tutorialsSwitch = false;

    public bool TutorialsSwitch
    {
        get { return tutorialsSwitch; }
        set { tutorialsSwitch = value; }
    }

    // ゲームプレイへ遷移するかどうか
    private bool playingSwitch = false;

    public bool PlayingSwitch
    {
        get { return playingSwitch; }
        set { playingSwitch = value; }
    }

    // ゲームオプションへ遷移するかどうか
    private bool optionsSwitch = false;

    public bool OptionsSwitch
    {
        get { return optionsSwitch; }
        set { optionsSwitch = value; }
    }

    private void Start()
    {
        // ゲームの起動時であれば
        if (StaticUnits.Startup)
        {
            StaticUnits.Startup = false;

            // プラットフォーム画面を有効にする
            platformUI.SetActive(true);
            openingUI.SetActive(false);
        }
        // ゲームの起動時でなければ
        else
        {
            // オープニング画面を有効にする
            platformUI.SetActive(false);
            openingUI.SetActive(true);

            // アニメーション時間を設定する
            animTime = 3.0f;
        }
    }

    private void Update()
    {
        // アニメーション時間中は
        if (animTime > 0f)
        {
            // 時間を経過させる
            animTime -= Time.deltaTime;
        }
        else if (animTime < 0f) // アニメーション時間後は
        {
            // 時間を初期化する（正常な処理のため）
            animTime = 0f;
        }

        /* オープニング画面へ遷移する（2回の待機あり） */
        if (openingSwitch)
        {
            openingSwitch = false;

            StartCoroutine(ToOpening(0.3f, 0.5f));
        }

        /* ゲームチュートリアルへ遷移する（1回の待機あり） */
        if (tutorialsSwitch)
        {
            tutorialsSwitch = false;

            StartCoroutine(ToTutorials(0.3f));
        }

        /* ゲームプレイへ遷移する（1回の待機あり） */
        if (playingSwitch)
        {
            playingSwitch = false;

            StartCoroutine(ToPlaying(0.3f));
        }

        /* ゲームオプションへ遷移する（1回の待機あり） */
        if (optionsSwitch)
        {
            optionsSwitch = false;

            StartCoroutine(ToOptions(0.3f));
        }
    }

    private IEnumerator ToOpening(float fWT, float sWT)
    {
        // 1回目の待機
        yield return new WaitForSeconds(fWT);

        // プラットフォーム画面を無効にする
        platformUI.SetActive(false);

        // 2回目の待機
        yield return new WaitForSeconds(sWT);

        // オープニング画面を有効にする
        openingUI.SetActive(true);

        // アニメーション時間を設定する
        animTime = 3.0f;
    }

    private IEnumerator ToTutorials(float fWT)
    {
        // 1回目の待機
        yield return new WaitForSeconds(fWT);

        SceneManager.LoadScene("GameTutorialsScene");
    }

    private IEnumerator ToPlaying(float fWT)
    {
        // 1回目の待機
        yield return new WaitForSeconds(fWT);

        // ゲームの一時停止を実行する（正確なゲームプレイの時間計測を行うための処理）
        Time.timeScale = 0f;

        SceneManager.LoadScene("GamePlayingScene");
    }

    private IEnumerator ToOptions(float fWT)
    {
        // 1回目の待機
        yield return new WaitForSeconds(fWT);

        SceneManager.LoadScene("GameOptionsScene");
    }
}
