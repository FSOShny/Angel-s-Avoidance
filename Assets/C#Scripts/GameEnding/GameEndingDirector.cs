using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameEndingDirector : MonoBehaviour
{
    // テキストコンポーネント
    private TextMeshProUGUI difficultyText;
    private TextMeshProUGUI damagedText;
    private TextMeshProUGUI fatiguedText;

    // 難易度係数
    private int level = 0;

    // 難易度評価の文字列
    private string[] difficulty = {
        "Practice", "Very Easy", "Easy", "Normal", 
        "Hard", "Very Hard", "Angel" 
    };

    // アニメーション時間
    private float animTime = 4.0f;
    public float AnimTime
    {
        get { return animTime; }
    }

    // オープニングへ移動するかどうかのフラグ変数
    private bool openingSwitch = false;
    public bool OpeningSwitch
    {
        get { return openingSwitch; }
        set { openingSwitch = value; }
    }

    // 被弾した回数
    private int damaged;
    public int Damaged
    {
        get { return damaged; }
        set { damaged = value; }
    }

    // 疲労状態になった回数
    private int fatigued;
    public int Fatigued
    {
        get { return fatigued; }
        set { fatigued = value; }
    }

    private void Start()
    {
        // 各コンポーネントを取得する
        difficultyText = GameObject.Find("Difficulty Text").GetComponent<TextMeshProUGUI>();
        damagedText = GameObject.Find("Damaged Text").GetComponent<TextMeshProUGUI>();
        fatiguedText = GameObject.Find("Fatigued Text").GetComponent<TextMeshProUGUI>();

        // ゲームの時間に応じて難易度係数を増やす
        if (StaticUnits.GameTime >= 45)
        {
            level++;

            if (StaticUnits.GameTime == 60)
            {
                level++;
            }
        }

        // エネミーの移動速度係数に応じて難易度係数を増やす
        if (StaticUnits.EnemyMoveSpeed >= 8)
        {
            level++;

            if (StaticUnits.EnemyMoveSpeed == 12)
            {
                level++;
            }
        }

        // プレイヤーの体力最大値に応じて難易度係数を増やす
        if (StaticUnits.MaxPlayerLives <= 3.0f)
        {
            level++;

            if (StaticUnits.MaxPlayerLives == 2.0f)
            {
                level++;
            }
        }
    }

    private void Update()
    {
        if (animTime > 0f)
        {
            /* アニメーションのときは
               その時間を経過させる   */

            animTime -= Time.deltaTime;
        }
        else if (animTime < 0f)
        {
            /* アニメーションが終了したときは
               時間を初期化する（無駄な処理を省くため） */

            animTime = 0f;
        }

        if (openingSwitch)
        {
            /* オープニングへ移動する */

            // （フラグをオフにしてから処理を行う）
            openingSwitch = false;
            StartCoroutine(ToOpening(0.5f));
        }

        // 各プレイ評価を表示する
        difficultyText.text = "difficulty : " + difficulty[level];
        damagedText.text = "damaged : " + damaged;
        fatiguedText.text = "fatigued : " + fatigued;
    }

    private IEnumerator ToOpening(float fWT)
    {
        // 待機処理（0.5秒）
        yield return new WaitForSeconds(fWT);

        // オープニングシーンをロードする
        SceneManager.LoadScene("OpeningScene");
    }
}
