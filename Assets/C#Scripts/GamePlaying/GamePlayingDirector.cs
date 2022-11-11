using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GamePlayingDirector : MonoBehaviour
{
    public float gameTimeLim = 45f; // �Q�[���̐�������

    [SerializeField] private GameObject startUI;
    [SerializeField] private GameObject smartPhoneUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject platformUI;
    [SerializeField] private GameObject loserUI;
    [SerializeField] private GameObject winnerUI;

    private Image lifeBar;
    private Image upDown;
    private Image forwardBack;
    private TextMeshProUGUI timerText;
    private GameEndingDirector nextDirector;
    private float nowTimeLim; // ���݂̐�������
    private int iTimeLim; // ������������������

    private int nowPlayerLife; // ���݂̃v���C���[�̗̑�

    public int NowPlayerLife
    {
        get { return nowPlayerLife; }
        set { nowPlayerLife = value; }
    }

    private bool modeChange = false; // �ړ����[�h���u�㉺�v���ǂ���

    public bool ModeChange
    {
        get { return modeChange; }
        set { modeChange = value; }
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

    private bool openingSwitch = false; // �I�[�v�j���O�֑J�ڂ��邩�ǂ���

    public bool OpeningSwitch
    {
        get { return openingSwitch; }
        set { openingSwitch = value; }
    }

    private bool canUseInterf = false; // �C���^�t�F�[�X���g�����Ԃ��ǂ���

    public bool CanUseInterf
    {
        get { return canUseInterf; }
    }

    private bool canUseButton = false; // �{�^�����g�����Ԃ��ǂ���

    public bool CanUseButton
    {
        get { return canUseButton; }
    }

    private void Start()
    {
        // �̗̓o�[�̃C���[�W�R���|�[�l���g���擾����
        lifeBar = GameObject.Find("5Life Bar").GetComponent<Image>();

        // �㉺���[�h�摜�̃C���[�W�R���|�[�l���g���擾����
        upDown = GameObject.Find("UpDown Mode Image").GetComponent<Image>();

        // �O�ヂ�[�h�摜�̃C���[�W�R���|�[�l���g���擾����
        forwardBack = GameObject.Find("ForwardBack Mode Image").GetComponent<Image>();

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

        // �㉺���[�h�摜�𖳌��ɂ���
        upDown.enabled = false;

        // ���݂̐������Ԃ��i�[����
        nowTimeLim = gameTimeLim;

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

        // �C���^�t�F�[�X���g�����Ԃ�
        if (canUseInterf)
        {
            // Esc�L�[����͂����
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

            // �C���^�t�F�[�X�g�p�\��Ԃ𖳌��ɂ���
            canUseInterf = false;

            // �{�^���g�p�\��Ԃ𖳌��ɂ���
            canUseButton = false;

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
        if (openingSwitch)
        {
            openingSwitch = false;

            // �C���^�t�F�[�X�g�p�\��Ԃ𖳌��ɂ���
            canUseInterf = false;

            // �{�^���g�p�\��Ԃ𖳌��ɂ���
            canUseButton = false;

            StartCoroutine(ToOpening(0.3f));
        }

        // �̗̓o�[���X�V����
        lifeBar.fillAmount = (float)nowPlayerLife / StaticUnits.MaxPlayerLife;

        // �̗͂��[���ɂȂ��
        if (nowPlayerLife == 0)
        {
            // �C���^�t�F�[�X�g�p�\��Ԃ𖳌��ɂ���
            canUseInterf = false;

            // �{�^���g�p�\��Ԃ𖳌��ɂ���
            canUseButton = false;

            // �s�k�҂ɂȂ�i1��̑ҋ@����j
            StartCoroutine(Loser(2.5f));
        }

        // �ړ����[�h���u�㉺�v�ł����
        if (modeChange)
        {
            // �O�ヂ�[�h�摜�𖳌��ɂ���
            forwardBack.enabled = false;

            // �㉺���[�h�摜��L���ɂ���
            upDown.enabled = true;
        }
        // �ړ����[�h���u�O��v�ł����
        else
        {
            // �O�ヂ�[�h�摜��L���ɂ���
            forwardBack.enabled = true;

            // �㉺���[�h�摜�𖳌��ɂ���
            upDown.enabled = false;
        }

        // ���݂̐������Ԃ��o�߂�����
        nowTimeLim -= Time.deltaTime;

        // �������Ԃ𐮐�������
        iTimeLim = (int)nowTimeLim;

        // �^�C�}�[���X�V����
        timerText.text = iTimeLim.ToString();

        // �������Ԃ��[���ɂȂ��
        if (nowTimeLim < 0f)
        {
            // ���Ԃ�����������i����ȏ����̂��߁j
            nowTimeLim = 0f;

            // �C���^�t�F�[�X�g�p�\��Ԃ𖳌��ɂ���
            canUseInterf = false;

            // �{�^���g�p�\��Ԃ𖳌��ɂ���
            canUseButton = false;

            // �����҂ɂȂ�i1��̑ҋ@����j
            StartCoroutine(Winner(5.5f));
        }
    }

    private IEnumerator GameStart(float fWT)
    {
        // 1��ڂ̑ҋ@
        yield return new WaitForSecondsRealtime(fWT);

        // �C���^�t�F�[�X�g�p�\��Ԃ�L���ɂ���
        canUseInterf = true;

        // �{�^���g�p�\��Ԃ�L���ɂ���
        canUseButton = true;

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

        // ���̃V�[���ւ̕ϐ������n�����J�n����
        SceneManager.sceneLoaded += GameEndingLoaded;

        // �Q�[���̈ꎞ��~����������
        Time.timeScale = 1.0f;

        // �Q�[���G���f�B���O�֑J�ڂ���
        SceneManager.LoadScene("GameEndingScene");
    }

    private void GameEndingLoaded(Scene scene, LoadSceneMode mode)
    {
        // �Q�[���G���f�B���O�f�B���N�^�[���擾����
        nextDirector = GameObject.Find("Game Ending Director").GetComponent<GameEndingDirector>();

        // ��e�񐔂��i�[����
        nextDirector.Attacked = StaticUnits.MaxPlayerLife - nowPlayerLife;

        // ���̃V�[���ւ̕ϐ������n�����I������
        SceneManager.sceneLoaded -= GameEndingLoaded;
    }
}
