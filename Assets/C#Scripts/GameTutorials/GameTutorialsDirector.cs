using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTutorialsDirector : MonoBehaviour
{
    [SerializeField] private GameObject pauseUi;
    [SerializeField] private List<GameObject> tutorialUis;
    private int pastPage; // 過去のページ数

    // 現在のページ数
    private int nowPage = 0;

    public int NowPage
    {
        get { return nowPage; }
        set { nowPage = value; }
    }

    // ポーズ画面へ遷移するかどうか
    private bool pauseSwitch = false;

    public bool PauseSwitch
    {
        get { return pauseSwitch; }
        set { pauseSwitch = value; }
    }

    // ゲームチュートリアルを続行するかどうか
    private bool continueSwitch = false;

    public bool ContinueSwitch
    {
        get { return continueSwitch; }
        set { continueSwitch = value; }
    }

    // オープニングへ遷移するかどうか
    private bool openingSwitch = false;

    public bool OpeningSwitch
    {
        get { return openingSwitch; }
        set { openingSwitch = value; }
    }

    // 特定のページへ遷移するかどうか
    private bool pageSwitch = false;

    public bool PageSwitch
    {
        get { return pageSwitch; }
        set { pageSwitch = value; }
    }

    private void Start()
    {
        // 1ページ目のチュートリアル画面を有効にする
        tutorialUis[0].SetActive(true);

        /* 1ページ目以外のチュートリアル画面を無効にする */
        for (int i = 1; i < tutorialUis.Count; i++)
        {
            tutorialUis[i].SetActive(false);
        }

        // ポーズ画面を無効にする
        pauseUi.SetActive(false);

        // 過去のページ数を設定する
        pastPage = nowPage;
    }

    private void Update()
    {
        /* ポーズ画面へ遷移する */
        if (pauseSwitch)
        {
            pauseSwitch = false;

            // ポーズ画面を有効にする
            pauseUi.SetActive(true);
        }

        /* ゲームチュートリアルを続行する */
        if (continueSwitch)
        {
            continueSwitch = false;

            // ポーズ画面を無効にする
            pauseUi.SetActive(false);
        }

        /* オープニングへ遷移する（1回の待機あり） */
        if (openingSwitch)
        {
            openingSwitch = false;

            StartCoroutine(ToOpening(0.3f));
        }

        /* 特定のページへ遷移する */
        if (pageSwitch)
        {
            pageSwitch = false;

            tutorialUis[pastPage].SetActive(false);
            tutorialUis[nowPage].SetActive(true);

            // 過去のページ数を設定する
            pastPage = nowPage;
        }
    }

    private IEnumerator ToOpening(float fWT)
    {
        // 1回目の待機
        yield return new WaitForSeconds(fWT);

        SceneManager.LoadScene("OpeningScene");
    }
}
