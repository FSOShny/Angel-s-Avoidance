using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private Rigidbody rigid;
    private Material playerMat;
    private new Transform camera;
    private GamePlayingDirector director;
    private int damage = 1; // �v���C���[�̔�e�W��
    private int penaSpeed = 1; // �v���C���[�̈ړ����x�y�i���e�B
    private bool canMove = true; // �v���C���[���������Ԃ��ǂ���
    private float playerMoveSpeed = 10f; // �v���C���[�̈ړ����x�W��
    private float hPlayerVelo; // �v���C���[�̍��E�̈ړ����x
    private float vPlayerVelo; // �v���C���[�̑O��i�㉺�j�̈ړ����x
    private bool guard = false; // �K�[�h�A�N�V���������s�ł����Ԃ��ǂ���
    private float stuckTime = 0f; // �s���s�\����
    private float invTime = 0f; // ���G����

    private void Start()
    {
        // ���W�b�h�{�f�B�[�R���|�[�l���g���擾����
        rigid = GetComponent<Rigidbody>();

        // �v���C���[�̐F���擾����
        playerMat = GetComponent<Renderer>().material;

        // �J�����̊p�x���擾
        camera = GameObject.Find("Main Camera").transform;

        // �Q�[���v���C�f�B���N�^�[���擾����
        director = GameObject.Find("Game Playing Director").GetComponent<GamePlayingDirector>();
    }

    private void Update()
    {
        /* ��J��Ԃł���΃y�i���e�B���󂯂� */
        if (director.FatigueSwitch)
        {
            damage = 2;
            penaSpeed = 4;
        }
        else
        {
            damage = 1;
            penaSpeed = 1;
        }

        // �C���^�t�F�[�X���g���A�v���C���[���������Ԃł���
        if (director.CanUseInterf && canMove)
        {


            hPlayerVelo = Input.GetAxis("Horizontal") * playerMoveSpeed / penaSpeed;
            vPlayerVelo = Input.GetAxis("Vertical") * playerMoveSpeed / penaSpeed;

            // E�L�[����͂����
            if (Input.GetKeyDown(KeyCode.E))
            {
                // �ړ����[�h��؂�ւ���
                director.ModeChange = !director.ModeChange;
            }

            // �ʏ��Ԃ�
            if (!director.FatigueSwitch)
            {
                // �X�y�[�X�L�[����͂����
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    // �K�[�h�A�N�V���������s�ł����Ԃɂ���
                    guard = true;
                }
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

            // �v���C���[���������Ԃɂ���
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

            // �v���C���[�̐F�𔒐F�ɕω�������i���̐F�ɖ߂��j
            playerMat.color = Color.white;
        }
    }

    private void FixedUpdate()
    {
        // �v���C���[�������Ԃł���
        if (canMove)
        {
            // �ړ����[�h���u�O��v�ł����
            if (!director.ModeChange)
            {
                // �v���C���[��O�㍶�E�Ɉړ�������
                NormalAct(0, 1);
            }
            // �ړ����[�h���u�㉺�v�ł����
            else
            {
                // �v���C���[���㉺���E�Ɉړ�������
                NormalAct(1, 0);
            }

            // �K�[�h�A�N�V���������s�ł����Ԃł���
            if (guard)
            {
                guard = false;

                // �C�͉񕜎��ԊO�ł����
                if (director.ChargeTime == 0f)
                {
                    // �K�[�h�A�N�V���������s������
                    GuardAct();

                    // �C�͉񕜎��Ԃ�ݒ肷��
                    director.ChargeTime = 3.0f;
                }
                // �C�͉񕜎��ԊO�łȂ����
                else
                {
                    // ��J��Ԃɂ���
                    director.FatigueSwitch = true;

                    // ��J��ԉ񐔂𑝂₷
                    director.FatigueCnt += 1;

                    // �C�͉񕜎��Ԃ�ݒ肷��
                    director.ChargeTime = 5.0f;
                }
            }
        }

        // �v���C���[�̊p�x���J�����̊p�x�ɍ��킹��
        rigid.rotation = camera.rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ���G���ԊO��
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

    private void NormalAct(int Y, int Z)
    {
        rigid.MovePosition(transform.position +
             transform.right * hPlayerVelo * Time.fixedDeltaTime +
             transform.up * vPlayerVelo * Time.fixedDeltaTime * Y +
             transform.forward * vPlayerVelo * Time.fixedDeltaTime * Z);
    }

    private void GuardAct()
    {
        // �v���C���[�̐F�����F�ɕω�������
        playerMat.color = Color.yellow;

        // �v���C���[�������Ȃ���Ԃɂ���
        canMove = false;

        // �s���s�\���Ԃ�ݒ肷��
        stuckTime = 1.0f;

        // ���G���Ԃ�ݒ肷��
        invTime = 1.0f;
    }

    private void KnockBack(int X, int Y, int Z)
    {
        rigid.AddForce(X, Y, Z, ForceMode.VelocityChange);

        // �v���C���[�̐F�����F�ɕω�������
        playerMat.color = Color.magenta;

        // �v���C���[�̗̑͂����炷
        director.NowPlayerLives -= damage;

        // �v���C���[�������Ȃ���Ԃɂ���
        canMove = false;

        // �s���s�\���Ԃ�ݒ肷��
        stuckTime = 2.0f;

        // ���G���Ԃ�ݒ肷��
        invTime = 3.0f;
    }
}
