using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float moveSpeed = -10f;

    private Rigidbody rigid;
    private Vector3 velocity = new Vector3(1f, 1f, 1f);

    private void Start()
    {
        // リジッドボディーコンポーネントを取得する
        rigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // 直線的に動く
        rigid.MovePosition(transform.position + velocity * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ゾーンに衝突するとエネミーがバウンドする
        if (collision.gameObject.name == "Bottom" || collision.gameObject.name == "Top")
        {
            Bounding(1, -1, 1);
        }
        else if (collision.gameObject.name == "Front" || collision.gameObject.name == "Back")
        {
            Bounding(1, 1, -1);
        }
        else if (collision.gameObject.name == "Left" || collision.gameObject.name == "Right")
        {
            Bounding(-1, 1, 1);
        }
        else
        {
            Bounding(-1, -1, -1);
        }
    }

    public void Bounding(int X, int Y, int Z)
    {
        velocity.x = velocity.x * X;
        velocity.y = velocity.y * Y;
        velocity.z = velocity.z * Z;
    }
}
