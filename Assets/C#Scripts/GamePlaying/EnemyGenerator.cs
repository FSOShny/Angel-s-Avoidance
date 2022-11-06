using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemy; // エネミープレハブ
    public float term = 5f; // エネミーの生成間隔

    private float delta = 0f;

    private void Start()
    {
        // 一体目のエネミーを生成する
        Instantiate(enemy, new Vector3(10f, 20f, 10f), Quaternion.identity);
    }

    private void Update()
    {
        // デルタ時間を更新する
        delta += Time.deltaTime;

        if (delta > term) // 一定のデルタ時間になると
        {
            // ランダムでエネミーの生成位置を決める
            float x = Random.Range(-11.9f, 11.9f);
            float y = Random.Range(20f, 25f);
            float z = Random.Range(-11.9f, 11.9f);
            Vector3 initPos = new Vector3(x, y, z);

            // 二体目以降のエネミーを生成する
            Instantiate(enemy, initPos, Quaternion.identity);

            // デルタ時間を初期化する
            delta = 0f;
        }
    }
}
