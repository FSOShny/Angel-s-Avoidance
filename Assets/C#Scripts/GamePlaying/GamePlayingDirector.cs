using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GamePlayingDirector : MonoBehaviour
{
    [SerializeField] private GameObject startUI;
    [SerializeField] private GameObject PCUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject platformUI;
    [SerializeField] private GameObject defeatUI;
    [SerializeField] private GameObject victoryUI;

    private Image lifeBar;
    private TextMeshProUGUI timeText;
    private float nowLife;
    private float maxLife = 5.0f; // オプション変更対応
    private float animTime = 3.0f;
    private bool pauseSwitch = false;
    private bool platformSwitch = false;
    private float gameTime = 45f; // オプション変更対応
    private int iGameTime;
    private bool gameSwitch = true;

    public float NowLife
    {
        get { return nowLife; }
        set { nowLife = value; }
    }

    public float AnimTime
    {
        get { return animTime; }
    }

    public bool PauseSwitch
    {
        get { return pauseSwitch; }
        set { pauseSwitch = value; }
    }

    public bool PlatformSwitch
    {
        get { return platformSwitch; }
        set { platformSwitch = value; }
    }

    private void Start()
    {
        lifeBar = GameObject.Find("Life Bar").GetComponent<Image>();

        timeText = GameObject.Find("Time Text").GetComponent<TextMeshProUGUI>() ;

        startUI.SetActive(true);
        PCUI.SetActive(false);
        pauseUI.SetActive(false);
        platformUI.SetActive(false);
        defeatUI.SetActive(false);
        victoryUI.SetActive(false);

        nowLife = maxLife;
    }

    private void Update()
    {
        if (animTime > 0f)
        {
            animTime -= Time.deltaTime;
            Time.timeScale = 0f;
        }
        else if (animTime < 0f)
        {
            animTime = 0f;
            Time.timeScale = 1.0f;

            if (gameSwitch)
            {
                startUI.SetActive(false);
                PCUI.SetActive(true);
            }
            else
            {
                SceneManager.LoadScene("GameEndingScene");
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !pauseSwitch)
        {
            pauseSwitch = true;
        }

        if (pauseSwitch)
        {
            Time.timeScale = 0f;
            pauseUI.SetActive(true);
        }
        else
        {
            Time.timeScale = 1.0f;
            pauseUI.SetActive(false);
        }

        if (platformSwitch)
        {
            platformUI.SetActive(true);
        }
        else
        {
            platformUI.SetActive(false);
        }

        lifeBar.fillAmount = nowLife / maxLife;

        if (nowLife == 0f)
        {
            PCUI.SetActive(false);
            defeatUI.SetActive(true);
        }

        gameTime -= Time.deltaTime;

        iGameTime = (int)gameTime;

        timeText.text = "Time : " + iGameTime.ToString();

        if (gameTime < 0f)
        {
            gameTime = 0f;

            PCUI.SetActive(false);
            victoryUI.SetActive(true);

            gameSwitch = false;
            animTime = 3.0f;
        }
    }
}
