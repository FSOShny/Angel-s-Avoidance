using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

    private void Start()
    {
        // �G�l�~�[�𐶐����Ă����i2��ȏ�̑ҋ@����j
        StartCoroutine(EnemyGenerate(2.0f, 4.0f));
    }

    private IEnumerator EnemyGenerate(float fWT, float sWT)
    {
        // 1��ڂ̑ҋ@
        yield return new WaitForSeconds(fWT);

        // 1�̖ڂ̃G�l�~�[�𐶐�����
        Instantiate(enemy, new Vector3(9f, 20f, 9f), Quaternion.identity);

        while (true)
        {
            // 2+����ڂ̑ҋ@�i����0�ȏ�̐����j
            yield return new WaitForSeconds(sWT);

            //�@�G�l�~�[�̐����ʒu�������_���Ɍ��߂�
            float randX = Random.Range(-11.9f, 11.9f);
            float randY = Random.Range(20f, 25f);
            float randZ = Random.Range(-11.9f, 11.9f);
            Vector3 initPos = new Vector3(randX, randY, randZ);

            // �G�l�~�[�𐶐�����
            Instantiate(enemy, initPos, Quaternion.identity);
        }
    }
}
