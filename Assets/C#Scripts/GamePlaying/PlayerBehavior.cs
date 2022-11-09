using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float playerMoveSpeed = 10f; // �v���C���[�̈ړ����x

    private Rigidbody rigid;
    private new Transform camera;
    private GamePlayingDirector director;
    private bool canMove = true; // �������Ԃł��邩�ǂ���
    private float hInput; // �O��i�㉺�j�̈ړ����x
    private float vInput; // ���E�̈ړ����x
    private bool depthSwitch = true; // �ړ��^�C�v���u�O��v���ǂ���
    private bool guardSwitch = false; // �K�[�h�A�N�V���������s����ǂ���
    private float stuckTime = 0f; // �s���s�\����
    private float invTime = 0f; // ���G����

    private void NormalAct(int Y, int Z)
    {
        rigid.MovePosition(transform.position +
             transform.right * hInput * Time.fixedDeltaTime +
             transform.up * vInput * Time.fixedDeltaTime * Y +
             transform.forward * vInput * Time.fixedDeltaTime * Z);
    }

    private void GuardAct()
    {
        // �s���s�\���Ԃ�ݒ肷��
        stuckTime = 1.0f;

        // �s���\��Ԃ𖳌��ɂ���
        canMove = false;

        // ���G���Ԃ�ݒ肷��
        invTime = 1.0f;
    }

    private void KnockBack(int X, int Y, int Z)
    {
        rigid.AddForce(X, Y, Z, ForceMode.VelocityChange);

        // �v���C���[�̗̑͂����炷
        director.NowPlayerLife -= 1.0f;

        // �s���s�\���Ԃ�ݒ肷��
        stuckTime = 2.0f;

        // �s���\��Ԃ𖳌��ɂ���
        canMove = false;

        // ���G���Ԃ�ݒ肷��
        invTime = 4.0f;
    }

    private void Start()
    {
        // ���W�b�h�{�f�B�[�R���|�[�l���g���擾����
        rigid = GetComponent<Rigidbody>();

        // �J�����̊p�x���擾
        camera = GameObject.Find("Main Camera").transform;

        // �Q�[���v���C�f�B���N�^�[���擾����
        director = GameObject.Find("Game Playing Director").GetComponent<GamePlayingDirector>();
    }

    private void Update()
    {
        // W, S, ��, ���L�[�Ńv���C���[�̑O��i�㉺�j�̈ړ��ʂ����߂�
        hInput = Input.GetAxis("Horizontal") * playerMoveSpeed;

        // A, D, ��, ���L�[�Ńv���C���[�̍��E�̈ړ��ʂ����߂�
        vInput = Input.GetAxis("Vertical") * playerMoveSpeed;

        if (director.CanUseInterf)
        {
            // E�L�[�ňړ��^�C�v��ύX����
            if (Input.GetKeyDown(KeyCode.E))
            {
                depthSwitch = !depthSwitch;
            }

            // �X�y�[�X�L�[�ŃK�[�h�A�N�V������L���ɂ���
            if (Input.GetKeyDown(KeyCode.Space))
            {
                guardSwitch = true;
            }
        }

        // �s���s�\���Ԓ���
        if (stuckTime > 0f)
        {
            // ���Ԃ��o�߂�����
            stuckTime -= Time.deltaTime;
        }
        // �s���s�\���ԏI������
        else if (stuckTime < 0f)
        {
            // ���Ԃ�����������i����ȏ����̂��߁j
            stuckTime = 0f;

            // �v���C���[�ɂ������Ă��鑬�x���[���ɂ���
            rigid.velocity = Vector3.zero;

            // �s���\�����L���ɂ���
            canMove = true;
        }

        // ���G���Ԓ���
        if (invTime > 0f)
        {
            // ���Ԃ��o�߂�����
            invTime -= Time.deltaTime;
        }
        // ���G���ԏI������
        else if (invTime < 0f)
        {
            // ���Ԃ�����������i����ȏ����̂��߁j
            invTime = 0f;
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            // �ړ��^�C�v���u�O��v�ł����
            if (depthSwitch)
            {
                // �v���C���[��O�㍶�E�Ɉړ�������
                NormalAct(0, 1);
            }
            // �ړ��^�C�v���u�㉺�v�ł����
            else
            {
                // �v���C���[���㉺���E�Ɉړ�������
                NormalAct(1, 0);
            }

            // �K�[�h�A�N�V�������L���ł���ꍇ��
            if (guardSwitch)
            {
                guardSwitch = false;

                // �K�[�h�A�N�V���������s������
                GuardAct();
            }
        }

        // �v���C���[�̊p�x���J�����̊p�x�ɍ��킹��
        rigid.rotation = camera.rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ���G���ԊO�ł���
        if (invTime == 0)
        {
            /* �]�[����G�l�~�[�ɏՓ˂���ƃv���C���[���m�b�N�o�b�N���� */
            if (collision.gameObject.name == "Bottom")
            {
                KnockBack(0, 1, 0);
            }
            else if (collision.gameObject.name == "Front")
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
            else if (collision.gameObject.name == "EnemyPrefab(Clone)")
            {
                KnockBack(0, 0, 0);
            }
        }
    }
}
