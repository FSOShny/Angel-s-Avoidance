using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTutorialsDirector : MonoBehaviour
{
    // UIオブジェクト
    [SerializeField] private GameObject pauseUi;
    [SerializeField] private List<GameObject> tutorialUis;

    // 以前のページ数
    private int pastPage;

    // 現在のページ数
    private int nowPage = 0;
    public int NowPage
    {
        get { return nowPage; }
        set { nowPage = value; }
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

    // オープニングへ移動するかどうかのフラグ変数
    private bool openingSwitch = false;
    public bool OpeningSwitch
    {
        get { return openingSwitch; }
        set { openingSwitch = value; }
    }

    // 設定したページへ移動するかどうかのフラグ変数
    private bool pageSwitch = false;
    public bool PageSwitch
    {
        get { return pageSwitch; }
        set { pageSwitch = value; }
    }

    private void Start()
    {
        // チュートリアル画面（ページ1）を有効にする
        tutorialUis[0].SetActive(true);

        // チュートリアル画面（ページ1以外）、ポーズ画面を無効にする
        for (int i = 1; i < tutorialUis.Count; i++)
        {
            tutorialUis[i].SetActive(false);
        }
        pauseUi.SetActive(false);

        // 以前のページ数を設定する
        pastPage = nowPage;
    }

    private void Update()
    {
        if (pauseSwitch)
        {
            /* ポーズ画面を有効にする */

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
        }

        if (openingSwitch)
        {
            /* オープニングへ移動する */

            // （フラグをオフにしてから処理を行う）
            openingSwitch = false;
            StartCoroutine(ToOpening(0.5f));
        }

        if (pageSwitch)
        {
            /* 設定したページへ移動するへ移動する */

            // （フラグをオフにしてから処理を行う）
            pageSwitch = false;
            tutorialUis[pastPage].SetActive(false);
            tutorialUis[nowPage].SetActive(true);

            // 以前のページ数を設定する
            pastPage = nowPage;
        }
    }

    private IEnumerator ToOpening(float fWT)
    {
        // 待機処理（0.5秒）
        yield return new WaitForSeconds(fWT);

        // オープニングシーンをロードする
        SceneManager.LoadScene("OpeningScene");
    }
}
