using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningDirector : MonoBehaviour
{
    [SerializeField] private GameObject platformUI;
    [SerializeField] private GameObject openingUI;

    private static bool platform = true;
    private float animTime = 0f;
    private bool opening = false;
    private bool playing = false;
    private bool options = false;

    public float AnimTime
    {
        get { return animTime; }
    }

    public bool Opening
    {
        get { return opening; }
        set { opening = value; }
    }

    public bool Playing
    {
        get { return playing; }
        set { playing = value; }
    }

    public bool Options
    {
        get { return options; }
        set { options = value; }
    }

    void Start()
    {
        if (platform)
        {
            platformUI.SetActive(true);
            openingUI.SetActive(false);
        }
        else
        {
            platformUI.SetActive(false);
            openingUI.SetActive(true);
            animTime = 3.0f;
        }
    }

    void Update()
    {
        if (animTime > 0f)
        {
            animTime -= Time.deltaTime;
        }
        else if (animTime < 0f)
        {
            animTime = 0f;
        }

        if (opening)
        {
            opening = false;
            platform = false;
            StartCoroutine(ToOpening(0.3f, 0.5f));
        }

        if (playing)
        {
            playing = false;
            StartCoroutine(ToPlaying(0.3f));
        }

        if (options)
        {
            options = false;
            StartCoroutine(ToOptions(0.3f));
        }
    }

    private IEnumerator ToOpening(float fWT, float sWT)
    {
        yield return new WaitForSeconds(fWT);

        platformUI.SetActive(false);

        yield return new WaitForSeconds(sWT);

        openingUI.SetActive(true);

        animTime = 3.0f;

        yield break;
    }

    private IEnumerator ToPlaying(float fWT)
    {
        yield return new WaitForSeconds(fWT);

        SceneManager.LoadScene("GamePlayingScene");

        yield break;
    }

    private IEnumerator ToOptions(float fWT)
    {
        yield return new WaitForSeconds(fWT);

        SceneManager.LoadScene("GameOptionsScene");

        yield break;
    }
}
