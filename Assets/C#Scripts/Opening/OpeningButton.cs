using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OpeningButton : MonoBehaviour, 
    IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Image image;
    private OpeningDirector director;

    private void Start()
    {
        // �C���[�W�R���|�[�l���g���擾����
        image = GetComponent<Image>();

        // �I�[�v�j���O�f�B���N�^�[���擾����
        director = GameObject.Find("Opening Director").GetComponent<OpeningDirector>();
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        // �A�j���[�V�������ԊO�Ń{�^�����������ꍇ��
        if (director.AnimTime == 0f)
        {
            // �{�^�����D�F�ɕω�������
            image.color = Color.gray;

            // �p�\�R���{�^�����X�}�z�{�^���ł����
            if (name == "PC Button")
            {
                // ���݂̃v���b�g�t�H�[�����p�\�R���ɍX�V����
                StaticUnits.SmartPhone = false;

                // �I�[�v�j���O��ʂւ̑J�ڔ����L���ɂ���
                director.OpeningSwitch = true;
            }
            else if (name == "Smart Phone Button")
            {
                // ���݂̃v���b�g�t�H�[�����X�}�z�ɍX�V����
                StaticUnits.SmartPhone = true;

                // �I�[�v�j���O��ʂւ̑J�ڔ����L���ɂ���
                director.OpeningSwitch = true;
            }
            // �v���C�{�^���ł����
            else if (name == "Playing Button")
            {
                // �Q�[���v���C�ւ̑J�ڔ����L���ɂ���
                director.PlayingSwitch = true;
            }
            // �I�v�V�����{�^���ł����
            else if (name == "Options Button")
            {
                // �Q�[���I�v�V�����ւ̑J�ڔ����L���ɂ���
                director.OptionsSwitch = true;
            }
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        // �A�j���[�V�������ԊO�Ń{�^���ɃJ�[�\���𓖂Ă��ꍇ��
        if (director.AnimTime == 0f)
        {
            // �{�^���𔒂��ۂ��D�F�ɕω�������
            image.color = new Color(0.9f, 0.9f, 0.9f, 1.0f);
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        // �A�j���[�V�������ԊO�Ń{�^������J�[�\�����O�����ꍇ��
        if (director.AnimTime == 0f)
        {
            // �{�^���𔒐F�ɕω�������i���̐F�ɖ߂��j
            image.color = Color.white;
        }
    }
}
