using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTutorialsDirector : MonoBehaviour
{
    [SerializeField] private GameObject pauseUi;
    [SerializeField] private List<GameObject> tutorialUis;
    private int pastPage; // �ߋ��̃y�[�W��

    // ���݂̃y�[�W��
    private int nowPage = 0;

    public int NowPage
    {
        get { return nowPage; }
        set { nowPage = value; }
    }

    // �|�[�Y��ʂ֑J�ڂ��邩�ǂ���
    private bool pauseSwitch = false;

    public bool PauseSwitch
    {
        get { return pauseSwitch; }
        set { pauseSwitch = value; }
    }

    // �Q�[���`���[�g���A���𑱍s���邩�ǂ���
    private bool continueSwitch = false;

    public bool ContinueSwitch
    {
        get { return continueSwitch; }
        set { continueSwitch = value; }
    }

    // �I�[�v�j���O�֑J�ڂ��邩�ǂ���
    private bool openingSwitch = false;

    public bool OpeningSwitch
    {
        get { return openingSwitch; }
        set { openingSwitch = value; }
    }

    // ����̃y�[�W�֑J�ڂ��邩�ǂ���
    private bool pageSwitch = false;

    public bool PageSwitch
    {
        get { return pageSwitch; }
        set { pageSwitch = value; }
    }

    private void Start()
    {
        // 1�y�[�W�ڂ̃`���[�g���A����ʂ�L���ɂ���
        tutorialUis[0].SetActive(true);

        /* 1�y�[�W�ڈȊO�̃`���[�g���A����ʂ𖳌��ɂ��� */
        for (int i = 1; i < tutorialUis.Count; i++)
        {
            tutorialUis[i].SetActive(false);
        }

        // �|�[�Y��ʂ𖳌��ɂ���
        pauseUi.SetActive(false);

        // �ߋ��̃y�[�W����ݒ肷��
        pastPage = nowPage;
    }

    private void Update()
    {
        /* �|�[�Y��ʂ֑J�ڂ��� */
        if (pauseSwitch)
        {
            pauseSwitch = false;

            // �|�[�Y��ʂ�L���ɂ���
            pauseUi.SetActive(true);
        }

        /* �Q�[���`���[�g���A���𑱍s���� */
        if (continueSwitch)
        {
            continueSwitch = false;

            // �|�[�Y��ʂ𖳌��ɂ���
            pauseUi.SetActive(false);
        }

        /* �I�[�v�j���O�֑J�ڂ���i1��̑ҋ@����j */
        if (openingSwitch)
        {
            openingSwitch = false;

            StartCoroutine(ToOpening(0.3f));
        }

        /* ����̃y�[�W�֑J�ڂ��� */
        if (pageSwitch)
        {
            pageSwitch = false;

            tutorialUis[pastPage].SetActive(false);
            tutorialUis[nowPage].SetActive(true);

            // �ߋ��̃y�[�W����ݒ肷��
            pastPage = nowPage;
        }
    }

    private IEnumerator ToOpening(float fWT)
    {
        // 1��ڂ̑ҋ@
        yield return new WaitForSeconds(fWT);

        SceneManager.LoadScene("OpeningScene");
    }
}
