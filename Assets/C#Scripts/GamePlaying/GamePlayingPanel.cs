using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GamePlayingPanel : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // �J�����̉�]���x
    public float cameRotSpeed = 0.3f;

    // �J�����̕ψ�
    private Transform came;

    // �f�B���N�^�[�R���|�[�l���g
    private GamePlayingDirector director;

    // �X���C�v�W��
    private int swipe = 0;

    // �ȑO�̎w�̈ʒu
    private Vector2 lastFingerPos;

    // ���݂̃J�����̊p�x
    private Vector2 newCameAng;

    private void Start()
    {
        // �e�R���|�[�l���g���擾����
        came = GameObject.FindGameObjectWithTag("Camera").transform;
        director = GameObject.FindGameObjectWithTag("Director").GetComponent<GamePlayingDirector>();
    }

    private void LateUpdate()
    {
        if (swipe == 1)
        {
            /* �X���C�v�W����1�ł���Ƃ���
               ���݂̃J�����̊p�x��ݒ肷�� */

            newCameAng = came.transform.localEulerAngles;
        }
        else if (swipe == 2)
        {
            /* �X���C�v�W����2�ł���Ƃ���
               �J��������]������          */

            came.transform.localEulerAngles = newCameAng;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // �i�C���^�t�F�[�X���g�p�\�ł���Ƃ��͗L���j
        if (director.CanUseInterf)
        {
            /* ��ʂ��X���C�v���n�߂��
               �X���C�v�W����1�ɐݒ肷�� */

            swipe = 1;

            // ���݂̎w�̈ʒu��ݒ肷��
            lastFingerPos = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        // �i�C���^�t�F�[�X���g�p�\�ł���Ƃ��͗L���j
        if (director.CanUseInterf)
        {
            /* ���̂܂܉�ʂ��X���C�v�������Ă����
               �X���C�v�W����2�ɐݒ肷��            */

            swipe = 2;

            // �ȑO�̎w�̈ʒu�ƌ��݂̎w�̈ʒu����J�����̉�]�ʂ����߂�
            newCameAng.x += (lastFingerPos.y - eventData.position.y) * cameRotSpeed * StaticUnits.Reverse;
            newCameAng.y += (eventData.position.x - lastFingerPos.x) * cameRotSpeed * StaticUnits.Reverse;

            // ���݂̎w�̈ʒu��ݒ肷��
            lastFingerPos = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // ��ʂ��X���C�v���I�����
        // �X���C�v�W����0�ɐݒ肷��
        swipe = 0;
    }
}
