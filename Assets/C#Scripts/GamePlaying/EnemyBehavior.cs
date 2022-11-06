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
        // ���W�b�h�{�f�B�[�R���|�[�l���g���擾����
        rigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // �����I�ɓ���
        rigid.MovePosition(transform.position + velocity * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �]�[���ɏՓ˂���ƃG�l�~�[���o�E���h����
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
