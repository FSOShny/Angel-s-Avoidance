using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float moveSpeed = 10f; // �v���C���[�̈ړ����x

    private new Camera camera;
    private Rigidbody rigid;
    private float hInput;
    private float vInput;
    private bool depthSwitch = true;
    private bool moveCan = true;
    private float invTime = 0;

    private void Start()
    {
        // �J�����̊p�x���擾
        camera = Camera.main;

        // ���W�b�h�{�f�B�[�R���|�[�l���g���擾����
        rigid = GetComponent<Rigidbody>();
    }

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

        if (invTime > 0) // ���G���Ԓ���
        {
            // ���G���Ԃ��X�V����
            invTime -= Time.deltaTime;
        }
        else if (invTime < 0) // ���G���ԏI������
        {
            // �v���C���[�ɂ������Ă��鑬�x���[���ɂ���
            rigid.velocity = Vector3.zero;

            // �������Ԃɂ���
            moveCan = true;
        }
    }

    private void FixedUpdate()
    {
        if (moveCan) // �������Ԃł���
        {
            if (depthSwitch) // �ړ��^�C�v���u�O��v�ł���ꍇ��
            {
                // �v���C���[��O�㍶�E�Ɉړ�������
                NormalMove(0, 1);
            }
            else // �ړ��^�C�v���u�㉺�v�ł���ꍇ��
            {
                // �v���C���[���㉺���E�Ɉړ�������
                NormalMove(1, 0);
            }
        }

        // �v���C���[�̊p�x���J�����̊p�x�ɍ��킹��
        transform.rotation = camera.transform.rotation;
    }

    public void NormalMove(int Y, int Z)
    {
         rigid.MovePosition(transform.position + 
             transform.right * hInput * Time.fixedDeltaTime + 
             transform.up * vInput * Time.fixedDeltaTime * Y + 
             transform.forward * vInput * Time.fixedDeltaTime * Z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �_���[�W�]�[���ɏՓ˂���ƃv���C���[���m�b�N�o�b�N����
        if (collision.gameObject.name == "Front")
        {
            KnockBack(0, 0, -1);
        }
        else if (collision.gameObject.name == "Left")
        {
            KnockBack(1, 0, 0);
        }
        else if (collision.gameObject.name == "Back")
        {
            KnockBack(0, 0, 1);
        }
        else if (collision.gameObject.name == "Right")
        {
            KnockBack(-1, 0, 0);
        }
        else if (collision.gameObject.name == "Top")
        {
            KnockBack(0, -1, 0);
        }
        else if (collision.gameObject.name == "Enemy(Clone)")
        {
            KnockBack(0, 0, 0);
        }
    }

    public void KnockBack(int X, int Y, int Z)
    {
        rigid.AddForce(new Vector3(X, Y, Z), ForceMode.Impulse);
        invTime = 2;
        moveCan = false;
    }
}