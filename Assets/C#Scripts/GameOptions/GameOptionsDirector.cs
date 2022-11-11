using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOptionsDirector : MonoBehaviour
{
    private bool openingSwitch = false; // �I�[�v�j���O�֑J�ڂ��邩�ǂ���

    public bool OpeningSwitch
    {
        get { return openingSwitch; }
        set { openingSwitch = value; }
    }

    private void Update()
    {
        /* �I�[�v�j���O�֑J�ڂ���i1��̑ҋ@����j */
        if (openingSwitch)
        {
            openingSwitch = false;

            StartCoroutine(ToOpening(0.3f));
        }
    }

    private IEnumerator ToOpening(float fWT)
    {
        // 1��ڂ̑ҋ@
        yield return new WaitForSeconds(fWT);

        SceneManager.LoadScene("OpeningScene");
    }
}
