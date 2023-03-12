using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameEndingDirector : MonoBehaviour
{
    // �e�L�X�g�R���|�[�l���g
    private TextMeshProUGUI difficultyText;
    private TextMeshProUGUI damagedText;
    private TextMeshProUGUI fatiguedText;

    // ��Փx�W��
    private int level = 0;

    // ��Փx�]���̕�����
    private string[] difficulty = {
        "Practice", "Very Easy", "Easy", "Normal", 
        "Hard", "Very Hard", "Angel" 
    };

    // �A�j���[�V��������
    private float animTime = 4.0f;
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

    // ��e������
    private int damaged;
    public int Damaged
    {
        get { return damaged; }
        set { damaged = value; }
    }

    // ��J��ԂɂȂ�����
    private int fatigued;
    public int Fatigued
    {
        get { return fatigued; }
        set { fatigued = value; }
    }

    private void Start()
    {
        // �e�R���|�[�l���g���擾����
        difficultyText = GameObject.Find("Difficulty Text").GetComponent<TextMeshProUGUI>();
        damagedText = GameObject.Find("Damaged Text").GetComponent<TextMeshProUGUI>();
        fatiguedText = GameObject.Find("Fatigued Text").GetComponent<TextMeshProUGUI>();

        // �Q�[���̎��Ԃɉ����ē�Փx�W���𑝂₷
        if (StaticUnits.GameTime >= 45)
        {
            level++;

            if (StaticUnits.GameTime == 60)
            {
                level++;
            }
        }

        // �G�l�~�[�̈ړ����x�W���ɉ����ē�Փx�W���𑝂₷
        if (StaticUnits.EnemyMoveSpeed >= 8)
        {
            level++;

            if (StaticUnits.EnemyMoveSpeed == 12)
            {
                level++;
            }
        }

        // �v���C���[�̗͍̑ő�l�ɉ����ē�Փx�W���𑝂₷
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
            /* �I�[�v�j���O�ֈړ����� */

            // �i�t���O���I�t�ɂ��Ă��珈�����s���j
            openingSwitch = false;
            StartCoroutine(ToOpening(0.5f));
        }

        // �e�v���C�]����\������
        difficultyText.text = "difficulty : " + difficulty[level];
        damagedText.text = "damaged : " + damaged;
        fatiguedText.text = "fatigued : " + fatigued;
    }

    private IEnumerator ToOpening(float fWT)
    {
        // �ҋ@�����i0.5�b�j
        yield return new WaitForSeconds(fWT);

        // �I�[�v�j���O�V�[�������[�h����
        SceneManager.LoadScene("OpeningScene");
    }
}
