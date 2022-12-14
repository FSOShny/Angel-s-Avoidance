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

        // �X�L�b�v�e�L�X�g�ł���
        if (CompareTag("SkipText"))
        {
            // �X�L�b�v�e�L�X�g(1)�ł����
            if (name == "Skip Text (1)")
            {
                // ����̃y�[�W����ݒ肷��
                director.NowPage = 0;
            }
            // �X�L�b�v�e�L�X�g(2)�ł����
            else if (name == "Skip Text (2)")
            {
                // ����̃y�[�W����ݒ肷��
                director.NowPage = 2;
            }

            // �Q�[���v���C�̑��s��L���ɂ���
            director.ContinueSwitch = true;

            // ����̃y�[�W�ւ̑J�ڂ�L���ɂ���
            director.PageSwitch = true;
        }
        // �X�L�b�v�e�L�X�g�łȂ�
        else
        {
            // �v���r�A�X�{�^���ł����
            if (name == "Previous Button")
            {
                // �O�̃y�[�W����ݒ肷��
                director.NowPage--;

                // �O�̃y�[�W�ւ̑J�ڂ�L���ɂ���
                director.PageSwitch = true;
            }
            // �l�N�X�g�{�^���ł����
            else if (name == "Next Button")
            {
                // ���̃y�[�W����ݒ肷��
                director.NowPage++;

                // ���̃y�[�W�ւ̑J�ڂ�L���ɂ���
                director.PageSwitch = true;
            }
            // �|�[�Y�{�^���ł����
            else if (name == "Pause Button")
            {
                // �|�[�Y��ʂւ̑J�ڂ�L���ɂ���
                director.PauseSwitch = true;
            }
            // �R���e�B�j���[�{�^���ł����
            else if (name == "Continue Button")
            {
                // �Q�[���v���C�̑��s��L���ɂ���
                director.ContinueSwitch = true;
            }
            // �I�[�v�j���O�{�^���ł����
            else if (name == "Opening Button")
            {
                // �I�[�v�j���O�ւ̑J�ڂ�L���ɂ���
                director.OpeningSwitch = true;
            }
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
