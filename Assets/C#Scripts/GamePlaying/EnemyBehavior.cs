using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private Rigidbody rigid;
    private GamePlayingDirector director;
    private Vector3 enemyMove; // エネミーの移動速度

    private void Start()
    {
        // リジッドボディーコンポーネントを取得する
        rigid = GetComponent<Rigidbody>();

        // エネミーの移動量を決める
        enemyMove = -Vector3.one * StaticUnits.EnemyMoveSpeed;

        // ゲームプレイディレクターを取得する
        director = GameObject.FindGameObjectWithTag("Director").GetComponent<GamePlayingDirector>();

        // ( (ゲームの制限時間) / 3 )秒経過したエネミーを破壊する
        Destroy(gameObject, StaticUnits.GameTimeLim / 3);
    }

    private void FixedUpdate()
    {
        // エネミーを移動させる
        rigid.MovePosition(transform.position + enemyMove * Time.fixedDeltaTime);

        // エネミーがゾーンから出ないようにする
        rigid.position = new Vector3(
            Mathf.Clamp(rigid.position.x, -13.5f, 13.5f),
            Mathf.Clamp(rigid.position.y, -26.5f, 26.5f),
            Mathf.Clamp(rigid.position.z, -13.5f, 13.5f));
    }

    private void OnCollisionEnter(Collision collision)
    {
        /* ゾーンやプレイヤーに衝突するとエネミーが反発する */
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

        enemyMove.x = enemyMove.x * X * randX;
        enemyMove.y = enemyMove.y * Y * randY;
        enemyMove.z = enemyMove.z * Z * randZ;
    }
}
