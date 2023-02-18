using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    // �G�l�~�[�i�v���n�u�j
    [SerializeField] private GameObject enemy;

    private void Start()
    {
        // �G�l�~�[�𐶐����Ă���
        StartCoroutine(EnemyGenerate(2.0f, 3.0f));
    }

    private IEnumerator EnemyGenerate(float fWT, float sWT)
    {
        // �ҋ@�����i2.0�b�j
        yield return new WaitForSeconds(fWT);

        // 1�̖ڂ̃G�l�~�[�𐶐�����
        Instantiate(enemy, new(5.0f, 15f, 5.0f), Quaternion.identity);

        while (true)
        {
            // �ҋ@�����i3.0�b�j
            yield return new WaitForSeconds(sWT);

            // �G�l�~�[�̐����ʒu�������_���Ɍ��߂�
            float randX = Random.Range(-11f, 11f);
            float randY = Random.Range(15f, 26f);
            float randZ = Random.Range(-11f, 11f);
            Vector3 enemyPos = new(randX, randY, randZ);

            // 2�̖ڈȍ~�̃G�l�~�[�𐶐�����
            Instantiate(enemy, enemyPos, Quaternion.identity);
        }
    }
}
