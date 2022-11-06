using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float moveSpeed = 10f; // �v���C���[�̈ړ����x

    private Rigidbody rigid;
    private new Camera camera;
    private GameDirector game;
    private float hInput;
    private float vInput;
    private bool depthSwitch = true;
    private bool guardSwitch = false;
    private bool moveCan = true;
    private float invTime = 0f;
    private float stuckTime = 0f;

    private void Start()
    {
        // ���W�b�h�{�f�B�[�R���|�[�l���g���擾����
        rigid = GetComponent<Rigidbody>();

        // �J�����̊p�x���擾
        camera = Camera.main;

        // �Q�[���f�B���N�^�[��L���ɂ���
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

        // �K�[�h���u�L���v�ɂ���
        if (Input.GetKeyDown(KeyCode.Space))
        {
            guardSwitch = true;
        }

        if (invTime > 0f) // ���G���Ԓ���
        {
            // ���G���Ԃ��X�V����
            invTime -= Time.deltaTime;
        }
        else if (invTime <= 0f) // ���G���ԏI������
        {
            // �Փ˔���𐳏�ɂ��邽�߂Ƀ[����������
            invTime = 0f;
        }

        if (stuckTime > 0f) // �s���s�\���Ԓ���
        {
            // �s���s�\���Ԃ��X�V����
            stuckTime -= Time.deltaTime;
        }
        else if (stuckTime <= 0f) // �s���s�\���ԏI������
        {
            // �v���C���[�ɂ������Ă��鑬�x���[���ɂ���
            rigid.velocity = Vector3.zero;

            // �s���\�ɂ���
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

            if (guardSwitch) // �K�[�h���u�L���v�ł���ꍇ��
            {
                // �K�[�h�A�N�V���������s������
                GuardMove();
            }
        }

        // �v���C���[�̊p�x���J�����̊p�x�ɍ��킹��
        rigid.rotation = camera.transform.rotation;
    }

    public void NormalMove(int Y, int Z)
    {
        rigid.MovePosition(transform.position + 
             transform.right * hInput * Time.fixedDeltaTime + 
             transform.up * vInput * Time.fixedDeltaTime * Y + 
             transform.forward * vInput * Time.fixedDeltaTime * Z);
    }

    public void GuardMove()
    {
        invTime = 1f;
        stuckTime = 1f;
        moveCan = false;
        guardSwitch = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (invTime == 0) // ���G���Ԃ��[���ł���ꍇ��
        {
            // �_���[�W�]�[����G�l�~�[�ɏՓ˂���ƃv���C���[���m�b�N�o�b�N����
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
        rigid.AddForce(X, Y, Z, ForceMode.VelocityChange);
        game.Life -= 1f;
        invTime = 4f;
        stuckTime = 2f;
        moveCan = false;
    }
}
