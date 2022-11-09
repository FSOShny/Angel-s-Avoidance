using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

    private void Start()
    {
        // エネミーを生成していく（2回以上の待機あり）
        StartCoroutine(EnemyGenerate(2.0f, 4.0f));
    }

    private IEnumerator EnemyGenerate(float fWT, float sWT)
    {
        // 1回目の待機
        yield return new WaitForSeconds(fWT);

        // 1体目のエネミーを生成する
        Instantiate(enemy, new Vector3(9f, 20f, 9f), Quaternion.identity);

        while (true)
        {
            // 2+α回目の待機（αは0以上の整数）
            yield return new WaitForSeconds(sWT);

            //　エネミーの生成位置をランダムに決める
            float randX = Random.Range(-11.9f, 11.9f);
            float randY = Random.Range(20f, 25f);
            float randZ = Random.Range(-11.9f, 11.9f);
            Vector3 initPos = new Vector3(randX, randY, randZ);

            // エネミーを生成する
            Instantiate(enemy, initPos, Quaternion.identity);
        }
    }
}
