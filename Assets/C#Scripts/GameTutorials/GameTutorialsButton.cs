using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameTutorialsButton : MonoBehaviour,
    IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    // �R���|�[�l���g�i�C���[�W�A�f�B���N�^�[�A�I�[�f�B�I�V�X�e���j
    private Image image;
    private GameTutorialsDirector director;
    private AudioSystem audioSystem;

    private void Start()
    {
        // �e�R���|�[�l���g���擾����
        image = GetComponent<Image>();
        director = GameObject.FindGameObjectWithTag("Director").GetComponent<GameTutorialsDirector>();
        audioSystem = GameObject.FindGameObjectWithTag("AudioSystem").GetComponent<AudioSystem>();
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        // �{�^������͂����
        // ���̃{�^���̖��邳���Â�����i���x�F���j
        image.color = Color.gray;

        // �i�{�^�����ƂɐU�镑����ς���j
        if (CompareTag("SkipText"))
        {
            // �i���ʉ��Đ�����j
            audioSystem.Music = 0;

            if (name == "Skip Text (1)")
            {
                /* ���݂̃y�[�W��0�ɐݒ肷�� */

                director.NowPage = 0;
            }
            else if (name == "Skip Text (2)")
            {
                /* ���݂̃y�[�W��2�ɐݒ肷�� */

                director.NowPage = 2;
            }
            else if (name == "Skip Text (3)")
            {
                /* ���݂̃y�[�W��10�ɐݒ肷�� */

                director.NowPage = 10;
            }
            else if (name == "Skip Text (4)")
            {
                /* ���݂̃y�[�W��13�ɐݒ肷�� */

                director.NowPage = 13;
            }
            else if (name == "Skip Text (5)")
            {
                /* ���݂̃y�[�W��17�ɐݒ肷�� */

                director.NowPage = 17;
            }
            else if (name == "Skip Text (6)")
            {
                /* ���݂̃y�[�W��20�ɐݒ肷�� */

                director.NowPage = 20;
            }

            // �|�[�Y��ʂ𖳌��ɂ��A
            // �ݒ肵���y�[�W�ֈړ�����
            director.ContinueSwitch = true;
            director.PageSwitch = true;
        }
        else
        {
            // �i�{�^�����ƂɐU�镑����ς���j
            if (name == "Previous Button")
            {
                /* �O�̃y�[�W����ݒ肷��i���ʉ��Đ�����j */

                audioSystem.Music = 0;
                director.NowPage--;

                // �ݒ肵���y�[�W�ֈړ�����
                director.PageSwitch = true;
            }
            else if (name == "Next Button")
            {
                /* ���̃y�[�W����ݒ肷��i���ʉ��Đ�����j */

                audioSystem.Music = 0;
                director.NowPage++;

                // �ݒ肵���y�[�W�ֈړ�����
                director.PageSwitch = true;
            }
            else if (name == "Pause Button")
            {
                /* �|�[�Y��ʂ�L���ɂ���i���ʉ��Đ�����j */

                audioSystem.Music = 1;
                director.PauseSwitch = true;
            }
            else if (name == "Continue Button")
            {
                /* �|�[�Y��ʂ𖳌��ɂ���i���ʉ��Đ�����j */

                audioSystem.Music = 2;
                director.ContinueSwitch = true;
            }
            else if (name == "Opening Button")
            {
                /* �I�[�v�j���O�ֈړ�����i���ʉ��Đ�����j */

                audioSystem.Music = 3;
                director.OpeningSwitch = true;
            }
        }

        // ���̃{�^���̖��邳�����ɖ߂�
        image.color = Color.white;
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        // �{�^���ɃJ�[�\���𓖂Ă��
        // ���̃{�^���̖��邳���Â�����i���x�F���j
        image.color = new Color(0.9f, 0.9f, 0.9f, 1.0f);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        // �{�^������J�[�\�����O����
        // ���̃{�^���̖��邳�����ɖ߂�
        image.color = Color.white;
    }
}
