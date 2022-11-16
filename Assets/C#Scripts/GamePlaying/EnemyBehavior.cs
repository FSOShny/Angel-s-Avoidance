using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private Rigidbody rigid;
    private Vector3 enemyVelo; // �G�l�~�[�̈ړ����x

    private void Start()
    {
        // ���W�b�h�{�f�B�[�R���|�[�l���g���擾����
        rigid = GetComponent<Rigidbody>();

        // �G�l�~�[�̈ړ����x�����߂�
        enemyVelo = -Vector3.one * StaticUnits.EnemyMoveSpeed;

        // ( (�Q�[���̐�������) / 3 )�b�o�߂����G�l�~�[��j�󂷂�
        Destroy(gameObject, StaticUnits.GameTimeLim / 3);
    }

    private void FixedUpdate()
    {
        // �G�l�~�[���ړ�������
        rigid.MovePosition(transform.position + enemyVelo * Time.fixedDeltaTime);

        /* �̈�O�G�l�~�[�̈ʒu���X�V���� */
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

    private void Repulsion(int X, int Y, int Z)
    {
        // �G�l�~�[�̔����{���������_���Ɍ��߂�
        float randX = Random.Range(0.75f, 1.25f);
        float randY = Random.Range(0.75f, 1.25f);
        float randZ = Random.Range(0.75f, 1.25f);

        enemyVelo.x = enemyVelo.x * X * randX;
        enemyVelo.y = enemyVelo.y * Y * randY;
        enemyVelo.z = enemyVelo.z * Z * randZ;
    }
}
