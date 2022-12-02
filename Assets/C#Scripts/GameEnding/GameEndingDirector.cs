using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameEndingDirector : MonoBehaviour
{
    private TextMeshProUGUI difficultyText;
    private TextMeshProUGUI damagedText;
    private TextMeshProUGUI fatiguedText;
    private int level = 0; // 難易度係数
    private string[] difficulty = {
        "Practice", "Very Easy", "Easy", "Normal", 
        "Hard", "Very Hard", "Angel" 
    }; // 難易度評価

    // アニメーション時間
    private float animTime = 4.0f;

    public float AnimTime
    {
        get { return animTime; }
    }

    // オープニングへ遷移するかどうか
    private bool openingSwitch = false;

    public bool OpeningSwitch
    {
        get { return openingSwitch; }
        set { openingSwitch = value; }
    }

    // 被弾回数
    private int damaged;

    public int Damaged
    {
        get { return damaged; }
        set { damaged = value; }
    }

    // 疲労状態回数
    private int fatigued;

    public int Fatigued
    {
        get { return fatigued; }
        set { fatigued = value; }
    }

    private void Start()
    {
        // 難易度評価のテキストコンポーネントを取得する
        difficultyText = GameObject.Find("Difficulty Text").GetComponent<TextMeshProUGUI>();

        // 被弾回数評価のテキストコンポーネントを取得する
        damagedText = GameObject.Find("Damaged Text").GetComponent<TextMeshProUGUI>();

        // 疲労状態回数評価のテキストコンポーネントを取得する
        fatiguedText = GameObject.Find("Fatigued Text").GetComponent<TextMeshProUGUI>();

        /* ゲームの制限時間に応じて難易度係数を増やす */
        if (StaticUnits.GameTimeLim >= 45)
        {
            level++;

            if (StaticUnits.GameTimeLim == 60)
            {
                level++;
            }
        }

        /* エネミーの移動速度係数に応じて難易度係数を増やす */
        if (StaticUnits.EnemyMoveSpeed >= 8)
        {
            level++;

            if (StaticUnits.EnemyMoveSpeed == 12)
            {
                level++;
            }
        }

        /* プレイヤーの体力最大値に応じて難易度係数を増やす */
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
        // アニメーション時間中は
        if (animTime > 0f)
        {
            // 時間を経過させる
            animTime -= Time.deltaTime;
        }
        else if (animTime < 0f) // アニメーション時間後は
        {
            // 時間を初期化する（正常な処理のため）
            animTime = 0f;
        }

        /* オープニングへ遷移する（1回の待機あり） */
        if (openingSwitch)
        {
            openingSwitch = false;

            StartCoroutine(ToOpening(0.3f));
        }

        // 難易度評価を更新する
        difficultyText.text = "difficulty : " + difficulty[level];

        // 被弾回数評価を更新する
        damagedText.text = "damaged : " + damaged;

        // 疲労状態回数評価を更新する
        fatiguedText.text = "fatigued : " + fatigued;
    }

    private IEnumerator ToOpening(float fWT)
    {
        // 1回目の待機
        yield return new WaitForSeconds(fWT);

        SceneManager.LoadScene("OpeningScene");
    }
}
