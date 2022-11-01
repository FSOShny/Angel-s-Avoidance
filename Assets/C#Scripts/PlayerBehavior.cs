using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float moveSpeed = 10f; // �v���C���[�̈ړ����x

    private new Camera camera;
    private Rigidbody rb;
    private float hInput;
    private float vInput;
    private bool depthSwitch = true;

    // Start is called before the first frame update
    private void Start()
    {
        // �J�����̊p�x���擾
        camera = Camera.main;
        // ���W�b�h�{�f�B�[�R���|�[�l���g���擾����
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        // �v���C���[�̑O��i�㉺�j�̈ړ��ʂ����߂�
        hInput = Input.GetAxis("Horizontal") * moveSpeed;
        // �v���C���[�̍��E�̈ړ��ʂ����߂�
        vInput = Input.GetAxis("Vertical") * moveSpeed;

        // �ړ��^�C�v��ύX����
        if (Input.GetKeyDown(KeyCode.E))
        {
            depthSwitch = !depthSwitch;
        }
    }

    private void FixedUpdate()
    {
        if (depthSwitch) // �ړ��^�C�v���u�O��v�ł���ꍇ
        {
            NormalMove(0, 1);
        }
        else // �ړ��^�C�v���u�㉺�v�ł���ꍇ
        {
            NormalMove(1, 0);
        }

        // �v���C���[�̊p�x���J�����̊p�x�ɍ��킹��
        transform.rotation = camera.transform.rotation;
    }

    public void NormalMove(int Y, int Z)
    {
        // �v���C���[���ړ�������
        rb.MovePosition(transform.position +
                transform.right * hInput * Time.fixedDeltaTime +
                transform.up * vInput * Time.fixedDeltaTime * Y +
                transform.forward * vInput * Time.fixedDeltaTime * Z);
    }
}
