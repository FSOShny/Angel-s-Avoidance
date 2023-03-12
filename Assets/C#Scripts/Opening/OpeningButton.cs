using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OpeningButton : MonoBehaviour, 
    IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    // �R���|�[�l���g�i�C���[�W�A�f�B���N�^�[�A�I�[�f�B�I�V�X�e���j
    private Image image;
    private OpeningDirector director;
    private AudioSystem audioSystem;

    private void Start()
    {
        // �e�R���|�[�l���g���擾����
        image = GetComponent<Image>();
        director = GameObject.FindGameObjectWithTag("Director").GetComponent<OpeningDirector>();
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

            // �i�{�^�����ƂɐU�镑����ς���j
            if (name == "PC Button")
            {
                /* ���݂̃v���b�g�t�H�[�����p�\�R���ɂ���i���ʉ��Đ�����j */

                audioSystem.Music = 0;
                StaticUnits.SmartPhone = false;

                // �I�[�v�j���O��ʂ�L���ɂ���
                director.OpeningSwitch = true;
            }
            else if (name == "Smart Phone Button")
            {
                /* ���݂̃v���b�g�t�H�[�����X�}�z�ɂ���i���ʉ��Đ�����j */

                audioSystem.Music = 0;
                StaticUnits.SmartPhone = true;

                // �I�[�v�j���O��ʂ�L���ɂ���
                director.OpeningSwitch = true;
            }
            else if (name == "Tutorials Button")
            {
                /* �Q�[���`���[�g���A���ֈړ�����i���ʉ��Đ�����j */

                audioSystem.Music = 1;
                director.TutorialsSwitch = true;
            }
            else if (name == "Playing Button")
            {
                /* �Q�[���v���C�ֈړ�����i���ʉ��Đ�����j */

                audioSystem.Music = 2;
                director.PlayingSwitch = true;
            }
            else if (name == "Options Button")
            {
                /* �Q�[���I�v�V�����ֈړ�����i���ʉ��Đ�����j */

                audioSystem.Music = 3;
                director.OptionsSwitch = true;
            }
            else if (name == "Shutdown Button")
            {

                /* �Q�[�����V���b�g�_�E������i���ʉ��Đ�����j */

                audioSystem.Music = 4;
                Application.Quit();

                // �i�G�f�B�^�[�ł̔����m�F�j
                Debug.Log("Shutdown !!!");
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
