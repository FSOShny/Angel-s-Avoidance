using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerBehavior : MonoBehaviour
{
    public float moveSpeed = 10f; // �v���C���[�̈ړ����x

    private new Camera camera;
    private Rigidbody rigid;
    private GameDirector game;
    private float hInput;
    private float vInput;
    private bool depthSwitch = true;
    private bool avoidSwitch = false;
    private bool moveCan = true;
    private Vector3 moveDir = new Vector3(0f, 0f, 1f);
    private float invTime = 0f;

    private void Start()
    {
        // �J�����̊p�x���擾
        camera = Camera.main;

        // ���W�b�h�{�f�B�[�R���|�[�l���g���擾����
        rigid = GetComponent<Rigidbody>();

        game = GameObject.Find("Game Director").GetComponent<GameDirector>();
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

        if (Input.GetKeyDown(KeyCode.F))
        {
            avoidSwitch = true;
        }

        if (invTime > 0f) // ���G���Ԓ���
        {
            // ���G���Ԃ��X�V����
            invTime -= Time.deltaTime;
        }
        else if (invTime <= 0f) // ���G���ԏI������
        {
            invTime = 0f;

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

                if (avoidSwitch)
                {
                    AvoidMove(0, 1);
                }
            }
            else // �ړ��^�C�v���u�㉺�v�ł���ꍇ��
            {
                // �v���C���[���㉺���E�Ɉړ�������
                NormalMove(1, 0);

                if (avoidSwitch)
                {
                    AvoidMove(1, 0);
                }
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

    public void AvoidMove(int Y, int Z)
    {
        rigid.MovePosition(transform.position +
             transform.right * hInput * Time.fixedDeltaTime * 10 +
             transform.up * vInput * Time.fixedDeltaTime * Y  * 10 +
             transform.forward * vInput * Time.fixedDeltaTime * Z * 10 );
        invTime = 1f;
        moveCan = false;
        avoidSwitch = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (invTime == 0) // ���G���Ԃ��[���ł���ꍇ��
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
    }

    public void KnockBack(int X, int Y, int Z)
    {
        rigid.AddForce(new Vector3(X, Y, Z), ForceMode.Impulse);
        game.Life -= 1f;
        invTime = 2;
        moveCan = false;
    }
}