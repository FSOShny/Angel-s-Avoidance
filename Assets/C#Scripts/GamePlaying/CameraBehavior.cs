using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    private Transform player;
    private GamePlayingDirector director;
    private float cameraRotSpeed = 0.1f; // �J�����̉�]���x
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

        // �C���^�t�F�[�X���g�����Ԃ�
        if (director.CanUseInterf)
        {
            // �}�E�X�����N���b�N���n�߂��
            if (Input.GetMouseButtonDown(0))
            {
                // �ŐV�̃J�����̊p�x��ݒ肷��
                newCameraAng = transform.localEulerAngles;

                // ���݂̃}�E�X�ʒu��ݒ肷��
                lastMousePos = Input.mousePosition;
            }
            // ���̂܂܍��N���b�N���������
            else if (Input.GetMouseButton(0))
            {
                // �ȑO�̃}�E�X�ʒu�ƌ��݂̃}�E�X�ʒu����J�����̉�]�ʂ����߂�
                newCameraAng.x += (lastMousePos.y - Input.mousePosition.y) * cameraRotSpeed;
                newCameraAng.y += (Input.mousePosition.x - lastMousePos.x) * cameraRotSpeed;

                // �J��������]������
                transform.localEulerAngles = newCameraAng * StaticUnits.Reverse;

                // ���݂̃}�E�X�ʒu��ݒ肷��
                lastMousePos = Input.mousePosition;
            }
        }
    }
}
