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
        // �A�j���[�V�������ԊO�Ƀ{�^�����������ꍇ��
        if (director.AnimTime == 0f)
        {
            // �{�^�����D�F�ɕω�������
            image.color = new Color(0.5f, 0.5f, 0.5f, 1.0f);

            // �p�\�R���{�^�����X�}�z�{�^���ł����
            if (name == "PC Button" || name == "Smart Phone Button")
            {
                // �I�[�v�j���O��ʂւ̑J�ڔ����L���ɂ���
                director.Opening = true;
            }

            // �v���C�{�^���ł����
            if (name == "Playing Button")
            {
                // �Q�[���v���C�ւ̑J�ڔ����L���ɂ���
                director.Playing = true;
            }

            // �I�v�V�����{�^���ł����
            if (name == "Options Button")
            {
                // �Q�[���I�v�V�����ւ̑J�ڔ����L���ɂ���
                director.Options = true;
            }
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        // �A�j���[�V�������ԊO�Ƀ{�^���ɃJ�[�\���𓖂Ă��ꍇ��
        if (director.AnimTime == 0f)
        {
            // �{�^���𔒂��ۂ��D�F�ɕω�������
            image.color = new Color(0.9f, 0.9f, 0.9f, 1.0f);
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        // �A�j���[�V�������ԊO�Ƀ{�^������J�[�\�����O�����ꍇ��
        if (director.AnimTime == 0f)
        {
            // �{�^���𔒐F�ɕω�������i���̐F�ɖ߂��j
            image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
    }
}
