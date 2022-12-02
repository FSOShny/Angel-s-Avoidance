using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GamePlayingDirector : MonoBehaviour
{
    [SerializeField] private GameObject startUi;
    [SerializeField] private GameObject smartPhoneUi;
    [SerializeField] private GameObject pauseUi;
    [SerializeField] private GameObject platformUi;
    [SerializeField] private GameObject loserUi;
    [SerializeField] private GameObject winnerUi;
    [SerializeField] private Sprite energy;
    [SerializeField] private Sprite fatigue;

    private Image noLivesBar;
    private Image livesBar;
    private Image energyBar;
    private Image upDown;
    private Image forwardBack;
    private TextMeshProUGUI timerText;
    private GameEndingDirector nextDirector;
    private float nowTimeLim; // ���݂̐�������
    private int iTimeLim; // ������������������

    // �C���^�t�F�[�X���g�����Ԃ��ǂ���
    private bool canUseInterf = false;

    public bool CanUseInterf
    {
        get { return canUseInterf; }
    }

    // �{�^�����g�����Ԃ��ǂ���
    private bool canUseButton = false;

    public bool CanUseButton
    {
        get { return canUseButton; }
    }

    // ���݂̃v���C���[�̗̑�
    private int nowPlayerLives;

    public int NowPlayerLives
    {
        get { return nowPlayerLives; }
        set { nowPlayerLives = value; }
    }

    // �|�[�Y��ʂ֑J�ڂ��邩�ǂ���
    private bool pauseSwitch = false;

    public bool PauseSwitch
    {
        get { return pauseSwitch; }
        set { pauseSwitch = value; }
    }

    // �Q�[���v���C�𑱍s���邩�ǂ���
    private bool continueSwitch = false;

    public bool ContinueSwitch
    {
        get { return continueSwitch; }
        set { continueSwitch = value; }
    }

    // �Q�[���v���C���ĊJ�n���邩�ǂ���
    private bool restartSwitch = false;

    public bool RestartSwitch
    {
        get { return restartSwitch; }
        set { restartSwitch = value; }
    }

    // �v���b�g�t�H�[����ʂ֑J�ڂ��邩�ǂ���
    private bool platformSwitch = false;

    public bool PlatformSwitch
    {
        get { return platformSwitch; }
        set { platformSwitch = value; }
    }

    // �I�[�v�j���O�֑J�ڂ��邩�ǂ���
    private bool openingSwitch = false;

    public bool OpeningSwitch
    {
        get { return openingSwitch; }
        set { openingSwitch = value; }
    }

    // �ړ����[�h���u�㉺�v���ǂ���
    private bool modeChange = false;

    public bool ModeChange
    {
        get { return modeChange; }
        set { modeChange = value; }
    }

    // ��J��Ԃ��ǂ���
    private bool fatigueSwitch = false;

    public bool FatigueSwitch
    {
        get { return fatigueSwitch; }
        set { fatigueSwitch = value; }
    }

    // �C�͉񕜎���
    private float chargeTime = 0f;

    public float ChargeTime
    {
        get { return chargeTime; }
        set { chargeTime = value; }
    }

    // ��J��ԉ�
    private int fatigueCnt = 0;

    public int FatigueCnt
    {
        get { return fatigueCnt; }
        set { fatigueCnt = value; }
    }

    private void Start()
    {
        // �̗̓o�[�i���F�j�̃C���[�W�R���|�[�l���g���擾����
        noLivesBar = GameObject.Find("No Lives Bar").GetComponent<Image>();

        // �̗̓o�[�i�ΐF�j�̃C���[�W�R���|�[�l���g���擾����
        livesBar = GameObject.Find("Lives Bar").GetComponent<Image>();

        // �C�̓o�[�̃C���[�W�R���|�[�l���g���擾����
        energyBar = GameObject.Find("Energy Bar").GetComponent<Image>();

        // �㉺���[�h�摜�̃C���[�W�R���|�[�l���g���擾����
        upDown = GameObject.Find("UpDown Mode Image").GetComponent<Image>();

        // �O�ヂ�[�h�摜�̃C���[�W�R���|�[�l���g���擾����
        forwardBack = GameObject.Find("ForwardBack Mode Image").GetComponent<Image>();

        // �^�C�}�[�̃e�L�X�g�R���|�[�l���g���擾����
        timerText = GameObject.Find("Timer Text").GetComponent<TextMeshProUGUI>();

        // �X�^�[�g��ʂ�L���ɂ���
        startUi.SetActive(true);
        smartPhoneUi.SetActive(false);
        pauseUi.SetActive(false);
        platformUi.SetActive(false);
        loserUi.SetActive(false);
        winnerUi.SetActive(false);

        // ���݂̃v���C���[�̗̑͂�ݒ肷��
        nowPlayerLives = StaticUnits.MaxPlayerLives;

        // �㉺���[�h�摜�𖳌��ɂ���
        upDown.enabled = false;

        // ���݂̐������Ԃ�ݒ肷��
        nowTimeLim = StaticUnits.GameTimeLim;

        // �Q�[���v���C���J�n����i1��̑ҋ@����j
        StartCoroutine(GameStart(3.0f));
    }

    private void Update()
    {
        // ���݂̃v���b�g�t�H�[�����X�}�z�ł����
        if (StaticUnits.SmartPhone)
        {
            // �X�}�zUI�\����L���ɂ���
            smartPhoneUi.SetActive(true);
        }
        // ���݂̃v���b�g�t�H�[�����p�\�R���ł����
        else
        {
            // �X�}�zUI�\���𖳌��ɂ���
            smartPhoneUi.SetActive(false);
        }

        // �C���^�t�F�[�X���g�����Ԃ�
        if (canUseInterf)
        {
            // X�L�[����͂����
            if (Input.GetKeyDown(KeyCode.X))
            {
                // �|�[�Y��ʂւ̑J�ڂ�L���ɂ���
                pauseSwitch = true;
            }
        }

        /* �|�[�Y��ʂ֑J�ڂ��� */
        if (pauseSwitch)
        {
            pauseSwitch = false;

            // �Q�[���̈ꎞ��~�����s����
            Time.timeScale = 0f;

            // �C���^�t�F�[�X���g���Ȃ���Ԃɂ���
            canUseInterf = false;

            // �|�[�Y��ʂ�L���ɂ���
            pauseUi.SetActive(true);
        }
        
        /* �Q�[���v���C�𑱍s���� */
        if (continueSwitch)
        {
            continueSwitch = false;

            // �|�[�Y��ʂ𖳌��ɂ���
            pauseUi.SetActive(false);

            // �C���^�t�F�[�X���g�����Ԃɂ���
            canUseInterf = true;

            // �Q�[���̈ꎞ��~����������
            Time.timeScale = 1.0f;
        }

        /* �Q�[���v���C���ĊJ�n����i1��̑ҋ@����j */
        if (restartSwitch)
        {
            restartSwitch = false;

            // �{�^�����g���Ȃ���Ԃɂ���
            canUseButton = false;

            StartCoroutine(GameRestart(0.3f));
        }

        /* �v���b�g�t�H�[����ʂ֑J�ڂ��� */
        if (platformSwitch)
        {
            // �v���b�g�t�H�[����ʂ�L���ɂ���
            platformUi.SetActive(true);
        }
        else
        {
            // �v���b�g�t�H�[����ʂ𖳌��ɂ���
            platformUi.SetActive(false);
        }

        /* �I�[�v�j���O�֑J�ڂ���i1��̑ҋ@����j */
        if (openingSwitch)
        {
            openingSwitch = false;

            // �{�^�����g���Ȃ���Ԃɂ���
            canUseButton = false;

            StartCoroutine(ToOpening(0.3f));
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

        // �̗̓o�[�i���F�j��\������
        noLivesBar.fillAmount = StaticUnits.MaxPlayerLives / 5.0f;

        // �̗̓o�[�i�ΐF�j���X�V����
        livesBar.fillAmount = nowPlayerLives / 5.0f;

        // �̗͂��[���ɂȂ��
        if (nowPlayerLives <= 0)
        {
            // �C���^�t�F�[�X���g���Ȃ���Ԃɂ���
            canUseInterf = false;

            // �{�^�����g���Ȃ���Ԃɂ���
            canUseButton = false;

            // �s�k�҂ɂȂ�i1��̑ҋ@����j
            StartCoroutine(Loser(2.5f));
        }

        // �ʏ��Ԃł����
        if (!fatigueSwitch)
        {
            // �C�̓o�[�̐F�����F�ɂ���
            energyBar.sprite = energy;

            // �C�̓o�[���X�V����
            energyBar.fillAmount = 1.0f - chargeTime / 3.0f;
        }
        // ��J��Ԃł����
        else
        {
            // �C�̓o�[�̐F�𐅐F�ɂ���
            energyBar.sprite = fatigue;

            // �C�̓o�[���X�V����
            energyBar.fillAmount = 1.0f - chargeTime / 5.0f;
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

            // �C���^�t�F�[�X���g���Ȃ���Ԃɂ���
            canUseInterf = false;

            // �{�^�����g���Ȃ���Ԃɂ���
            canUseButton = false;

            // �����҂ɂȂ�i1��̑ҋ@����j
            StartCoroutine(Winner(5.5f));
        }
    }

    private IEnumerator GameStart(float fWT)
    {
        // 1��ڂ̑ҋ@
        yield return new WaitForSecondsRealtime(fWT);

        // �C���^�t�F�[�X���g�����Ԃɂ���
        canUseInterf = true;

        // �{�^�����g�����Ԃɂ���
        canUseButton = true;

        // �X�^�[�g��ʂ𖳌��ɂ���
        startUi.SetActive(false);

        // �Q�[���̈ꎞ��~����������
        Time.timeScale = 1.0f;
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
        loserUi.SetActive(true);

        // �Q�[���̈ꎞ��~�����s����
        Time.timeScale = 0f;

        // 1��ڂ̑ҋ@
        yield return new WaitForSecondsRealtime(fWT);

        // �{�^�����g�����Ԃɂ���
        canUseButton = true;
    }

    private IEnumerator Winner(float fWT)
    {
        // �����҉�ʂ�L���ɂ���
        winnerUi.SetActive(true);

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
        nextDirector = GameObject.FindGameObjectWithTag("Director").GetComponent<GameEndingDirector>();

        // ��e�񐔂�ݒ肷��
        nextDirector.Damaged = StaticUnits.MaxPlayerLives - nowPlayerLives;

        // ��J��ԉ񐔂�ݒ肷��
        nextDirector.Fatigued = FatigueCnt;

        // ���̃V�[���ւ̕ϐ������n�����I������
        SceneManager.sceneLoaded -= GameEndingLoaded;
    }
}
