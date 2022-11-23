using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameTutorialsButton : MonoBehaviour,
    IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
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
