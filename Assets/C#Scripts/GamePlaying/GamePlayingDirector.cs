using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GamePlayingDirector : MonoBehaviour
{
    [SerializeField] private GameObject startUI;
    [SerializeField] private GameObject smartPhoneUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject platformUI;
    [SerializeField] private GameObject loserUI;
    [SerializeField] private GameObject winnerUI;

    private Image lifeBar;
    private TextMeshProUGUI timerText;
    private float nowTimeLim; // ���݂̐�������
    private int iTimeLim; // ������������������

    private float nowPlayerLife; // ���݂̃v���C���[�̗̑�

    public float NowPlayerLife
    {
        get { return nowPlayerLife; }
        set { nowPlayerLife = value; }
    }

    private bool pauseSwitch = false; // �|�[�Y��ʂ֑J�ڂ��邩�ǂ���

    public bool PauseSwitch
    {
        get { return pauseSwitch; }
        set { pauseSwitch = value; }
    }

    private bool continueSwitch = false; // �Q�[���v���C�𑱍s���邩�ǂ���

    public bool ContinueSwitch
    {
        get { return continueSwitch; }
        set { continueSwitch = value; }
    }

    private bool restartSwitch = false; // �Q�[���v���C���ĊJ�n���邩�ǂ���

    public bool RestartSwitch
    {
        get { return restartSwitch; }
        set { restartSwitch = value; }
    }

    private bool platformSwitch = false; // �v���b�g�t�H�[����ʂ֑J�ڂ��邩�ǂ���

    public bool PlatformSwitch
    {
        get { return platformSwitch; }
        set { platformSwitch = value; }
    }

    private bool opening = false; // �I�[�v�j���O�֑J�ڂ��邩�ǂ���

    public bool Opening
    {
        get { return opening; }
        set { opening = value; }
    }

    private bool canUseButton = false; // �{�^�����g�����Ԃ��ǂ���

    public bool CanUseButton
    {
        get { return canUseButton; }
    }

    private bool canUseInterf = false; // �C���^�t�F�[�X���g�����Ԃ��ǂ���

    public bool CanUseInterf
    {
        get { return canUseInterf; }
    }

    private void Start()
    {
        // �̗̓o�[�̃C���[�W�R���|�[�l���g���擾����
        lifeBar = GameObject.Find("5Life Bar").GetComponent<Image>();

        // �^�C�}�[�̃e�L�X�g�R���|�[�l���g���擾����
        timerText = GameObject.Find("Timer Text").GetComponent<TextMeshProUGUI>();

        // �X�^�[�g��ʂ�L���ɂ���
        startUI.SetActive(true);
        smartPhoneUI.SetActive(false);
        pauseUI.SetActive(false);
        platformUI.SetActive(false);
        loserUI.SetActive(false);
        winnerUI.SetActive(false);

        // ���݂̃v���C���[�̗̑͂��i�[����
        nowPlayerLife = StaticUnits.MaxPlayerLife;

        // ���݂̐������Ԃ��i�[����
        nowTimeLim = StaticUnits.GameTimeLim;

        /* �Q�[���v���C���J�n����i1��̑ҋ@����j */
        StartCoroutine(GameStart(3.0f));
    }

    private void Update()
    {
        // ���݂̃v���b�g�t�H�[�����X�}�z�ł����
        if (StaticUnits.SmartPhone)
        {
            // �X�}�zUI�\����L���ɂ���
            smartPhoneUI.SetActive(true);
        }
        // ���݂̃v���b�g�t�H�[�����p�\�R���ł����
        else
        {
            // �X�}�zUI�\���𖳌��ɂ���
            smartPhoneUI.SetActive(false);
        }

        // �C���^�t�F�[�X���g�����Ԃł���Esc�L�[���������ꍇ��
        if (canUseInterf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // �|�[�Y��ʂւ̑J�ڔ����L���ɂ���
                pauseSwitch = true;
            }
        }

        /* �|�[�Y��ʂ֑J�ڂ��� */
        if (pauseSwitch)
        {
            pauseSwitch = false;

            // �Q�[���̈ꎞ��~�����s����
            Time.timeScale = 0f;

            // �C���^�t�F�[�X�g�p�\��Ԃ𖳌��ɂ���
            canUseInterf = false;

            // �|�[�Y��ʂ�L���ɂ���
            pauseUI.SetActive(true);
        }
        
        /* �Q�[���v���C�𑱍s���� */
        if (continueSwitch)
        {
            continueSwitch = false;

            // �|�[�Y��ʂ𖳌��ɂ���
            pauseUI.SetActive(false);

            // �C���^�t�F�[�X�g�p�\��Ԃ�L���ɂ���
            canUseInterf = true;

            // �Q�[���̈ꎞ��~����������
            Time.timeScale = 1.0f;
        }

        /* �Q�[���v���C���ĊJ�n����i1��̑ҋ@����j */
        if (restartSwitch)
        {
            restartSwitch = false;

            // �{�^���g�p�\��Ԃ𖳌��ɂ���
            canUseButton = false;

            // �C���^�t�F�[�X�g�p�\��Ԃ𖳌��ɂ���
            canUseInterf = false;

            StartCoroutine(GameRestart(0.3f));
        }

        /* �v���b�g�t�H�[����ʂ֑J�ڂ��� */
        if (platformSwitch)
        {
            // �v���b�g�t�H�[����ʂ�L���ɂ���
            platformUI.SetActive(true);
        }
        else
        {
            // �v���b�g�t�H�[����ʂ𖳌��ɂ���
            platformUI.SetActive(false);
        }

        /* �I�[�v�j���O�֑J�ڂ���i1��̑ҋ@����j */
        if (opening)
        {
            opening = false;

            // �{�^���g�p�\��Ԃ𖳌��ɂ���
            canUseButton = false;

            // �C���^�t�F�[�X�g�p�\��Ԃ𖳌��ɂ���
            canUseInterf = false;

            StartCoroutine(ToOpening(0.3f));
        }

        // �̗̓o�[�̕\�����X�V����
        lifeBar.fillAmount = nowPlayerLife / StaticUnits.MaxPlayerLife;

        // �̗͂��[���ɂȂ��
        if (nowPlayerLife == 0f)
        {
            // �{�^���g�p�\��Ԃ𖳌��ɂ���
            canUseButton = false;

            // �C���^�t�F�[�X�g�p�\��Ԃ𖳌��ɂ���
            canUseInterf = false;

            // �s�k�҂ɂȂ�i1��̑ҋ@����j
            StartCoroutine(Loser(2.5f));
        }

        // ���݂̐������Ԃ��o�߂�����
        nowTimeLim -= Time.deltaTime;

        // �������Ԃ𐮐�������
        iTimeLim = (int)nowTimeLim;

        // �^�C�}�[�̕\�����X�V����
        timerText.text = iTimeLim.ToString();

        // �������Ԃ��[���ɂȂ��
        if (nowTimeLim < 0f)
        {
            // ���Ԃ�����������i����ȏ����̂��߁j
            nowTimeLim = 0f;

            // �{�^���g�p�\��Ԃ𖳌��ɂ���
            canUseButton = false;

            // �C���^�t�F�[�X�g�p�\��Ԃ𖳌��ɂ���
            canUseInterf = false;

            // �����҂ɂȂ�i1��̑ҋ@����j
            StartCoroutine(Winner(5.5f));
        }
    }

    private IEnumerator GameStart(float fWT)
    {
        // 1��ڂ̑ҋ@
        yield return new WaitForSecondsRealtime(fWT);

        // �{�^���g�p�\��Ԃ�L���ɂ���
        canUseButton = true;

        // �C���^�t�F�[�X�g�p�\��Ԃ�L���ɂ���
        canUseInterf = true;

        // �X�^�[�g��ʂ𖳌��ɂ���
        startUI.SetActive(false);

        // �Q�[���̈ꎞ��~����������
        Time.timeScale= 1.0f;
    }

    private IEnumerator GameRestart(float fWT)
    {
        // 1��ڂ̑ҋ@
        yield return new WaitForSecondsRealtime(fWT);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator ToOpening(float fWT)
    {
        // 1��ڂ̑ҋ@
        yield return new WaitForSecondsRealtime(fWT);

        // �Q�[���̈ꎞ��~����������
        Time.timeScale = 1.0f;

        SceneManager.LoadScene("OpeningScene");
    }

    private IEnumerator Loser(float fWT)
    {
        // �s�k�҉�ʂ�L���ɂ���
        loserUI.SetActive(true);

        // �Q�[���̈ꎞ��~�����s����
        Time.timeScale = 0f;

        // 1��ڂ̑ҋ@
        yield return new WaitForSecondsRealtime(fWT);

        // �{�^���g�p�\��Ԃ�L���ɂ���
        canUseButton = true;
    }

    private IEnumerator Winner(float fWT)
    {
        // �����҉�ʂ�L���ɂ���
        winnerUI.SetActive(true);

        // �Q�[���̈ꎞ��~�����s����
        Time.timeScale = 0f;

        // 1��ڂ̑ҋ@
        yield return new WaitForSecondsRealtime(fWT);

        // �Q�[���̈ꎞ��~����������
        Time.timeScale = 1.0f;

        // �Q�[���G���f�B���O�֑J�ڂ���
        SceneManager.LoadScene("GameEndingScene");
    }
}
