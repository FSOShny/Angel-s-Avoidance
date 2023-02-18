using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    // �J�����̉�]���x
    public float cameRotSpeed = 0.1f;

    // �v���C���[�̕ψ�
    private Transform player;

    // �f�B���N�^�[�R���|�[�l���g
    private GamePlayingDirector director;

    // �ȑO�̃}�E�X�̈ʒu
    private Vector2 lastMousePos;

    // ���݂̃J�����̊p�x
    private Vector2 newCameAng;

    private void Start()
    {
        // �e�R���|�[�l���g���擾����
        player = GameObject.Find("Player").transform;
        director = GameObject.FindGameObjectWithTag("Director").GetComponent<GamePlayingDirector>();
    }

    private void LateUpdate()
    {
        // �J�����̈ʒu���v���C���[�̈ʒu�ɍ��킹��
        transform.position = player.position;

        // �i�C���^�t�F�[�X���g�p�\�ł���A����
        //   ���݂̃v���b�g�t�H�[�����p�\�R���ł���Ƃ��͗L���j
        if (director.CanUseInterf && !StaticUnits.SmartPhone)
        {
            if (Input.GetMouseButtonDown(0))
            {
                /* �}�E�X�����N���b�N���n�߂��
                   ���݂̃J�����̊p�x��ݒ肷�� */

                newCameAng = transform.localEulerAngles;

                // ���݂̃}�E�X�̈ʒu��ݒ肷��
                lastMousePos = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                /* ���̂܂܃}�E�X�����N���b�N���������
                   �ȑO�̃}�E�X�̈ʒu�ƌ��݂̃}�E�X�̈ʒu����J�����̉�]�ʂ����߂� */

                newCameAng.x += (lastMousePos.y - Input.mousePosition.y) * cameRotSpeed * StaticUnits.Reverse;
                newCameAng.y += (Input.mousePosition.x - lastMousePos.x) * cameRotSpeed * StaticUnits.Reverse;

                // �J��������]������
                transform.localEulerAngles = newCameAng;

                // ���݂̃}�E�X�̈ʒu��ݒ肷��
                lastMousePos = Input.mousePosition;
            }
        }
    }
}
