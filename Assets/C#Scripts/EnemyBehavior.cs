using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public int pow = 5; // 力の大きさ

    private Rigidbody rigid;
    private bool firstBound = true;

    private void Start()
    {
        // リジッドボディーコンポーネントを取得する
        rigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (firstBound) // ゾーンに衝突するまでは
        {
            // 直線的に動く
            rigid.AddForce(pow, pow, pow);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ゾーンに衝突するとダークボールがバウンドする
        if (collision.gameObject.name == "Bottom")
        {
            Bounding(0, pow, 0);
        }
        else if (collision.gameObject.name == "Front")
        {
            Bounding(0, 0, -pow);
        }
        else if (collision.gameObject.name == "Left")
        {
            Bounding(pow, 0, 0);
        }
        else if (collision.gameObject.name == "Back")
        {
            Bounding(0, 0, pow);
        }
        else if (collision.gameObject.name == "Right")
        {
            Bounding(-pow, 0, 0);
        }
        else if (collision.gameObject.name == "Top")
        {
            Bounding(0, -pow, 0);
        }
    }

    public void Bounding(int X, int Y, int Z)
    {
        rigid.AddForce(X, Y, Z, ForceMode.Impulse);

        if (firstBound)
        {
            firstBound = false;
        }
    }
}
