using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningDirector : MonoBehaviour
{
    [SerializeField] private GameObject platformUI;
    [SerializeField] private GameObject openingUI;

    private bool opening = false;
    private bool playing = false;

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

    void Start()
    {
        platformUI.SetActive(true);
        openingUI.SetActive(false);
    }

    void Update()
    {
        if (opening)
        {
            StartCoroutine(ToOpening());
            opening = false;
        }

        if (playing)
        {
            StartCoroutine(ToPlaying());
            SceneManager.LoadScene("Game Playing");
            opening = false;
        }
    }

    private IEnumerator ToOpening()
    {
        yield return new WaitForSeconds(0.2f);

        platformUI.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        openingUI.SetActive(true);
    }

    private IEnumerator ToPlaying()
    {
        yield return new WaitForSeconds(0.2f);

        SceneManager.LoadScene("PlayingScene");
    }
}
