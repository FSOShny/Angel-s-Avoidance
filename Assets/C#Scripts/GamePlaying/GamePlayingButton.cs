using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GamePlayingButton : MonoBehaviour,
    IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Image image;
    private GamePlayingDirector director;
    private PlayerBehavior player;

    private void Start()
    {
        // �C���[�W�R���|�[�l���g���擾����
        image = GetComponent<Image>();

        // �Q�[���v���C�f�B���N�^�[���擾����
        director = GameObject.FindGameObjectWithTag("Director").GetComponent<GamePlayingDirector>();

        // �v���C���[�r�w�C�r�A���擾����
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>();
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        // �{�^�����g�����ԂŃ{�^������͂����
        if (director.CanUseButton)
        {
            // �{�^�����D�F�ɕω�������
            image.color = Color.gray;

            // �C���^�t�F�[�X���g�����Ԃł���A�v���C���[���������Ԃł���
            if (director.CanUseInterf && player.CanMove)
            {
                // ����{�^���ł����
            �@�@if (name == "Up Arrow Button")
                {
                    // �O�i��j�։�������
                    player.UpMove = true;
                }
                // �����{�^���ł����
                else if (name == "Left Arrow Button")
                {
                    // ���։�������
                    player.LeftMove = true;
                }
                // �����{�^���ł����
                else if (name == "Down Arrow Button")
                {
                    // ��i���j�։�������
                    player.DownMove = true;
                }
                // �E���{�^���ł����
                else if (name == "Right Arrow Button")
                {
                    // �E�։�������
                    player.RightMove = true;
                }
                // ���[�h�`�F���W�{�^���ł����
                else if (name == "Mode Change Button")
                {
                    // �ړ����[�h���`�F���W����
                    director.ModeChange = !director.ModeChange;
                }

                // ��J��ԂłȂ�
                if (!director.FatigueSwitch)
                {
                    // �K�[�h�A�N�V�����{�^���ł����
                    if (name == "Guard Action Button")
                    {
                        // �K�[�h�A�N�V���������s�ł����Ԃɂ���
                        player.Guard = true;
                    }
                }
            }

            // �|�[�Y�{�^���ł����
            if (name == "Pause Button")
            {
                // �|�[�Y��ʂւ̑J�ڂ�L���ɂ���
                director.PauseSwitch = true;
            }
            // �R���e�B�j���[�{�^���ł����
            else if (name == "Continue Button")
            {
                // �Q�[���v���C�̑��s��L���ɂ���
                director.ContinueSwitch = true;

                // �{�^���𔒐F�ɕω�������i���̐F�ɖ߂��j
                image.color = Color.white;
            }
            // ���X�^�[�g�{�^���ł����
            else if (name == "Restart Button")
            {
                // �Q�[���v���C�̍ĊJ�n��L���ɂ���
                director.RestartSwitch = true;
            }
            // �v���b�g�t�H�[���{�^���ł����
            else if (name == "Platform Button")
            {
                // �v���b�g�t�H�[����ʂւ̑J�ڂ�L���ɂ���
                director.PlatformSwitch = true;

                // �{�^���𔒐F�ɕω�������i���̐F�ɖ߂��j
                image.color = Color.white;
            }
            // �I�[�v�j���O�{�^���ł����
            else if (name == "Opening Button")
            {
                // �I�[�v�j���O�ւ̑J�ڂ�L���ɂ���
                director.OpeningSwitch = true;
            }
            // �p�\�R���{�^���ł����
            else if (name == "PC Button")
            {
                // ���݂̃v���b�g�t�H�[�����p�\�R���ɍX�V����
                StaticUnits.SmartPhone = false;

                // �v���b�g�t�H�[����ʂւ̑J�ڂ𖳌��ɂ���i�|�[�Y��ʂ֖߂�j
                director.PlatformSwitch = false;

                // �{�^���𔒐F�ɕω�������i���̐F�ɖ߂��j
                image.color = Color.white;
            }
            // �X�}�z�{�^���ł����
            else if (name == "Smart Phone Button")
            {
                // ���݂̃v���b�g�t�H�[�����X�}�z�ɍX�V����
                StaticUnits.SmartPhone = true;

                // �v���b�g�t�H�[����ʂւ̑J�ڂ𖳌��ɂ���i�|�[�Y��ʂ֖߂�j
                director.PlatformSwitch = false;

                // �{�^���𔒐F�ɕω�������i���̐F�ɖ߂��j
                image.color = Color.white;
            }
        }
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        // �{�^�����g�����ԂŃ{�^���𗣂���
        if (director.CanUseButton)
        {
            // ����{�^���ł����
            if (name == "Up Arrow Button")
            {
                // ��������
                player.UpMove = false;
            }
            // �����{�^���ł����
            else if (name == "Left Arrow Button")
            {
                // ��������
                player.LeftMove = false;
            }
            // �����{�^���ł����
            else if (name == "Down Arrow Button")
            {
                // ��������
                player.DownMove = false;
            }
            // �E���{�^���ł����
            else if (name == "Right Arrow Button")
            {
                // ��������
                player.RightMove = false;
            }
        }

        // �{�^���𔒐F�ɕω�������i���̐F�ɖ߂��j
        image.color = Color.white;
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
        // �{�^������J�[�\�����O����

        // �{�^���𔒐F�ɕω�������i���̐F�ɖ߂��j
        image.color = Color.white;
    }
}
