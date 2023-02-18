using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOptionsDirector : MonoBehaviour
{
    // �I�[�v�j���O�ֈړ����邩�ǂ����̃t���O�ϐ�
    private bool openingSwitch = false;
    public bool OpeningSwitch
    {
        get { return openingSwitch; }
        set { openingSwitch = value; }
    }

    private void Update()
    {
        if (openingSwitch)
        {
            /* �I�[�v�j���O�ֈړ����� */

            // �i�t���O���I�t�ɂ��Ă��珈�����s���j
            openingSwitch = false;
            StartCoroutine(ToOpening(0.3f));
        }
    }

    private IEnumerator ToOpening(float fWT)
    {
        // �ҋ@�����i0.3�b�j
        yield return new WaitForSeconds(fWT);

        // �I�[�v�j���O�V�[�������[�h����
        SceneManager.LoadScene("OpeningScene");
    }
}
