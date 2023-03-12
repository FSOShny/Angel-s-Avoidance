using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningDirector : MonoBehaviour
{
    // UI�I�u�W�F�N�g
    [SerializeField] private GameObject platformUi;
    [SerializeField] private GameObject openingUi;

    // �A�j���[�V��������
    private float animTime = 0f;
    public float AnimTime
    {
        get { return animTime; }
    }

    // �I�[�v�j���O�ֈړ����邩�ǂ����̃t���O�ϐ�
    private bool openingSwitch = false;
    public bool OpeningSwitch
    {
        get { return openingSwitch; }
        set { openingSwitch = value; }
    }

    // �Q�[���`���[�g���A���ֈړ����邩�ǂ����̃t���O�ϐ�
    private bool tutorialsSwitch = false;
    public bool TutorialsSwitch
    {
        get { return tutorialsSwitch; }
        set { tutorialsSwitch = value; }
    }

    // �Q�[���v���C�ֈړ����邩�ǂ����̃t���O�ϐ�
    private bool playingSwitch = false;
    public bool PlayingSwitch
    {
        get { return playingSwitch; }
        set { playingSwitch = value; }
    }

    // �Q�[���I�v�V�����ֈړ����邩�ǂ����̃t���O�ϐ�
    private bool optionsSwitch = false;
    public bool OptionsSwitch
    {
        get { return optionsSwitch; }
        set { optionsSwitch = value; }
    }

    private void Start()
    {
        if (StaticUnits.Startup)
        {
            /* �Q�[���̋N������ł����
               �v���b�g�t�H�[����ʂ�L���ɂ��� */

            // �i�t���O���I�t�ɂ��Ă��珈�����s���j
            StaticUnits.Startup = false;
            platformUi.SetActive(true);
            openingUi.SetActive(false);
        }
        else
        {
            /* �Q�[���̋N������ł����
               �I�[�v�j���O��ʂ�L���ɂ��� */

            platformUi.SetActive(false);
            openingUi.SetActive(true);

            // �A�j���[�V�������Ԃ�ݒ肷��
            animTime = 3.0f;
        }
    }

    private void Update()
    {
        if (animTime > 0f)
        {
            /* �A�j���[�V�����̂Ƃ���
               ���̎��Ԃ��o�߂�����   */

            animTime -= Time.deltaTime;
        }
        else if (animTime < 0f)
        {
            /* �A�j���[�V�������I�������Ƃ���
               ���Ԃ�����������i���ʂȏ������Ȃ����߁j */

            animTime = 0f;
        }

        if (openingSwitch)
        {
            /* �I�[�v�j���O��ʂ�L���ɂ��� */

            // �i�t���O���I�t�ɂ��Ă��珈�����s���j
            openingSwitch = false;
            StartCoroutine(ToOpening(0.3f, 0.5f));
        }

        if (tutorialsSwitch)
        {
            /* �Q�[���`���[�g���A���ֈړ����� */

            // �i�t���O���I�t�ɂ��Ă��珈�����s���j
            tutorialsSwitch = false;
            StartCoroutine(ToTutorials(0.5f));
        }

        if (playingSwitch)
        {
            /* �Q�[���v���C�ֈړ����� */

            // �i�t���O���I�t�ɂ��Ă��珈�����s���j
            playingSwitch = false;
            StartCoroutine(ToPlaying(1.5f));
        }

        if (optionsSwitch)
        {
            /* �Q�[���I�v�V�����ֈړ����� */

            // �i�t���O���I�t�ɂ��Ă��珈�����s���j
            optionsSwitch = false;
            StartCoroutine(ToOptions(0.5f));
        }
    }

    private IEnumerator ToOpening(float fWT, float sWT)
    {
        // �ҋ@�����i0.3�b�j
        yield return new WaitForSeconds(fWT);

        // �v���b�g�t�H�[����ʂ𖳌��ɂ���
        platformUi.SetActive(false);

        // �ҋ@�����i0.5�b�j
        yield return new WaitForSeconds(sWT);

        // �I�[�v�j���O��ʂ�L���ɂ�
        // �A�j���[�V�������Ԃ�ݒ肷��
        openingUi.SetActive(true);
        animTime = 3.0f;
    }

    private IEnumerator ToTutorials(float fWT)
    {
        // �ҋ@�����i0.5�b�j
        yield return new WaitForSeconds(fWT);

        // �Q�[���`���[�g���A���V�[�������[�h����
        SceneManager.LoadScene("GameTutorialsScene");
    }

    private IEnumerator ToPlaying(float fWT)
    {
        // �ҋ@�����i1.5�b�j
        yield return new WaitForSeconds(fWT);

        // �Q�[���̈ꎞ��~�����s����
        // �i���m�ȃQ�[���v���C�̎��Ԍv�����s�����߁j
        Time.timeScale = 0f;

        // �Q�[���v���C�V�[�������[�h����
        SceneManager.LoadScene("GamePlayingScene");
    }

    private IEnumerator ToOptions(float fWT)
    {
        // �ҋ@�����i0.5�b�j
        yield return new WaitForSeconds(fWT);

        // �Q�[���I�v�V�����V�[�������[�h����
        SceneManager.LoadScene("GameOptionsScene");
    }
}
