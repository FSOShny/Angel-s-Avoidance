using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningDirector : MonoBehaviour
{
    [SerializeField] private GameObject platformUI;
    [SerializeField] private GameObject openingUI;

    // �A�j���[�V��������
    private float animTime = 0f;

    public float AnimTime
    {
        get { return animTime; }
    }

    // �I�[�v�j���O��ʂ֑J�ڂ��邩�ǂ���
    private bool openingSwitch = false;

    public bool OpeningSwitch
    {
        get { return openingSwitch; }
        set { openingSwitch = value; }
    }

    // �Q�[���`���[�g���A���֑J�ڂ��邩�ǂ���
    private bool tutorialsSwitch = false;

    public bool TutorialsSwitch
    {
        get { return tutorialsSwitch; }
        set { tutorialsSwitch = value; }
    }

    // �Q�[���v���C�֑J�ڂ��邩�ǂ���
    private bool playingSwitch = false;

    public bool PlayingSwitch
    {
        get { return playingSwitch; }
        set { playingSwitch = value; }
    }

    // �Q�[���I�v�V�����֑J�ڂ��邩�ǂ���
    private bool optionsSwitch = false;

    public bool OptionsSwitch
    {
        get { return optionsSwitch; }
        set { optionsSwitch = value; }
    }

    private void Start()
    {
        // �Q�[���̋N�����ł����
        if (StaticUnits.Startup)
        {
            StaticUnits.Startup = false;

            // �v���b�g�t�H�[����ʂ�L���ɂ���
            platformUI.SetActive(true);
            openingUI.SetActive(false);
        }
        // �Q�[���̋N�����łȂ����
        else
        {
            // �I�[�v�j���O��ʂ�L���ɂ���
            platformUI.SetActive(false);
            openingUI.SetActive(true);

            // �A�j���[�V�������Ԃ�ݒ肷��
            animTime = 3.0f;
        }
    }

    private void Update()
    {
        // �A�j���[�V�������Ԓ���
        if (animTime > 0f)
        {
            // ���Ԃ��o�߂�����
            animTime -= Time.deltaTime;
        }
        else if (animTime < 0f) // �A�j���[�V�������Ԍ��
        {
            // ���Ԃ�����������i����ȏ����̂��߁j
            animTime = 0f;
        }

        /* �I�[�v�j���O��ʂ֑J�ڂ���i2��̑ҋ@����j */
        if (openingSwitch)
        {
            openingSwitch = false;

            StartCoroutine(ToOpening(0.3f, 0.5f));
        }

        /* �Q�[���`���[�g���A���֑J�ڂ���i1��̑ҋ@����j */
        if (tutorialsSwitch)
        {
            tutorialsSwitch = false;

            StartCoroutine(ToTutorials(0.3f));
        }

        /* �Q�[���v���C�֑J�ڂ���i1��̑ҋ@����j */
        if (playingSwitch)
        {
            playingSwitch = false;

            StartCoroutine(ToPlaying(0.3f));
        }

        /* �Q�[���I�v�V�����֑J�ڂ���i1��̑ҋ@����j */
        if (optionsSwitch)
        {
            optionsSwitch = false;

            StartCoroutine(ToOptions(0.3f));
        }
    }

    private IEnumerator ToOpening(float fWT, float sWT)
    {
        // 1��ڂ̑ҋ@
        yield return new WaitForSeconds(fWT);

        // �v���b�g�t�H�[����ʂ𖳌��ɂ���
        platformUI.SetActive(false);

        // 2��ڂ̑ҋ@
        yield return new WaitForSeconds(sWT);

        // �I�[�v�j���O��ʂ�L���ɂ���
        openingUI.SetActive(true);

        // �A�j���[�V�������Ԃ�ݒ肷��
        animTime = 3.0f;
    }

    private IEnumerator ToTutorials(float fWT)
    {
        // 1��ڂ̑ҋ@
        yield return new WaitForSeconds(fWT);

        SceneManager.LoadScene("GameTutorialsScene");
    }

    private IEnumerator ToPlaying(float fWT)
    {
        // 1��ڂ̑ҋ@
        yield return new WaitForSeconds(fWT);

        // �Q�[���̈ꎞ��~�����s����i���m�ȃQ�[���v���C�̎��Ԍv�����s�����߂̏����j
        Time.timeScale = 0f;

        SceneManager.LoadScene("GamePlayingScene");
    }

    private IEnumerator ToOptions(float fWT)
    {
        // 1��ڂ̑ҋ@
        yield return new WaitForSeconds(fWT);

        SceneManager.LoadScene("GameOptionsScene");
    }
}
