using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndingDirector : MonoBehaviour
{
    private bool opening = false; // �I�[�v�j���O�֑J�ڂ��邩�ǂ���

    public bool Opening
    {
        get { return opening; }
        set { opening = value; }
    }

    void Update()
    {
        /* �I�[�v�j���O�֑J�ڂ���i1��̑ҋ@����j */
        if (opening)
        {
            opening = false;

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
