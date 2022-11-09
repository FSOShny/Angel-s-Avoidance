using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

    private void Start()
    {
        // エネミーを生成していく（2回以上の待機あり）
        StartCoroutine(EnemyGenerate(2.0f, 3.0f));
    }

    private IEnumerator EnemyGenerate(float fWT, float sWT)
    {
        // 1回目の待機
        yield return new WaitForSeconds(fWT);

        // 1体目のエネミーを生成する
        Instantiate(enemy, new Vector3(5.0f, 15f, 5.0f), Quaternion.identity);

        while (true)
        {
            // 2+α回目の待機（αは0以上の整数）
            yield return new WaitForSeconds(sWT);

            //　エネミーの生成位置をランダムに決める
            float randX = Random.Range(-11.5f, 11.5f);
            float randY = Random.Range(15f, 26.5f);
            float randZ = Random.Range(-11.5f, 11.5f);
            Vector3 enemyPos = new Vector3(randX, randY, randZ);

            // エネミーを生成する
            Instantiate(enemy, enemyPos, Quaternion.identity);
        }
    }
}
