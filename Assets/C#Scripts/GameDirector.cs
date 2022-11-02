using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameDirector : MonoBehaviour
{
    public float time = 45f;
    public float maxLife = 5f;

    private float life;
    private Image lifeBar;
    private TextMeshProUGUI timerText;
    private int iTime;

    public float Life
    {
        get { return life; }
        set { life = value; }
    }

    private void Start()
    {
        lifeBar = GameObject.Find("Life Bar").GetComponent<Image>();
        timerText = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>() ;
        life = maxLife;
    }

    private void Update()
    {
        lifeBar.fillAmount = life / maxLife;
        time -= Time.deltaTime;
        iTime = (int)time;
        timerText.text = "Time : " + iTime.ToString();
    }
}
