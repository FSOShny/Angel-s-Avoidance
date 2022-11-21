using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    private Transform player;
    private GamePlayingDirector director;
    private float cameRotSpeed = 0.1f; // �J�����̉�]���x
    private Vector2 lastMousePos; // �ȑO�̃}�E�X�ʒu
    private Vector2 newCameAng; // �J�����̊p�x

    // ���_�𐧌�ł����Ԃ��ǂ���
    private bool control = true;

    public bool Control
    {
        get { return control; }
        set { control = value; }
    }

    private void Start()
    {
        // �v���C���[�̈ʒu���擾����
        player = GameObject.Find("Player").transform;

        // �Q�[���v���C�f�B���N�^�[���擾����
        director = GameObject.FindGameObjectWithTag("Director").GetComponent<GamePlayingDirector>();
    }

    private void LateUpdate()
    {
        // �J�����̈ʒu���v���C���[�̈ʒu�ɍ��킹��
        transform.position = player.position;

        // �C���^�t�F�[�X���g�����Ԃł���A���_�𐧌�ł����Ԃ�
        if (director.CanUseInterf && control)
        {
            // �}�E�X�����N���b�N���n�߂��
            if (Input.GetMouseButtonDown(0))
            {
                // �ŐV�̃J�����̊p�x��ݒ肷��
                newCameAng = transform.localEulerAngles;

                // ���݂̃}�E�X�ʒu��ݒ肷��
                lastMousePos = Input.mousePosition;
            }
            // ���̂܂܍��N���b�N���������
            else if (Input.GetMouseButton(0))
            {
                // �ȑO�̃}�E�X�ʒu�ƌ��݂̃}�E�X�ʒu����J�����̉�]�ʂ����߂�
                newCameAng.x += (lastMousePos.y - Input.mousePosition.y) * cameRotSpeed * StaticUnits.Reverse;
                newCameAng.y += (Input.mousePosition.x - lastMousePos.x) * cameRotSpeed * StaticUnits.Reverse;

                // �J��������]������
                transform.localEulerAngles = newCameAng;

                // ���݂̃}�E�X�ʒu��ݒ肷��
                lastMousePos = Input.mousePosition;
            }
        }
    }
}
