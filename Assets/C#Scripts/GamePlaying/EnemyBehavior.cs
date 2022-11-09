using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private Rigidbody rigid;
    private Vector3 velocity = new Vector3(-1.0f, -1.0f, -1.0f); // エネミーの移動速度

    public void Repulsion(int X, int Y, int Z)
    {
        // エネミーの反発倍率をランダムで決める
        float randX = Random.Range(0.9f, 1.1f);
        float randY = Random.Range(0.9f, 1.1f);
        float randZ = Random.Range(0.9f, 1.1f);

        velocity.x = velocity.x * X * randX;
        velocity.y = velocity.y * Y * randY;
        velocity.z = velocity.z * Z * randZ;
    }

    private void OutOfArea(float X, float Y, float Z)
    {
        rigid.position = new Vector3(X, Y, Z);

        velocity = new Vector3(-1.0f, -1.0f, -1.0f) * StaticUnits.EnemyMoveSpeed;
    }

    private void Start()
    {
        // リジッドボディーコンポーネントを取得する
        rigid = GetComponent<Rigidbody>();

        // エネミーの移動速度を決める
        velocity *= StaticUnits.EnemyMoveSpeed;
    }

    private void FixedUpdate()
    {
        // エネミーを移動させる
        rigid.MovePosition(transform.position + velocity * Time.fixedDeltaTime);

        /* 領域外エネミーの位置と移動速度を更新する */
        if (rigid.position.x > 12f)
        {
            OutOfArea(12f, rigid.position.y, rigid.position.z);
        }
        else if (rigid.position.x < -12f)
        {
            OutOfArea(-12f, rigid.position.y, rigid.position.z);
        }
        if (rigid.position.y > 27f)
        {
            OutOfArea(rigid.position.x, 27f, rigid.position.z);
        }
        else if (rigid.position.y < 3.0f)
        {
            OutOfArea(rigid.position.x, 3.0f, rigid.position.z);
        }
        if (rigid.position.z > 12f)
        {
            OutOfArea(rigid.position.x, rigid.position.y, 12f);
        }
        else if (rigid.position.z < -12f)
        {
            OutOfArea(rigid.position.x, rigid.position.y, -12f);
        }
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
}
