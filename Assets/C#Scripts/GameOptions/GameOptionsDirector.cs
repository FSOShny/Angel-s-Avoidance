using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOptionsDirector : MonoBehaviour
{
    // オープニングへ遷移するかどうか
    private bool openingSwitch = false;

    public bool OpeningSwitch
    {
        get { return openingSwitch; }
        set { openingSwitch = value; }
    }

    private void Update()
    {
        /* オープニングへ遷移する（1回の待機あり） */
        if (openingSwitch)
        {
            openingSwitch = false;

            StartCoroutine(ToOpening(0.3f));
        }
    }

    private IEnumerator ToOpening(float fWT)
    {
        // 1回目の待機
        yield return new WaitForSeconds(fWT);

        SceneManager.LoadScene("OpeningScene");
    }
}
