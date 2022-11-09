using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndingDirector : MonoBehaviour
{
    private bool opening = false; // オープニングへ遷移するかどうか

    public bool Opening
    {
        get { return opening; }
        set { opening = value; }
    }

    void Update()
    {
        /* オープニングへ遷移する（1回の待機あり） */
        if (opening)
        {
            opening = false;

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
