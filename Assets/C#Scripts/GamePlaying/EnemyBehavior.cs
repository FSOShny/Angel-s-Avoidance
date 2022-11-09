using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private Rigidbody rigid;
    private Vector3 velocity = new Vector3(-1.0f, -1.0f, -1.0f); // �G�l�~�[�̈ړ����x

    public void Repulsion(int X, int Y, int Z)
    {
        // �G�l�~�[�̔����{���������_���Ō��߂�
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
        // ���W�b�h�{�f�B�[�R���|�[�l���g���擾����
        rigid = GetComponent<Rigidbody>();

        // �G�l�~�[�̈ړ����x�����߂�
        velocity *= StaticUnits.EnemyMoveSpeed;
    }

    private void FixedUpdate()
    {
        // �G�l�~�[���ړ�������
        rigid.MovePosition(transform.position + velocity * Time.fixedDeltaTime);

        /* �̈�O�G�l�~�[�̈ʒu�ƈړ����x���X�V���� */
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
        /* �]�[����v���C���[�ɏՓ˂���ƃG�l�~�[���������� */
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
