using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameOptionsButton : MonoBehaviour,
    IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Sprite purpleButton;
    [SerializeField] private Sprite yellowButton;
    [SerializeField] private Sprite whiteButton;

    private Image image;
    private GameOptionsDirector director;

    private void Start()
    {
        // �C���[�W�R���|�[�l���g���擾����
        image = GetComponent<Image>();

        // �I�[�v�j���O�f�B���N�^�[���擾����
        director = GameObject.FindGameObjectWithTag("Director").GetComponent<GameOptionsDirector>();
    }

    private void Update()
    {
        // �L���ɂȂ��Ă���I�v�V�����{�^���ł����
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
            // ���F�̃{�^���ɂ���
            image.sprite = purpleButton;
        }
        // �I�[�v�j���O�{�^���ł����
        else if (name == "Opening Button")
        {
            // ���F�̃{�^���ɂ���
            image.sprite = yellowButton;
        }
        // ����ȊO�̃{�^���ł����
        else
        {
            // ���F�̃{�^���ɂ���
            image.sprite = whiteButton;
        }
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        // �{�^������͂����

        // �{�^�����D�F�ɕω�������
        image.color = Color.gray;

        // 30�b�{�^���ł����
        if (name == "30 Seconds Button")
        {
            // �Q�[���̎��Ԃ��u30�b�v�ɂ���
            StaticUnits.GameTime = 30f;
        }
        // 45�b�{�^���ł����
        else if (name == "45 Seconds Button")
        {
            // �Q�[���̎��Ԃ��u45�b�v�ɂ���
            StaticUnits.GameTime = 45f;
        }
        // 60�b�{�^���ł����
        else if (name == "60 Seconds Button")
        {
            // �Q�[���̎��Ԃ��u60�b�v�ɂ���
            StaticUnits.GameTime = 60f;
        }
        // �ᑬ�x�{�^���ł����
        else if (name == "Low Speed Button")
        {
            // �G�l�~�[�̈ړ����x���u�ᑬ�x�v�ɂ���
            StaticUnits.EnemyMoveSpeed = 4;
        }
        // �ʏ푬�x�{�^���ł����
        else if (name == "Normal Speed Button")
        {
            // �G�l�~�[�̈ړ����x���u�ʏ푬�x�v�ɂ���
            StaticUnits.EnemyMoveSpeed = 8;
        }
        // �����x�{�^���ł����
        else if (name == "High Speed Button")
        {
            // �G�l�~�[�̈ړ����x���u�����x�v�ɂ���
            StaticUnits.EnemyMoveSpeed = 12;
        }
        // 5���C�t�{�^���ł����
        else if (name == "5 Lives Button")
        {
            // �v���C���[�̗͍̑ő�l���u5�v�ɂ���
            StaticUnits.MaxPlayerLives = 5;
        }
        // 3���C�t�{�^���ł����
        else if (name == "3 Lives Button")
        {
            // �v���C���[�̗͍̑ő�l���u3�v�ɂ���
            StaticUnits.MaxPlayerLives = 3;
        }
        // 2���C�t�{�^���ł����
        else if (name == "2 Lives Button")
        {
            // �v���C���[�̗͍̑ő�l���u2�v�ɂ���
            StaticUnits.MaxPlayerLives = 2;
        }
        // ���]�I���{�^���ł����
        else if (name == "Reverse On Button")
        {
            // �J�����̔��]�W�����u�I���v�ɂ���
            StaticUnits.Reverse = -1;
        }
        // ���]�I�t�{�^���ł����
        else if (name == "Reverse Off Button")
        {
            // �J�����̔��]�W�����u�I�t�v�ɂ���
            StaticUnits.Reverse = 1;
        }
        // �I�[�v�j���O�{�^���ł����
        else if (name == "Opening Button")
        {
            // �I�[�v�j���O�ւ̑J�ڔ����L���ɂ���
            director.OpeningSwitch = true;
        }
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        // �{�^���𗣂���

        // �{�^���𔒐F�ɕω�������i���̐F�ɖ߂��j
        image.color = Color.white;
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        // �{�^���ɃJ�[�\���𓖂Ă��

        // �{�^���𔒂��ۂ��D�F�ɕω�������
        image.color = new Color(0.9f, 0.9f, 0.9f, 1.0f);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        // �{�^������J�[�\�����O����

        // �{�^���𔒐F�ɕω�������i���̐F�ɖ߂��j
        image.color = Color.white;
    }
}
