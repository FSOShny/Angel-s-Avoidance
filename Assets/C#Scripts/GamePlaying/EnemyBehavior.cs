using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    // 剛体コンポーネント
    private Rigidbody rigid;

    // エネミーの初期位置
    private Vector3 firstEnemyPos;

    // エネミーの移動速度
    private Vector3 enemyMove;

    private void Start()
    {
        // 剛体コンポーネントを取得する
        rigid = GetComponent<Rigidbody>();

        // エネミーの初期位置、移動量を設定する
        firstEnemyPos = transform.position;
        enemyMove = -Vector3.one * StaticUnits.EnemyMoveSpeed;

        // ( (ゲームの時間) / 3 )秒経過したエネミーを破壊する
        Destroy(gameObject, StaticUnits.GameTime / 3);
    }

    private void Update()
    {
        if (transform.position.x <= -13f || 13f <= transform.position.x ||
            transform.position.y <= 2.0f || 28f <= transform.position.y ||
            transform.position.z <= -13f || 13f <= transform.position.z)
        {
            /* エネミーがゾーンから出ると位置と移動量を初期化する */

            transform.position = firstEnemyPos;
            enemyMove = -Vector3.one * StaticUnits.EnemyMoveSpeed;
        }
    }

    private void FixedUpdate()
    {
        // エネミーを移動させる
        rigid.MovePosition(transform.position + enemyMove * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ゾーンやプレイヤーに衝突するとエネミーが反発する
        if (collision.gameObject.name == "Bottom" || collision.gameObject.name == "Top")
        {
            Repulsion(1, -1, 1);
        }
        else if (collision.gameObject.name == "Front" || collision.gameObject.name == "Back")
        {
            Repulsion(1, 1, -1);
        }
        else if (collision.gameObject.name == "Left" || collision.gameObject.name == "Right")
        {
            Repulsion(-1, 1, 1);
        }
        else
        {
            Repulsion(-1, -1, -1);
        }
    }

    private void Repulsion(int X, int Y, int Z)
    {
        // エネミーの反発倍率をランダムに決める
        float randX = Random.Range(0.75f, 1.25f);
        float randY = Random.Range(0.75f, 1.25f);
        float randZ = Random.Range(0.75f, 1.25f);

        // エネミーの移動量を設定する
        enemyMove.x *= randX * X;
        enemyMove.y *= randY * Y;
        enemyMove.z *= randZ * Z;
    }
}
