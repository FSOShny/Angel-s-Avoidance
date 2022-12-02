using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameEndingDirector : MonoBehaviour
{
    private TextMeshProUGUI difficultyText;
    private TextMeshProUGUI damagedText;
    private TextMeshProUGUI fatiguedText;
    private int level = 0; // ��Փx�W��
    private string[] difficulty = {
        "Practice", "Very Easy", "Easy", "Normal", 
        "Hard", "Very Hard", "Angel" 
    }; // ��Փx�]��

    // �A�j���[�V��������
    private float animTime = 4.0f;

    public float AnimTime
    {
        get { return animTime; }
    }

    // �I�[�v�j���O�֑J�ڂ��邩�ǂ���
    private bool openingSwitch = false;

    public bool OpeningSwitch
    {
        get { return openingSwitch; }
        set { openingSwitch = value; }
    }

    // ��e��
    private int damaged;

    public int Damaged
    {
        get { return damaged; }
        set { damaged = value; }
    }

    // ��J��ԉ�
    private int fatigued;

    public int Fatigued
    {
        get { return fatigued; }
        set { fatigued = value; }
    }

    private void Start()
    {
        // ��Փx�]���̃e�L�X�g�R���|�[�l���g���擾����
        difficultyText = GameObject.Find("Difficulty Text").GetComponent<TextMeshProUGUI>();

        // ��e�񐔕]���̃e�L�X�g�R���|�[�l���g���擾����
        damagedText = GameObject.Find("Damaged Text").GetComponent<TextMeshProUGUI>();

        // ��J��ԉ񐔕]���̃e�L�X�g�R���|�[�l���g���擾����
        fatiguedText = GameObject.Find("Fatigued Text").GetComponent<TextMeshProUGUI>();

        /* �Q�[���̐������Ԃɉ����ē�Փx�W���𑝂₷ */
        if (StaticUnits.GameTimeLim >= 45)
        {
            level++;

            if (StaticUnits.GameTimeLim == 60)
            {
                level++;
            }
        }

        /* �G�l�~�[�̈ړ����x�W���ɉ����ē�Փx�W���𑝂₷ */
        if (StaticUnits.EnemyMoveSpeed >= 8)
        {
            level++;

            if (StaticUnits.EnemyMoveSpeed == 12)
            {
                level++;
            }
        }

        /* �v���C���[�̗͍̑ő�l�ɉ����ē�Փx�W���𑝂₷ */
        if (StaticUnits.MaxPlayerLives <= 3.0f)
        {
            level++;

            if (StaticUnits.MaxPlayerLives == 2.0f)
            {
                level++;
            }
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

        /* �I�[�v�j���O�֑J�ڂ���i1��̑ҋ@����j */
        if (openingSwitch)
        {
            openingSwitch = false;

            StartCoroutine(ToOpening(0.3f));
        }

        // ��Փx�]�����X�V����
        difficultyText.text = "difficulty : " + difficulty[level];

        // ��e�񐔕]�����X�V����
        damagedText.text = "damaged : " + damaged;

        // ��J��ԉ񐔕]�����X�V����
        fatiguedText.text = "fatigued : " + fatigued;
    }

    private IEnumerator ToOpening(float fWT)
    {
        // 1��ڂ̑ҋ@
        yield return new WaitForSeconds(fWT);

        SceneManager.LoadScene("OpeningScene");
    }
}
