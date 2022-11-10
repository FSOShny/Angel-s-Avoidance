using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameEndingDirector : MonoBehaviour
{
    private TextMeshProUGUI difficultyText;
    private TextMeshProUGUI attackedText;
    private int level = 0; // 難易度係数
    private string[] difficulty = 
        { "Very Easy", "Easy", "Normal", "Hard", "Very Hard" }; // 難易度

    private float animTime = 3.0f; // アニメーション時間

    public float AnimTime
    {
        get { return animTime; }
    }

    private bool openingSwitch = false; // オープニングへ遷移するかどうか

    public bool OpeningSwitch
    {
        get { return openingSwitch; }
        set { openingSwitch = value; }
    }

    private int attacked; // 被弾回数

    public int Attacked
    {
        get { return attacked; }
        set { attacked = value; }
    }

    private void Start()
    {
        // 難易度表示のテキストコンポーネントを取得する
        difficultyText = GameObject.Find("Difficulty Text").GetComponent<TextMeshProUGUI>();

        // 被弾表示のテキストコンポーネントを取得する
        attackedText = GameObject.Find("Attacked Text").GetComponent<TextMeshProUGUI>();

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
        if (0f <= StaticUnits.MaxPlayerLife && StaticUnits.MaxPlayerLife <= 5.0f)
        {
            level++;

            if (StaticUnits.MaxPlayerLife == 3.0f)
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

        // 難易度表示を更新する
        difficultyText.text = "difficulty : " + difficulty[level];

        // 被弾表示を更新する
        attackedText.text = "attacked : " + attacked;
    }

    private IEnumerator ToOpening(float fWT)
    {
        // 1回目の待機
        yield return new WaitForSeconds(fWT);

        SceneManager.LoadScene("OpeningScene");
    }
}
