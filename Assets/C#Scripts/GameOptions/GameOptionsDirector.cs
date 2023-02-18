using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOptionsDirector : MonoBehaviour
{
    // オープニングへ移動するかどうかのフラグ変数
    private bool openingSwitch = false;
    public bool OpeningSwitch
    {
        get { return openingSwitch; }
        set { openingSwitch = value; }
    }

    private void Update()
    {
        if (openingSwitch)
        {
            /* オープニングへ移動する */

            // （フラグをオフにしてから処理を行う）
            openingSwitch = false;
            StartCoroutine(ToOpening(0.3f));
        }
    }

    private IEnumerator ToOpening(float fWT)
    {
        // 待機処理（0.3秒）
        yield return new WaitForSeconds(fWT);

        // オープニングシーンをロードする
        SceneManager.LoadScene("OpeningScene");
    }
}
