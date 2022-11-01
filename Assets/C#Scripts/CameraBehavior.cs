using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public float rotateSpeed = 0.2f; // �J�����̉�]���x

    private Transform player;
    private Vector2 lastMousePos;
    private Vector2 newAngle;

    // Start is called before the first frame update
    private void Start()
    {
        // �v���C���[�̈ʒu���擾����
        player = GameObject.Find("Player").transform;
    }

    private void LateUpdate()
    {
        // �J�����̈ʒu���v���C���[�̈ʒu�ɍ��킹��
        transform.position = player.position;

        if (Input.GetMouseButtonDown(0)) // �}�E�X�����N���b�N�����
        {
            // ���݂̃J�����̊p�x���i�[����
            newAngle = transform.localEulerAngles;

            // ���݂̃}�E�X�̈ʒu���i�[����
            lastMousePos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0)) // ���̂܂܍��N���b�N�𑱂��Ă����
        {
            // �i�[����Ă���}�E�X�̈ʒu�ƌ��݂̃}�E�X�̈ʒu����J�����̉�]�ʂ����߂�
            newAngle.x += (lastMousePos.y - Input.mousePosition.y) * rotateSpeed;
            newAngle.y += (Input.mousePosition.x - lastMousePos.x) * rotateSpeed;

            // �J��������]������
            transform.localEulerAngles = newAngle;

            // �i�[����}�E�X�̈ʒu���X�V����
            lastMousePos = Input.mousePosition;
        }
    }
}
