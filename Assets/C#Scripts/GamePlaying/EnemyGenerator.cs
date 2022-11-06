using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemy; // �G�l�~�[�v���n�u
    public float term = 5f; // �G�l�~�[�̐����Ԋu

    private float delta = 0f;

    private void Start()
    {
        // ��̖ڂ̃G�l�~�[�𐶐�����
        Instantiate(enemy, new Vector3(10f, 20f, 10f), Quaternion.identity);
    }

    private void Update()
    {
        // �f���^���Ԃ��X�V����
        delta += Time.deltaTime;

        if (delta > term) // ���̃f���^���ԂɂȂ��
        {
            // �����_���ŃG�l�~�[�̐����ʒu�����߂�
            float x = Random.Range(-11.9f, 11.9f);
            float y = Random.Range(20f, 25f);
            float z = Random.Range(-11.9f, 11.9f);
            Vector3 initPos = new Vector3(x, y, z);

            // ��̖ڈȍ~�̃G�l�~�[�𐶐�����
            Instantiate(enemy, initPos, Quaternion.identity);

            // �f���^���Ԃ�����������
            delta = 0f;
        }
    }
}
