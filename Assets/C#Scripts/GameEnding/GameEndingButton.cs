using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameEndingButton : MonoBehaviour,
    IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Image image;
    private GameEndingDirector director;

    private void Start()
    {
        // �C���[�W�R���|�[�l���g���擾����
        image = GetComponent<Image>();

        // �I�[�v�j���O�f�B���N�^�[���擾����
        director = GameObject.Find("Game Ending Director").GetComponent<GameEndingDirector>();
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        // �A�j���[�V�������ԊO�Ƀ{�^����������
        if (director.AnimTime == 0f)
        {
            // �{�^�����D�F�ɕω�������
            image.color = Color.gray;

            // �I�[�v�j���O�{�^���ł����
            if (name == "Opening Button")
            {
                // �I�[�v�j���O�ւ̑J�ڔ����L���ɂ���
                director.OpeningSwitch = true;
            }
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        // �A�j���[�V�������ԊO�Ƀ{�^���ɃJ�[�\���𓖂Ă��
        if (director.AnimTime == 0f)
        {
            // �{�^���𔒂��ۂ��D�F�ɕω�������
            image.color = new Color(0.9f, 0.9f, 0.9f, 1.0f);
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        // �A�j���[�V�������ԊO�Ƀ{�^������J�[�\�����O����
        if (director.AnimTime == 0f)
        {
            // �{�^���𔒐F�ɕω�������i���̐F�ɖ߂��j
            image.color = Color.white;
        }
    }
}