using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameEndingButton : MonoBehaviour,
    IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    // �R���|�[�l���g�i�C���[�W�A�f�B���N�^�[�A�I�[�f�B�I�V�X�e���j
    private Image image;
    private GameEndingDirector director;
    private AudioSystem audioSystem;

    private void Start()
    {
        // �e�R���|�[�l���g���擾����
        image = GetComponent<Image>();
        director = GameObject.FindGameObjectWithTag("Director").GetComponent<GameEndingDirector>();
        audioSystem = GameObject.FindGameObjectWithTag("AudioSystem").GetComponent<AudioSystem>();
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        // �{�^������͂����
        // ���̃{�^���̖��邳���Â�����i���x�F���j
        // �i�A�j���[�V�����̂Ƃ��͖����j
        if (director.AnimTime == 0f)
        {
            image.color = Color.gray;

            // �i���ʉ��Đ�����j
            audioSystem.Music = 0;

            if (name == "Opening Button")
            {
                /* �I�[�v�j���O�ֈړ����� */

                director.OpeningSwitch = true;
            }
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        // �{�^���ɃJ�[�\���𓖂Ă��
        // ���̃{�^���̖��邳���Â�����i���x�F���j
        // �i�A�j���[�V�����̂Ƃ��͖����j
        if (director.AnimTime == 0f)
        {
            image.color = new Color(0.9f, 0.9f, 0.9f, 1.0f);
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        // �{�^������J�[�\�����O����
        // ���̃{�^���̖��邳�����ɖ߂�
        // �i�A�j���[�V�����̂Ƃ��͖����j
        if (director.AnimTime == 0f)
        {
            image.color = Color.white;
        }
    }
}
