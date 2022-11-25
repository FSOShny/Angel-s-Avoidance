using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private Rigidbody rigid;
    private Vector3 firstEnemyPos;
    private Vector3 enemyMove; // �G�l�~�[�̈ړ����x

    private void Start()
    {
        // ���W�b�h�{�f�B�[�R���|�[�l���g���擾����
        rigid = GetComponent<Rigidbody>();

        // �G�l�~�[�̏����ʒu��ݒ肷��
        firstEnemyPos = transform.position;

        // �G�l�~�[�̈ړ��ʂ����߂�
        enemyMove = -Vector3.one * StaticUnits.EnemyMoveSpeed;

        // ( (�Q�[���̐�������) / 3 )�b�o�߂����G�l�~�[��j�󂷂�
        Destroy(gameObject, StaticUnits.GameTimeLim / 3);
    }

    private void Update()
    {
        /* �G�l�~�[���]�[������o��ƈʒu�ƈړ��ʂ����������� */
        if (transform.position.x <= -13f || 13f <= transform.position.x ||
            transform.position.y <= 2.0f || 28f <= transform.position.y ||
            transform.position.z <= -13f || 13f <= transform.position.z)
        {
            transform.position = firstEnemyPos;
            enemyMove = -Vector3.one * StaticUnits.EnemyMoveSpeed;
        }
    }

    private void FixedUpdate()
    {
        // �G�l�~�[���ړ�������
        rigid.MovePosition(transform.position + enemyMove * Time.fixedDeltaTime);
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

        enemyMove.x = enemyMove.x * X * randX;
        enemyMove.y = enemyMove.y * Y * randY;
        enemyMove.z = enemyMove.z * Z * randZ;
    }

}
