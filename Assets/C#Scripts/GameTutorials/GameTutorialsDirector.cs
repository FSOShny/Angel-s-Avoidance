using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTutorialsDirector : MonoBehaviour
{
    [SerializeField] private List<GameObject> tutorialUIs;
    private int nowPage = 0; // 現在のページ数

    // オープニングへ遷移するかどうか
    private bool openingSwitch = false;

    public bool OpeningSwitch
    {
        get { return openingSwitch; }
        set { openingSwitch = value; }
    }

    // 次のページへ遷移するかどうか
    private bool nextSwitch = false;

    public bool NextSwitch
    {
        get { return nextSwitch; }
        set { nextSwitch = value; }
    }

    // 前のページへ遷移するかどうか
    private bool prevSwitch = false;

    public bool PrevSwitch
    {
        get { return prevSwitch; }
        set { prevSwitch = value; }
    }

    private void Start()
    {
        // 1ページ目のチュートリアル画面を有効にする
        tutorialUIs[0].SetActive(true);

        /* 1ページ目以外のチュートリアル画面を無効にする */
        for (int i = 1; i < tutorialUIs.Count; i++)
        {
            tutorialUIs[i].SetActive(false);
        }
    }

    private void Update()
    {
        /* オープニングへ遷移する（1回の待機あり） */
        if (openingSwitch)
        {
            openingSwitch = false;

            StartCoroutine(ToOpening(0.3f));
        }

        /* 次のページへ遷移する */
        if (nextSwitch)
        {
            nextSwitch = false;

            // 現在のページ数を増やす
            nowPage++;

            tutorialUIs[nowPage - 1].SetActive(false);
            tutorialUIs[nowPage].SetActive(true);
        }

        /* 前のページへ遷移する */
        if (prevSwitch)
        {
            prevSwitch = false;

            // 現在のページ数を減らす
            nowPage--;

            tutorialUIs[nowPage + 1].SetActive(false);
            tutorialUIs[nowPage].SetActive(true);
        }
    }

    private IEnumerator ToOpening(float fWT)
    {
        // 1回目の待機
        yield return new WaitForSeconds(fWT);

        SceneManager.LoadScene("OpeningScene");
    }
}
