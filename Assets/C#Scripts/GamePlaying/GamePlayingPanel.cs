using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GamePlayingPanel : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public float cameRotSpeed = 0.3f; // �J�����̉�]���x

    private Transform came;
    private GamePlayingDirector director;
    private int swipe = 0; // �X���C�v�W��
    private Vector2 lastFingerPos; // �ȑO�̎w�̈ʒu
    private Vector2 newCameAng; // �J�����̊p�x

    private void Start()
    {
        // �J�����̊p�x���擾����
        came = GameObject.FindGameObjectWithTag("Camera").transform;

        // �Q�[���v���C�f�B���N�^�[���擾����
        director = GameObject.FindGameObjectWithTag("Director").GetComponent<GamePlayingDirector>();
    }

    private void LateUpdate()
    {
        // �X���C�v�W����1�ł����
        if (swipe == 1)
        {
            // �ŐV�̃J�����̊p�x��ݒ肷��
            newCameAng = came.transform.localEulerAngles;
        }
        // �X���C�v�W����2�ł����
        else if (swipe == 2)
        {
            // �J��������]������
            came.transform.localEulerAngles = newCameAng;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // �C���^�t�F�[�X���g�����Ԃŉ�ʂ��X���C�v���悤�Ƃ����
        if (director.CanUseInterf)
        {
            // �X���C�v�W����ݒ肷��
            swipe = 1;

            // ���݂̎w�̈ʒu��ݒ肷��
            lastFingerPos = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        // �C���^�t�F�[�X���g�����Ԃŉ�ʂ��X���C�v�����
        if (director.CanUseInterf)
        {
            // �X���C�v�W����ݒ肷��
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
        // �X���C�v�W��������������
        swipe = 0;
    }
}
