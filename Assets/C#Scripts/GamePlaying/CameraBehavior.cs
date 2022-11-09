using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public float cameraRotSpeed = 0.2f; // �J�����̉�]���x

    private Transform player;
    private GamePlayingDirector director;
    private Vector2 lastMousePos; // �ȑO�̃}�E�X�ʒu
    private Vector2 newCameraAng; // �J�����̊p�x

    private void Start()
    {
        // �v���C���[�̈ʒu���擾����
        player = GameObject.Find("Player").transform;

        // �Q�[���v���C�f�B���N�^�[���擾����
        director = GameObject.Find("Game Playing Director").GetComponent<GamePlayingDirector>();
    }

    private void LateUpdate()
    {
        // �J�����̈ʒu���v���C���[�̈ʒu�ɍ��킹��
        transform.position = player.position;

        // �J�����̉�]���������߂�
        cameraRotSpeed *= StaticUnits.Direction;

        if (director.CanUseInterf)
        {
            // �}�E�X�����N���b�N�����ꍇ��
            if (Input.GetMouseButtonDown(0))
            {
                // �ŐV�̃J�����̊p�x���i�[����
                newCameraAng = transform.localEulerAngles;

                // ���݂̃}�E�X�ʒu���i�[����
                lastMousePos = Input.mousePosition;
            }
            // ���̂܂܍��N���b�N�𑱂����ꍇ��
            else if (Input.GetMouseButton(0))
            {
                // �ȑO�̃}�E�X�ʒu�ƌ��݂̃}�E�X�ʒu����J�����̉�]�ʂ����߂�
                newCameraAng.x += (lastMousePos.y - Input.mousePosition.y) * cameraRotSpeed;
                newCameraAng.y += (Input.mousePosition.x - lastMousePos.x) * cameraRotSpeed;

                // �J��������]������
                transform.localEulerAngles = newCameraAng;

                // ���݂̃}�E�X�ʒu���i�[����
                lastMousePos = Input.mousePosition;
            }
        }
    }
}
