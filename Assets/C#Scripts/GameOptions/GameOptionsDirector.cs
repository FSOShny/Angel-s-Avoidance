using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOptionsDirector : MonoBehaviour
{
    private bool opening = false;

    public bool Opening
    {
        get { return opening; }
        set { opening = value; }
    }

    void Update()
    {
        if (opening)
        {
            opening = false;
            StartCoroutine(ToOpening(0.3f));
        }
    }

    private IEnumerator ToOpening(float fWT)
    {
        yield return new WaitForSeconds(fWT);

        SceneManager.LoadScene("OpeningScene");

        yield break;
    }
}
