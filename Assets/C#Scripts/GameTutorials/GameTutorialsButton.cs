using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameTutorialsButton : MonoBehaviour,
    IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Image image;
    private GameTutorialsDirector director;

    private void Start()
    {
        // �C���[�W�R���|�[�l���g���擾����
        image = GetComponent<Image>();

        // �I�[�v�j���O�f�B���N�^�[���擾����
        director = GameObject.FindGameObjectWithTag("Director").GetComponent<GameTutorialsDirector>();
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        // �{�^������͂����

        // �{�^�����D�F�ɕω�������
        image.color = Color.gray;

        // �I�[�v�j���O�{�^���ł����
        if (name == "Opening Button")
        {
            // �I�[�v�j���O�ւ̑J�ڂ�L���ɂ���
            director.OpeningSwitch = true;
        }
        // �l�N�X�g�{�^���ł����
        else if (name == "Next Button")
        {
            // ���̃y�[�W�ւ̑J�ڂ�L���ɂ���
            director.NextSwitch = true;
        }
        // �v���r�A�X�{�^���ł����
        else if (name == "Previous Button")
        {
            // �O�̃y�[�W�ւ̑J�ڂ�L���ɂ���
            director.PrevSwitch = true;
        }

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
