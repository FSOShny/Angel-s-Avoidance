using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GamePlayingDirector : MonoBehaviour
{
    // UI�I�u�W�F�N�g
    [SerializeField] private GameObject startUi;
    [SerializeField] private GameObject smartPhoneLeftUi;
    [SerializeField] private GameObject smartPhoneRightUi;
    [SerializeField] private GameObject pauseUi;
    [SerializeField] private GameObject platformUi;
    [SerializeField] private GameObject gameOverUi;
    [SerializeField] private GameObject gameClearUi;

    // �X�v���C�g
    [SerializeField] private Sprite energy;
    [SerializeField] private Sprite fatigue;

    // �R���|�[�l���g�i�C���[�W�A�e�L�X�g�A�f�B���N�^�[�j
    private Image noLivesBar;
    private Image livesBar;
    private Image energyBar;
    private Image upDown;
    private Image forwardBack;
    private TextMeshProUGUI timerText;
    private GameEndingDirector nextDirector;

    // ���݂̎c�莞��
    private float nowTimeLeft;

    // �i�����������j���݂̎c�莞��
    private int iTimeLeft;

    // �C���^�t�F�[�X���g�p�\���ǂ����̃t���O�ϐ�
    private bool canUseInterf = false;
    public bool CanUseInterf
    {
        get { return canUseInterf; }
    }

    // �{�^�����g�p�\���ǂ����̃t���O�ϐ�
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

    // �Q�[���v���C���ĊJ�n���邩�ǂ����̃t���O�ϐ�
    private bool restartSwitch = false;
    public bool RestartSwitch
    {
        get { return restartSwitch; }
        set { restartSwitch = value; }
    }

    // �v���b�g�t�H�[����ʂ�L���ɂ��邩�ǂ����̃t���O�ϐ�
    private bool platformSwitch = false;
    public bool PlatformSwitch
    {
        get { return platformSwitch; }
        set { platformSwitch = value; }
    }

    // �I�[�v�j���O�ֈړ����邩�ǂ����̃t���O�ϐ�
    private bool openingSwitch = false;
    public bool OpeningSwitch
    {
        get { return openingSwitch; }
        set { openingSwitch = value; }
    }

    // �ړ����[�h���u�㉺�v���ǂ����̃t���O�ϐ�
    private bool modeChange = false;
    public bool ModeChange
    {
        get { return modeChange; }
        set { modeChange = value; }
    }

    // �v���C���[����J��Ԃ��ǂ����̃t���O�ϐ�
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
        // �e�R���|�[�l���g���擾����
        noLivesBar = GameObject.Find("No Lives Bar").GetComponent<Image>();
        livesBar = GameObject.Find("Lives Bar").GetComponent<Image>();
        energyBar = GameObject.Find("Energy Bar").GetComponent<Image>();
        upDown = GameObject.Find("UpDown Mode Image").GetComponent<Image>();
        forwardBack = GameObject.Find("ForwardBack Mode Image").GetComponent<Image>();
        timerText = GameObject.Find("Timer Text").GetComponent<TextMeshProUGUI>();

        // �X�^�[�g��ʂ�L���ɂ���
        startUi.SetActive(true);

        // �X�^�[�g��ʈȊO�𖳌��ɂ���
        smartPhoneLeftUi.SetActive(false);
        smartPhoneRightUi.SetActive(false);
        pauseUi.SetActive(false);
        platformUi.SetActive(false);
        gameOverUi.SetActive(false);
        gameClearUi.SetActive(false);

        // ���݂̃v���C���[�̗̑͂�ݒ肷��
        nowPlayerLives = StaticUnits.MaxPlayerLives;

        // �㉺���[�h�摜�𖳌��ɂ���
        upDown.enabled = false;

        // ���݂̎c�莞�Ԃ�ݒ肷��
        nowTimeLeft = StaticUnits.GameTime;

        // �Q�[���v���C���J�n����
        StartCoroutine(GameStart(3.0f));
    }

    private void Update()
    {
        if (StaticUnits.SmartPhone)
        {
            /* ���݂̃v���b�g�t�H�[�����X�}�z�ł����
               �X�}�zUI�\����L���ɂ���               */

            smartPhoneLeftUi.SetActive(true);
            smartPhoneRightUi.SetActive(true);
        }
        else
        {
            /* ���݂̃v���b�g�t�H�[�����p�\�R���ł����
               �X�}�zUI�\���𖳌��ɂ���                 */

            smartPhoneLeftUi.SetActive(false);
            smartPhoneRightUi.SetActive(false);
        }

        // �i�C���^�t�F�[�X���g�p�\�ł���Ƃ��͗L���j
        if (canUseInterf)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                /* X�L�[����͂����
                   �|�[�Y��ʂ�L���ɂ��� */

                pauseSwitch = true;
            }
        }

        if (pauseSwitch)
        {
            /* �|�[�Y��ʂ�L���ɂ��� */

            // �Q�[���̈ꎞ��~�����s����
            Time.timeScale = 0f;

            // �C���^�t�F�[�X���g�p�s�ɂ���
            canUseInterf = false;

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

            // �C���^�t�F�[�X���g�p�\�ɂ���
            canUseInterf = true;

            // �Q�[���̈ꎞ��~����������
            Time.timeScale = 1.0f;
        }

        if (restartSwitch)
        {
            /* �Q�[���v���C���ĊJ�n���� */

            // �{�^�����g�p�s�ɂ���
            canUseButton = false;

            // �i�t���O���I�t�ɂ��Ă��珈�����s���j
            restartSwitch = false;
            StartCoroutine(GameRestart(0.3f));
        }

        if (platformSwitch)
        {
            /* �v���b�g�t�H�[����ʂ�L���ɂ��� */

            platformUi.SetActive(true);
        }
        else
        {
            /* �v���b�g�t�H�[����ʂ𖳌��ɂ��� */

            platformUi.SetActive(false);
        }

        if (openingSwitch)
        {
            /* �I�[�v�j���O�ֈړ����� */

            // �{�^�����g�p�s�ɂ���
            canUseButton = false;

            // �i�t���O���I�t�ɂ��Ă��珈�����s���j
            openingSwitch = false;
            StartCoroutine(ToOpening(0.3f));
        }

        if (modeChange)
        {
            /* �ړ����[�h���u�㉺�v�ł����
               �㉺���[�h�C���[�W��L���ɂ��� */

            forwardBack.enabled = false;
            upDown.enabled = true;
        }
        else
        {
            /* �ړ����[�h���u�O��v�ł����
               �O�ヂ�[�h�C���[�W��L���ɂ��� */

            forwardBack.enabled = true;
            upDown.enabled = false;
        }

        // �̗̓o�[�i���F�j��\������
        noLivesBar.fillAmount = StaticUnits.MaxPlayerLives / 5.0f;

        // �̗̓o�[�i�ΐF�j��\������
        livesBar.fillAmount = nowPlayerLives / 5.0f;

        if (nowPlayerLives <= 0)
        {
            /* �i�̗͂��[���ɂȂ�Ɓj
               �Q�[���I�[�o�[�ɂȂ�   */

            // �C���^�t�F�[�X�A�{�^�����g�p�s�ɂ���
            canUseInterf = false;
            canUseButton = false;

            // �Q�[���G���f�B���O�ֈړ�����
            StartCoroutine(GameOver(2.0f));
        }

        if (!fatigueSwitch)
        {
            /* �v���C���[����J��ԂłȂ����
               �C�̓o�[�̐F�����F�ɂ���       */

            energyBar.sprite = energy;

            // �C�̓o�[��\������
            energyBar.fillAmount = 1.0f - chargeTime / 3.0f;
        }
        else
        {
            /* �v���C���[����J��Ԃł����
               �C�̓o�[�̐F�𐅐F�ɂ���     */
            energyBar.sprite = fatigue;

            // �C�̓o�[��\������
            energyBar.fillAmount = 1.0f - chargeTime / 5.0f;
        }

        // ���݂̎c�莞�Ԃ��o�߂�����
        nowTimeLeft -= Time.deltaTime;

        // ���݂̎c�莞�Ԃ𐮐�������
        iTimeLeft = (int)nowTimeLeft;

        // �^�C�}�[���X�V����
        timerText.text = iTimeLeft.ToString();

        if (nowTimeLeft < 0f)
        {
            /* �i�c�莞�Ԃ��[���ɂȂ�Ɓj
               �Q�[���N���A�ɂȂ�         */

            // �C���^�t�F�[�X�A�{�^�����g�p�s�ɂ���
            canUseInterf = false;
            canUseButton = false;

            // �Q�[���G���f�B���O�ֈړ�����
            StartCoroutine(GameClear(5.0f));
        }
    }

    private IEnumerator GameStart(float fWT)
    {
        // �ҋ@�����i3.0�b�j
        yield return new WaitForSecondsRealtime(fWT);

        // �C���^�t�F�[�X���g�p�\�ɂ���
        canUseInterf = true;

        // �{�^�����g�p�\�ɂ���
        canUseButton = true;

        // �X�^�[�g��ʂ𖳌��ɂ���
        startUi.SetActive(false);

        // �Q�[���̈ꎞ��~����������
        Time.timeScale = 1.0f;
    }

    private IEnumerator GameRestart(float fWT)
    {
        // �ҋ@�����i0.3�b�j
        yield return new WaitForSecondsRealtime(fWT);

        // ���g�i�Q�[���v���C�V�[���j�����[�h����
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator ToOpening(float fWT)
    {
        // �ҋ@�����i0.3�b�j
        yield return new WaitForSecondsRealtime(fWT);

        // �Q�[���̈ꎞ��~����������
        Time.timeScale = 1.0f;

        // �I�[�v�j���O�V�[�������[�h����
        SceneManager.LoadScene("OpeningScene");
    }

    private IEnumerator GameOver(float fWT)
    {
        // �Q�[���I�[�o�[��ʂ�L���ɂ��A
        // �Q�[���̈ꎞ��~�����s����
        gameOverUi.SetActive(true);
        Time.timeScale = 0f;

        // �ҋ@�����i2.0�b�j
        yield return new WaitForSecondsRealtime(fWT);

        // �{�^�����g�p�\�ɂ���
        canUseButton = true;
    }

    private IEnumerator GameClear(float fWT)
    {
        // �Q�[���N���A��ʂ�L���ɂ��A
        // �Q�[���̈ꎞ��~�����s����
        gameClearUi.SetActive(true);
        Time.timeScale = 0f;

        // �ҋ@�����i5.0�b�j
        yield return new WaitForSecondsRealtime(fWT);

        // ���̃V�[���ւ̕ϐ������n�����J�n����
        SceneManager.sceneLoaded += GameEndingLoaded;

        // �Q�[���̈ꎞ��~����������
        Time.timeScale = 1.0f;

        // �Q�[���G���f�B���O�V�[�������[�h����
        SceneManager.LoadScene("GameEndingScene");
    }

    private void GameEndingLoaded(Scene scene, LoadSceneMode mode)
    {
        // �Q�[���G���f�B���O�f�B���N�^�[���擾����
        nextDirector = GameObject.FindGameObjectWithTag("Director").GetComponent<GameEndingDirector>();

        // ��e�����񐔁A��J��ԂɂȂ����񐔂�ݒ肷��
        nextDirector.Damaged = StaticUnits.MaxPlayerLives - nowPlayerLives;
        nextDirector.Fatigued = FatigueCnt;

        // ���̃V�[���ւ̕ϐ������n�����I������
        SceneManager.sceneLoaded -= GameEndingLoaded;
    }
}
