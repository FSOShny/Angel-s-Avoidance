using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GamePlayingButton : MonoBehaviour,
    IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Image image;
    private GamePlayingDirector director;

    private void Start()
    {
        // �C���[�W�R���|�[�l���g���擾����
        image = GetComponent<Image>();

        // �Q�[���v���C�f�B���N�^�[���擾����
        director = GameObject.Find("Game Playing Director").GetComponent<GamePlayingDirector>();
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        // �{�^�����g�����ԂŃ{�^����������
        if (director.CanUseButton)
        {
            // �{�^�����D�F�ɕω�������
            image.color = Color.gray;

            // �|�[�Y�{�^���ł����
            if (name == "Pause Button")
            {
                // �|�[�Y��ʂւ̑J�ڔ����L���ɂ���
                director.PauseSwitch = true;
            }
            // �R���e�B�j���[�{�^���ł����
            else if (name == "Continue Button")
            {
                // �Q�[���v���C�̑��s�����L���ɂ���
                director.ContinueSwitch = true;
            }
            // ���X�^�[�g�{�^���ł����
            else if (name == "Restart Button")
            {
                // �Q�[���v���C�̍ĊJ�n�����L���ɂ���
                director.RestartSwitch = true;
            }
            // �v���b�g�t�H�[���{�^���ł����
            else if (name == "Platform Button")
            {
                // �v���b�g�t�H�[����ʂւ̑J�ڔ����L���ɂ���
                director.PlatformSwitch = true;
            }
            // �p�\�R���{�^���ł����
            else if (name == "PC Button")
            {
                // ���݂̃v���b�g�t�H�[�����p�\�R���ɍX�V����
                StaticUnits.SmartPhone = false;

                // �v���b�g�t�H�[����ʂւ̑J�ڔ���𖳌��ɂ���i�|�[�Y��ʂ֖߂�j
                director.PlatformSwitch = false;
            }
            // �X�}�z�{�^���ł����
            else if (name == "Smart Phone Button")
            {
                // ���݂̃v���b�g�t�H�[�����X�}�z�ɍX�V����
                StaticUnits.SmartPhone = true;

                // �v���b�g�t�H�[����ʂւ̑J�ڔ���𖳌��ɂ���i�|�[�Y��ʂ֖߂�j
                director.PlatformSwitch = false;
            }
            // �N�C�b�g�{�^���ł����
            else if (name == "Quit Button")
            {
                // �I�[�v�j���O�ւ̑J�ڔ����L���ɂ���
                director.OpeningSwitch = true;
            }

            // �{�^���𔒐F�ɕω�������i���̐F�ɖ߂��j
            image.color = Color.white;
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        // �{�^�����g�����ԂŃ{�^���ɃJ�[�\���𓖂Ă��
        if (director.CanUseButton)
        {
            // �{�^���𔒂��ۂ��D�F�ɕω�������
            image.color = new Color(0.9f, 0.9f, 0.9f, 1.0f);
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        // �{�^�����g�����ԂŃ{�^������J�[�\�����O����
        if (director.CanUseButton)
        {
            // �{�^���𔒐F�ɕω�������i���̐F�ɖ߂��j
            image.color = Color.white;
        }
    }
}
