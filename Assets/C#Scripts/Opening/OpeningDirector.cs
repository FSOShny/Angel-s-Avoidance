using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningDirector : MonoBehaviour
{
    [SerializeField] private GameObject platformUI;
    [SerializeField] private GameObject openingUI;
    
    private float animTime = 0f; // アニメーション時間

    public float AnimTime
    {
        get { return animTime; }
    }

    private bool opening = false; // オープニング画面へ遷移するかどうか

    public bool Opening
    {
        get { return opening; }
        set { opening = value; }
    }

    private bool playing = false; // ゲームプレイへ遷移するかどうか

    public bool Playing
    {
        get { return playing; }
        set { playing = value; }
    }

    private bool options = false; // ゲームオプションへ遷移するかどうか

    public bool Options
    {
        get { return options; }
        set { options = value; }
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
        // そうでなければ
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
        if (opening)
        {
            opening = false;

            StartCoroutine(ToOpening(0.3f, 0.5f));
        }

        /* ゲームプレイへ遷移する（1回の待機あり） */
        if (playing)
        {
            playing = false;

            StartCoroutine(ToPlaying(0.3f));
        }

        /* ゲームオプションへ遷移する（1回の待機あり） */
        if (options)
        {
            options = false;

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

    private IEnumerator ToPlaying(float fWT)
    {
        // 1回目の待機
        yield return new WaitForSeconds(fWT);

        SceneManager.LoadScene("GamePlayingScene");
    }

    private IEnumerator ToOptions(float fWT)
    {
        // 1回目の待機
        yield return new WaitForSeconds(fWT);

        SceneManager.LoadScene("GameOptionsScene");
    }
}
