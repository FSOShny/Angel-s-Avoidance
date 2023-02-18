using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameOptionsButton : MonoBehaviour,
    IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    // �X�v���C�g
    [SerializeField] private Sprite purpleButton;
    [SerializeField] private Sprite yellowButton;
    [SerializeField] private Sprite whiteButton;

    // �R���|�[�l���g�i�C���[�W�A�f�B���N�^�[�j
    private Image image;
    private GameOptionsDirector director;

    private void Start()
    {
        // �e�R���|�[�l���g���擾����
        image = GetComponent<Image>();
        director = GameObject.FindGameObjectWithTag("Director").GetComponent<GameOptionsDirector>();
    }

    private void Update()
    {
        if ((name == "30 Seconds Button" && StaticUnits.GameTime == 30f) || 
            (name == "45 Seconds Button" && StaticUnits.GameTime == 45f) || 
            (name == "60 Seconds Button" && StaticUnits.GameTime == 60f) || 
            (name == "Low Speed Button" && StaticUnits.EnemyMoveSpeed == 4) || 
            (name == "Normal Speed Button" && StaticUnits.EnemyMoveSpeed == 8) || 
            (name == "High Speed Button" && StaticUnits.EnemyMoveSpeed == 12) || 
            (name == "5 Lives Button" && StaticUnits.MaxPlayerLives == 5) || 
            (name == "3 Lives Button" && StaticUnits.MaxPlayerLives == 3) || 
            (name == "2 Lives Button" && StaticUnits.MaxPlayerLives == 2) || 
            (name == "Reverse On Button" && StaticUnits.Reverse == -1) || 
            (name == "Reverse Off Button" && StaticUnits.Reverse == 1))
        {
            /* �L���ɂȂ��Ă���I�v�V�����{�^���͎��F�ɂ��� */

            image.sprite = purpleButton;
        }
        else if (name == "Opening Button")
        {
            /* �I�[�v�j���O�{�^���͉��F�ɂ��� */

            image.sprite = yellowButton;
        }
        else
        {
            /* ��L�ɊY�����Ȃ��{�^���͔��F�ɂ��� */

            image.sprite = whiteButton;
        }
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        // �{�^������͂����
        // ���̃{�^���̖��邳���Â�����i���x�F���j
        image.color = Color.gray;

        // �i�{�^�����ƂɐU�镑����ς���j
        if (name == "30 Seconds Button")
        {
            /* �Q�[���̎��Ԃ��u30�b�v�ɂ��� */

            StaticUnits.GameTime = 30f;
        }
        else if (name == "45 Seconds Button")
        {
            /* �Q�[���̎��Ԃ��u45�b�v�ɂ��� */

            StaticUnits.GameTime = 45f;
        }
        else if (name == "60 Seconds Button")
        {
            /* �Q�[���̎��Ԃ��u60�b�v�ɂ��� */

            StaticUnits.GameTime = 60f;
        }
        else if (name == "Low Speed Button")
        {
            /* �G�l�~�[�̈ړ����x���u�ᑬ�v�ɂ��� */

            StaticUnits.EnemyMoveSpeed = 4;
        }
        else if (name == "Normal Speed Button")
        {
            /* �G�l�~�[�̈ړ����x���u�ʏ�v�ɂ��� */

            StaticUnits.EnemyMoveSpeed = 8;
        }
        else if (name == "High Speed Button")
        {
            /* �G�l�~�[�̈ړ����x���u�����v�ɂ��� */

            StaticUnits.EnemyMoveSpeed = 12;
        }
        else if (name == "5 Lives Button")
        {
            /* �v���C���[�̗͍̑ő�l���u5�v�ɂ��� */

            StaticUnits.MaxPlayerLives = 5;
        }
        else if (name == "3 Lives Button")
        {
            /* �v���C���[�̗͍̑ő�l���u3�v�ɂ��� */

            StaticUnits.MaxPlayerLives = 3;
        }
        else if (name == "2 Lives Button")
        {
            /* �v���C���[�̗͍̑ő�l���u2�v�ɂ��� */

            StaticUnits.MaxPlayerLives = 2;
        }
        else if (name == "Reverse On Button")
        {
            /* �J�����̔��]�W�����u�I���v�ɂ��� */

            StaticUnits.Reverse = -1;
        }
        else if (name == "Reverse Off Button")
        {
            /* �J�����̔��]�W�����u�I�t�v�ɂ��� */

            StaticUnits.Reverse = 1;
        }
        else if (name == "Opening Button")
        {
            /* �I�[�v�j���O�ֈړ����� */

            director.OpeningSwitch = true;
        }
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        // �{�^���𗣂���
        // ���̃{�^���̖��邳�����ɖ߂�
        image.color = Color.white;
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        // �{�^���ɃJ�[�\���𓖂Ă��
        // ���̃{�^���̖��邳���Â�����i���x�F���j
        image.color = new Color(0.9f, 0.9f, 0.9f, 1.0f);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        // �{�^������J�[�\�����O����
        // ���̃{�^���̖��邳�����ɖ߂�
        image.color = Color.white;
    }
}
