using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningDirector : MonoBehaviour
{
    [SerializeField] private GameObject platformUI;
    [SerializeField] private GameObject openingUI;
    
    private float animTime = 0f; // �A�j���[�V��������

    public float AnimTime
    {
        get { return animTime; }
    }

    private bool opening = false; // �I�[�v�j���O��ʂ֑J�ڂ��邩�ǂ���

    public bool Opening
    {
        get { return opening; }
        set { opening = value; }
    }

    private bool playing = false; // �Q�[���v���C�֑J�ڂ��邩�ǂ���

    public bool Playing
    {
        get { return playing; }
        set { playing = value; }
    }

    private bool options = false; // �Q�[���I�v�V�����֑J�ڂ��邩�ǂ���

    public bool Options
    {
        get { return options; }
        set { options = value; }
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
        // �����łȂ����
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
        if (opening)
        {
            opening = false;

            StartCoroutine(ToOpening(0.3f, 0.5f));
        }

        /* �Q�[���v���C�֑J�ڂ���i1��̑ҋ@����j */
        if (playing)
        {
            playing = false;

            StartCoroutine(ToPlaying(0.3f));
        }

        /* �Q�[���I�v�V�����֑J�ڂ���i1��̑ҋ@����j */
        if (options)
        {
            options = false;

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

    private IEnumerator ToPlaying(float fWT)
    {
        // 1��ڂ̑ҋ@
        yield return new WaitForSeconds(fWT);

        SceneManager.LoadScene("GamePlayingScene");
    }

    private IEnumerator ToOptions(float fWT)
    {
        // 1��ڂ̑ҋ@
        yield return new WaitForSeconds(fWT);

        SceneManager.LoadScene("GameOptionsScene");
    }
}
