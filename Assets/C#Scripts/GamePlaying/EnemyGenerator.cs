using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

    private void Start()
    {
        // �G�l�~�[�𐶐����Ă����i2��ȏ�̑ҋ@����j
        StartCoroutine(EnemyGenerate(2.0f, 3.0f));
    }

    private IEnumerator EnemyGenerate(float fWT, float sWT)
    {
        // 1��ڂ̑ҋ@
        yield return new WaitForSeconds(fWT);

        // 1�̖ڂ̃G�l�~�[�𐶐�����
        Instantiate(enemy, new(5.0f, 15f, 5.0f), Quaternion.identity);

        while (true)
        {
            // 2+����ڂ̑ҋ@�i����0�ȏ�̐����j
            yield return new WaitForSeconds(sWT);

            //�@�G�l�~�[�̐����ʒu�������_���Ɍ��߂�
            float randX = Random.Range(-11f, 11f);
            float randY = Random.Range(15f, 26f);
            float randZ = Random.Range(-11f, 11f);
            Vector3 enemyPos = new(randX, randY, randZ);

            // �G�l�~�[�𐶐�����
            Instantiate(enemy, enemyPos, Quaternion.identity);
        }
    }
}
