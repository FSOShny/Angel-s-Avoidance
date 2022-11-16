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

    private int nowPlayerLives; // ���݂̃v���C���[�̗̑�

    public int NowPlayerLives
    {
        get { return nowPlayerLives; }
        set { nowPlayerLives = value; }
    }

    private bool canUseInterf = false; // �C���^�t�F�[�X���g�����Ԃ��ǂ���

    public bool CanUseInterf
    {
        get { return canUseInterf; }
    }

    private bool pauseSwitch = false; // �|�[�Y��ʂ֑J�ڂ��邩�ǂ���

    public bool PauseSwitch
    {
        get { return pauseSwitch; }
        set { pauseSwitch = value; }
    }

    private bool continueSwitch = true; // �Q�[���v���C�𑱍s���邩�ǂ���

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

    private bool canUseButton = false; // �{�^�����g�����Ԃ��ǂ���

    public bool CanUseButton
    {
        get { return canUseButton; }
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

    private bool modeChange = false; // �ړ����[�h���u�㉺�v���ǂ���

    public bool ModeChange
    {
        get { return modeChange; }
        set { modeChange = value; }
    }

    private bool fatigueSwitch = false; // ��J��Ԃ��ǂ���

    public bool FatigueSwitch
    {
        get { return fatigueSwitch; }
        set { fatigueSwitch = value; }
    }

    private float chargeTime = 0f; // �C�͉񕜎���

    public float ChargeTime
    {
        get { return chargeTime; }
        set { chargeTime = value; }
    }

    private int fatigueCnt = 0; // ��J��ԉ�

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
        startUI.SetActive(true);
        smartPhoneUI.SetActive(false);
        pauseUI.SetActive(false);
        platformUI.SetActive(false);
        loserUI.SetActive(false);
        winnerUI.SetActive(false);

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
            pauseUI.SetActive(true);
        }
        
        /* �Q�[���v���C�𑱍s���� */
        if (continueSwitch)
        {
            continueSwitch = false;

            // �|�[�Y��ʂ𖳌��ɂ���
            pauseUI.SetActive(false);

            // �C���^�t�F�[�X���g�����Ԃɂ���
            canUseInterf = true;

            // �Q�[���̈ꎞ��~����������
            Time.timeScale = 1.0f;
        }

        /* �Q�[���v���C���ĊJ�n����i1��̑ҋ@����j */
        if (restartSwitch)
        {
            restartSwitch = false;

            // �C���^�t�F�[�X���g���Ȃ���Ԃɂ���
            canUseInterf = false;

            // �{�^�����g���Ȃ���Ԃɂ���
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

            // �C���^�t�F�[�X���g���Ȃ���Ԃɂ���
            canUseInterf = false;

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
        if (nowPlayerLives == 0 || nowPlayerLives == -1)
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

        // �C�͉񕜎��Ԓ���
        if (chargeTime > 0f)
        {
            // ���Ԃ��o�߂�����
            chargeTime -= Time.deltaTime;
        }
        // �C�͉񕜎��ԏI������
        else if (chargeTime < 0f)
        {
            // ���Ԃ�����������i����ȏ����̂��߁j
            chargeTime = 0f;

            // �ʏ��Ԃɂ���
            fatigueSwitch = false;
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

        // �{�^�����g�����Ԃɂ���
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

        // ��e�񐔂�ݒ肷��
        nextDirector.Damaged = StaticUnits.MaxPlayerLives - nowPlayerLives;

        // ��J��ԉ񐔂�ݒ肷��
        nextDirector.Fatigued = FatigueCnt;

        // ���̃V�[���ւ̕ϐ������n�����I������
        SceneManager.sceneLoaded -= GameEndingLoaded;
    }
}
