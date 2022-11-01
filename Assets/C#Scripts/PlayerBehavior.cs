using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float moveSpeed = 10f; // プレイヤーの移動速度

    private new Camera camera;
    private Rigidbody rigid;
    private float hInput;
    private float vInput;
    private bool depthSwitch = true;
    private bool moveCan = true;
    public float invTime;

    // Start is called before the first frame update
    private void Start()
    {
        // カメラの角度を取得
        camera = Camera.main;

        // リジッドボディーコンポーネントを取得する
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        // プレイヤーの前後（上下）の移動量を求める
        hInput = Input.GetAxis("Horizontal") * moveSpeed;

        // プレイヤーの左右の移動量を求める
        vInput = Input.GetAxis("Vertical") * moveSpeed;

        // 移動タイプを変更する
        if (Input.GetKeyDown(KeyCode.E))
        {
            depthSwitch = !depthSwitch;
        }

        if (invTime > 0)
        {
            invTime -= Time.deltaTime;
            Debug.Log(invTime);
        }
        else if (invTime < 0)
        {
            rigid.velocity = Vector3.zero;
            moveCan = true;
        }
    }

    private void FixedUpdate()
    {
        if (moveCan)
        {
            if (depthSwitch) // 移動タイプが「前後」である場合は
            {
                // プレイヤーを前後左右に移動させる
                NormalMove(0, 1);
            }
            else // 移動タイプが「上下」である場合は
            {
                // プレイヤーを上下左右に移動させる
                NormalMove(1, 0);
            }
        }

        // プレイヤーの角度をカメラの角度に合わせる
        transform.rotation = camera.transform.rotation;
    }

    public void NormalMove(int Y, int Z)
    {
         rigid.MovePosition(transform.position + 
             transform.right * hInput * Time.fixedDeltaTime + 
             transform.up * vInput * Time.fixedDeltaTime * Y + 
             transform.forward * vInput * Time.fixedDeltaTime * Z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Front")
        {
            rigid.AddForce(Vector3.back, ForceMode.Impulse);
        }
        else if (collision.gameObject.name == "Left")
        {
            rigid.AddForce(Vector3.right, ForceMode.Impulse);
        }
        else if (collision.gameObject.name == "Back")
        {
            rigid.AddForce(Vector3.forward, ForceMode.Impulse);
        }
        else if (collision.gameObject.name == "Right")
        {
            rigid.AddForce(Vector3.left, ForceMode.Impulse);
        }
        else if (collision.gameObject.name == "Top")
        {
            rigid.AddForce(Vector3.down, ForceMode.Impulse);
        }

        invTime = 3;
        moveCan = false;
    }
}