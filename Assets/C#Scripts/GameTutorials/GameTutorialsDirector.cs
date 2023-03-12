using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTutorialsDirector : MonoBehaviour
{
    // UI�I�u�W�F�N�g
    [SerializeField] private GameObject pauseUi;
    [SerializeField] private List<GameObject> tutorialUis;

    // �ȑO�̃y�[�W��
    private int pastPage;

    // ���݂̃y�[�W��
    private int nowPage = 0;
    public int NowPage
    {
        get { return nowPage; }
        set { nowPage = value; }
    }

    // �|�[�Y��ʂ�L���ɂ��邩�ǂ����̃t���O�ϐ�
    private bool pauseSwitch = false;
    public bool PauseSwitch
    {
        get { return pauseSwitch; }
        set { pauseSwitch = value; }
    }

    // �|�[�Y��ʂ𖳌��ɂ��邩�ǂ����̃t���O�ϐ�
    private bool continueSwitch = false;
    public bool ContinueSwitch
    {
        get { return continueSwitch; }
        set { continueSwitch = value; }
    }

    // �I�[�v�j���O�ֈړ����邩�ǂ����̃t���O�ϐ�
    private bool openingSwitch = false;
    public bool OpeningSwitch
    {
        get { return openingSwitch; }
        set { openingSwitch = value; }
    }

    // �ݒ肵���y�[�W�ֈړ����邩�ǂ����̃t���O�ϐ�
    private bool pageSwitch = false;
    public bool PageSwitch
    {
        get { return pageSwitch; }
        set { pageSwitch = value; }
    }

    private void Start()
    {
        // �`���[�g���A����ʁi�y�[�W1�j��L���ɂ���
        tutorialUis[0].SetActive(true);

        // �`���[�g���A����ʁi�y�[�W1�ȊO�j�A�|�[�Y��ʂ𖳌��ɂ���
        for (int i = 1; i < tutorialUis.Count; i++)
        {
            tutorialUis[i].SetActive(false);
        }
        pauseUi.SetActive(false);

        // �ȑO�̃y�[�W����ݒ肷��
        pastPage = nowPage;
    }

    private void Update()
    {
        if (pauseSwitch)
        {
            /* �|�[�Y��ʂ�L���ɂ��� */

            // �i�t���O���I�t�ɂ��Ă��珈�����s���j
            pauseSwitch = false;
            pauseUi.SetActive(true);
        }

        if (continueSwitch)
        {
            /* �|�[�Y��ʂ𖳌��ɂ��� */

            // �i�t���O���I�t�ɂ��Ă��珈�����s���j
            continueSwitch = false;
            pauseUi.SetActive(false);
        }

        if (openingSwitch)
        {
            /* �I�[�v�j���O�ֈړ����� */

            // �i�t���O���I�t�ɂ��Ă��珈�����s���j
            openingSwitch = false;
            StartCoroutine(ToOpening(0.5f));
        }

        if (pageSwitch)
        {
            /* �ݒ肵���y�[�W�ֈړ�����ֈړ����� */

            // �i�t���O���I�t�ɂ��Ă��珈�����s���j
            pageSwitch = false;
            tutorialUis[pastPage].SetActive(false);
            tutorialUis[nowPage].SetActive(true);

            // �ȑO�̃y�[�W����ݒ肷��
            pastPage = nowPage;
        }
    }

    private IEnumerator ToOpening(float fWT)
    {
        // �ҋ@�����i0.5�b�j
        yield return new WaitForSeconds(fWT);

        // �I�[�v�j���O�V�[�������[�h����
        SceneManager.LoadScene("OpeningScene");
    }
}
