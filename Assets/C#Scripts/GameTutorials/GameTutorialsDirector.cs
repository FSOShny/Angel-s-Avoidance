using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTutorialsDirector : MonoBehaviour
{
    [SerializeField] private List<GameObject> tutorialUIs;
    private int nowPage = 0; // ���݂̃y�[�W��

    // �I�[�v�j���O�֑J�ڂ��邩�ǂ���
    private bool openingSwitch = false;

    public bool OpeningSwitch
    {
        get { return openingSwitch; }
        set { openingSwitch = value; }
    }

    // ���̃y�[�W�֑J�ڂ��邩�ǂ���
    private bool nextSwitch = false;

    public bool NextSwitch
    {
        get { return nextSwitch; }
        set { nextSwitch = value; }
    }

    // �O�̃y�[�W�֑J�ڂ��邩�ǂ���
    private bool prevSwitch = false;

    public bool PrevSwitch
    {
        get { return prevSwitch; }
        set { prevSwitch = value; }
    }

    private void Start()
    {
        // 1�y�[�W�ڂ̃`���[�g���A����ʂ�L���ɂ���
        tutorialUIs[0].SetActive(true);

        /* 1�y�[�W�ڈȊO�̃`���[�g���A����ʂ𖳌��ɂ��� */
        for (int i = 1; i < tutorialUIs.Count; i++)
        {
            tutorialUIs[i].SetActive(false);
        }
    }

    private void Update()
    {
        /* �I�[�v�j���O�֑J�ڂ���i1��̑ҋ@����j */
        if (openingSwitch)
        {
            openingSwitch = false;

            StartCoroutine(ToOpening(0.3f));
        }

        /* ���̃y�[�W�֑J�ڂ��� */
        if (nextSwitch)
        {
            nextSwitch = false;

            // ���݂̃y�[�W���𑝂₷
            nowPage++;

            tutorialUIs[nowPage - 1].SetActive(false);
            tutorialUIs[nowPage].SetActive(true);
        }

        /* �O�̃y�[�W�֑J�ڂ��� */
        if (prevSwitch)
        {
            prevSwitch = false;

            // ���݂̃y�[�W�������炷
            nowPage--;

            tutorialUIs[nowPage + 1].SetActive(false);
            tutorialUIs[nowPage].SetActive(true);
        }
    }

    private IEnumerator ToOpening(float fWT)
    {
        // 1��ڂ̑ҋ@
        yield return new WaitForSeconds(fWT);

        SceneManager.LoadScene("OpeningScene");
    }
}
