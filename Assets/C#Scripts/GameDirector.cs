using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameDirector : MonoBehaviour
{
    public float time = 45;

    private TextMeshProUGUI timerText;
    private TextMeshProUGUI damageText;
    private int iTime;
    private int damaged = 0;

    private void Start()
    {
        timerText = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>() ;
        damageText = GameObject.Find("Damage").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        time -= Time.deltaTime;
        iTime = (int)time;
        timerText.text = "Time : " + iTime.ToString();
        damageText.text = "Damaged : " + damaged.ToString();
    }
}
