using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GamePlayingButton : MonoBehaviour,
    IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    // �R���|�[�l���g�i�C���[�W�A�f�B���N�^�[�A�v���C���[�j
    private Image image;
    private GamePlayingDirector director;
    private PlayerBehavior player;

    private void Start()
    {
        // �e�R���|�[�l���g���擾����
        image = GetComponent<Image>();
        director = GameObject.FindGameObjectWithTag("Director").GetComponent<GamePlayingDirector>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>();
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        // �{�^������͂����
        // ���̃{�^���̖��邳���Â�����i���x�F���j
        // �i�{�^�����g�p�\�ł���Ƃ��͗L���j
        if (director.CanUseButton)
        {
            image.color = Color.gray;

            // �i�C���^�t�F�[�X���g�p�\�ł���A����
            //   �v���C���[���ړ��\�ł���Ƃ��͗L���j
            if (director.CanUseInterf && player.CanMove)
            {
                // �i�{�^�����ƂɐU�镑����ς���j
                if (name == "Up Arrow Button")
                {
                    /* �O�i��j�ւ̉�����L���ɂ��� */

                    player.UpMove = true;
                }
                else if (name == "Left Arrow Button")
                {
                    /* ���ւ̉�����L���ɂ��� */

                    player.LeftMove = true;
                }
                else if (name == "Down Arrow Button")
                {
                    /* ��i���j�ւ̉�����L���ɂ��� */

                    player.DownMove = true;
                }
                else if (name == "Right Arrow Button")
                {
                    /* �E�ւ̉�����L���ɂ��� */

                    player.RightMove = true;
                }
                else if (name == "Mode Change Button")
                {
                    /* �ړ����[�h���`�F���W���� */

                    director.ModeChange = !director.ModeChange;
                }

                // �i�v���C���[����J��Ԃł���Ƃ��͖����j
                if (!director.FatigueSwitch)
                {
                    if (name == "Guard Action Button")
                    {
                        /* �K�[�h�A�N�V�������s���i����������j */

                        player.Guard = true;
                    }
                }
            }

            // �i�{�^�����ƂɐU�镑����ς���j
            if (name == "Pause Button")
            {
                /* �|�[�Y��ʂ�L���ɂ��� */

                director.PauseSwitch = true;
            }
            else if (name == "Continue Button")
            {
                /* �|�[�Y��ʂ𖳌��ɂ��� */

                director.ContinueSwitch = true;

                // �{�^���̖��邳�����ɖ߂�
                image.color = Color.white;
            }
            else if (name == "Restart Button")
            {
                /* �Q�[���v���C���ĊJ�n���� */

                director.RestartSwitch = true;
            }
            // �v���b�g�t�H�[���{�^���ł����
            else if (name == "Platform Button")
            {
                /* �v���b�g�t�H�[����ʂ�L���ɂ��� */

                director.PlatformSwitch = true;

                // �{�^���̖��邳�����ɖ߂�
                image.color = Color.white;
            }
            else if (name == "Opening Button")
            {
                /* �I�[�v�j���O�ֈړ����� */

                director.OpeningSwitch = true;
            }
            else if (name == "PC Button")
            {
                /* ���݂̃v���b�g�t�H�[�����p�\�R���ɂ��� */

                StaticUnits.SmartPhone = false;

                // �v���b�g�t�H�[����ʂ𖳌��ɂ��A
                // �{�^���̖��邳�����ɖ߂�
                director.PlatformSwitch = false;
                image.color = Color.white;
            }
            else if (name == "Smart Phone Button")
            {
                /* ���݂̃v���b�g�t�H�[�����X�}�z�ɂ��� */

                StaticUnits.SmartPhone = true;

                // �v���b�g�t�H�[����ʂ𖳌��ɂ��A
                // �{�^���̖��邳�����ɖ߂�
                director.PlatformSwitch = false;
                image.color = Color.white;
            }
        }
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        // �{�^���𗣂���
        // ���̃{�^���̖��邳�����ɖ߂�
        image.color = Color.white;

        // �i�{�^�����g�p�\�ł���Ƃ��͗L���j
        if (director.CanUseButton)
        {
            // �i�{�^�����ƂɐU�镑����ς���j
            if (name == "Up Arrow Button")
            {
                /* �O�i��j�ւ̉����𖳌��ɂ��� */

                player.UpMove = false;
            }
            else if (name == "Left Arrow Button")
            {
                /* ���ւ̉����𖳌��ɂ��� */

                player.LeftMove = false;
            }
            else if (name == "Down Arrow Button")
            {
                /* ��i���j�ւ̉����𖳌��ɂ��� */

                player.DownMove = false;
            }
            else if (name == "Right Arrow Button")
            {
                /* �E�ւ̉����𖳌��ɂ��� */

                player.RightMove = false;
            }
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        // �{�^���ɃJ�[�\���𓖂Ă��
        // ���̃{�^���̖��邳���Â�����i���x�F���j
        // �i�{�^�����g�p�\�ł���Ƃ��͗L���j
        if (director.CanUseButton)
        {
            image.color = new Color(0.9f, 0.9f, 0.9f, 1.0f);
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        // �{�^������J�[�\�����O����
        // ���̃{�^���̖��邳�����ɖ߂�
        image.color = Color.white;
    }
}
