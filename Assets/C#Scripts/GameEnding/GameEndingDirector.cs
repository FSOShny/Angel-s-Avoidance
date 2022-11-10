using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameEndingDirector : MonoBehaviour
{
    private TextMeshProUGUI difficultyText;
    private TextMeshProUGUI attackedText;
    private int level = 0; // ��Փx�W��
    private string[] difficulty = 
        { "Very Easy", "Easy", "Normal", "Hard", "Very Hard" }; // ��Փx

    private float animTime = 3.0f; // �A�j���[�V��������

    public float AnimTime
    {
        get { return animTime; }
    }

    private bool openingSwitch = false; // �I�[�v�j���O�֑J�ڂ��邩�ǂ���

    public bool OpeningSwitch
    {
        get { return openingSwitch; }
        set { openingSwitch = value; }
    }

    private int attacked; // ��e��

    public int Attacked
    {
        get { return attacked; }
        set { attacked = value; }
    }

    private void Start()
    {
        // ��Փx�\���̃e�L�X�g�R���|�[�l���g���擾����
        difficultyText = GameObject.Find("Difficulty Text").GetComponent<TextMeshProUGUI>();

        // ��e�\���̃e�L�X�g�R���|�[�l���g���擾����
        attackedText = GameObject.Find("Attacked Text").GetComponent<TextMeshProUGUI>();

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
        if (0f <= StaticUnits.MaxPlayerLife && StaticUnits.MaxPlayerLife <= 5.0f)
        {
            level++;

            if (StaticUnits.MaxPlayerLife == 3.0f)
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

        // ��Փx�\�����X�V����
        difficultyText.text = "difficulty : " + difficulty[level];

        // ��e�\�����X�V����
        attackedText.text = "attacked : " + attacked;
    }

    private IEnumerator ToOpening(float fWT)
    {
        // 1��ڂ̑ҋ@
        yield return new WaitForSeconds(fWT);

        SceneManager.LoadScene("OpeningScene");
    }
}
