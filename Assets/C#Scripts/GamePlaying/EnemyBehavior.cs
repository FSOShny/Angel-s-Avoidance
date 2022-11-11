using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private Rigidbody rigid;
    private Vector3 velocity = -Vector3.one; // エネミーの移動速度

    private void Start()
    {
        // リジッドボディーコンポーネントを取得する
        rigid = GetComponent<Rigidbody>();

        // エネミーの移動速度を決める
        velocity *= StaticUnits.EnemyMoveSpeed;

        // 15秒経過したエネミーを破壊する
        Destroy(gameObject, 15f);
    }

    private void FixedUpdate()
    {
        // エネミーを移動させる
        rigid.MovePosition(transform.position + velocity * Time.fixedDeltaTime);

        /* 領域外エネミーの位置を更新する */
        if (rigid.position.x > 11.5f)
        {
            rigid.position = new Vector3(11.5f, rigid.position.y, rigid.position.z);
        }
        else if (rigid.position.x < -11.5f)
        {
            rigid.position = new Vector3(-11.5f, rigid.position.y, rigid.position.z);
        }
        if (rigid.position.y > 26.5f)
        {
            rigid.position = new Vector3(rigid.position.x, 26.5f, rigid.position.z);
        }
        else if (rigid.position.y < 3.5f)
        {
            rigid.position = new Vector3(rigid.position.x, 3.5f, rigid.position.z);
        }
        if (rigid.position.z > 11.5f)
        {
            rigid.position = new Vector3(rigid.position.x, rigid.position.y, 11.5f);
        }
        else if (rigid.position.z < -11.5f)
        {
            rigid.position = new Vector3(rigid.position.x, rigid.position.y, -11.5f);
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

    private void Repulsion(int X, int Y, int Z)
    {
        // エネミーの反発倍率をランダムに決める
        float randX = Random.Range(0.75f, 1.25f);
        float randY = Random.Range(0.75f, 1.25f);
        float randZ = Random.Range(0.75f, 1.25f);

        velocity.x = velocity.x * X * randX;
        velocity.y = velocity.y * Y * randY;
        velocity.z = velocity.z * Z * randZ;
    }
}
