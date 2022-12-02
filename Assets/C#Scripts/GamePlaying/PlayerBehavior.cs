using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float playerMoveSpeed = 10f; // �v���C���[�̈ړ��ʌW��

    [SerializeField] private GameObject playerAura;

    private Rigidbody rigid;
    private Animator anim;
    private Material playerMat;
    private Transform came;
    private GamePlayingDirector director;
    private float invTime = 0f; // ���G����
    private int damage = 1; // �v���C���[�̔�e�W��
    private int penaSpeed = 1; // �v���C���[�̈ړ����x�y�i���e�B
    private float stuckTime = 0f; // �s���s�\����
    private float hPlayerVelo = 0f; // �v���C���[�̍��E�̉����x
    private float vPlayerVelo = 0f; // �v���C���[�̑O��i�㉺�j�̉����x
    private float hPlayerMove; // �v���C���[�̍��E�̈ړ���
    private float vPlayerMove; // �v���C���[�̑O��i�㉺�j�̈ړ���

    // �v���C���[���������Ԃ��ǂ���
    private bool canMove = true;

    public bool CanMove
    {
        get { return canMove; }
        set { canMove = value; }
    }

    // �E���{�^������͂��Ă��邩�ǂ���
    private bool rightMove = false;

    public bool RightMove
    {
        get { return rightMove; }
        set { rightMove = value; }
    }

    // �����{�^������͂��Ă��邩�ǂ���
    private bool leftMove = false;

    public bool LeftMove
    {
        get { return leftMove; }
        set { leftMove = value; }
    }

    // ����{�^������͂��Ă��邩�ǂ���
    private bool upMove = false;

    public bool UpMove
    {
        get { return upMove; }
        set { upMove = value; }
    }

    // �����{�^������͂��Ă��邩�ǂ���
    private bool downMove = false;

    public bool DownMove
    {
        get { return downMove; }
        set { downMove = value; }
    }

    // �K�[�h�A�N�V���������s�ł����Ԃ��ǂ���
    private bool guard = false;

    public bool Guard
    {
        get { return guard; }
        set { guard = value; }
    }

    private void Start()
    {
        // ���W�b�h�{�f�B�[�R���|�[�l���g���擾����
        rigid = GetComponent<Rigidbody>();

        // �v���C���[�̃A�j���[�^�[���擾����
        anim = GameObject.Find("MorroMan_Idle_MoCap").GetComponent<Animator>();

        // �v���C���[�̐F���擾����
        playerMat = GameObject.Find("MHuman").GetComponent<Renderer>().material;

        // �J�����̊p�x���擾
        came = GameObject.Find("Camera").transform;

        // �Q�[���v���C�f�B���N�^�[���擾����
        director = GameObject.FindGameObjectWithTag("Director").GetComponent<GamePlayingDirector>();

        // �v���C���[�I�[���𖳌��ɂ���
        playerAura.SetActive(false);

        // �v���C���[�̃A�j���[�^�[���~�߂�
        anim.speed = 0f;
    }

    private void Update()
    {
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

            // ��J��ԂłȂ����
            if (!director.FatigueSwitch)
            {
                // �v���C���[�̐F�����ɖ߂�
                playerMat.color = Color.white;
            }

            // �v���C���[�o���A�𖳌��ɂ���
            playerAura.SetActive(false);
        }

        // �C�͉񕜎��Ԓ���
        if (director.ChargeTime > 0f)
        {
            // ���Ԃ��o�߂�����
            director.ChargeTime -= Time.deltaTime;
        }
        // �C�͉񕜎��ԏI������
        else if (director.ChargeTime < 0f)
        {
            // ���Ԃ�����������i����ȏ����̂��߁j
            director.ChargeTime = 0f;

            // �y�i���e�B�𖳌��ɂ���
            damage = 1;
            penaSpeed = 1;

            // �ʏ��Ԃɂ���
            director.FatigueSwitch = false;

            // ���G���ԊO�ł����
            if (invTime == 0)
            {
                // �v���C���[�̐F�����ɖ߂�
                playerMat.color = Color.white;
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

        // �C���^�t�F�[�X���g�����Ԃł���A�v���C���[���������Ԃ�
        if (director.CanUseInterf && canMove)
        {
            // �E�C���^�t�F�[�X�iD�L�[, �E���{�^���j����͂����
            if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) || rightMove)
            {
                // �E�։�������
                hPlayerVelo = 1.0f;
                vPlayerVelo = 0f;
            }
            // ���C���^�t�F�[�X�iA�L�[, �����{�^���j����͂����
            else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) || leftMove)
            {
                // ���։�������
                hPlayerVelo = -1.0f;
                vPlayerVelo = 0f;
            }
            // �O�i��j�C���^�t�F�[�X�iW�L�[, ����{�^���j����͂����
            else if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) || upMove)
            {
                // �O�i��j�։�������
                vPlayerVelo = 1.0f;
                hPlayerVelo = 0f;
            }
            // ��i���j�C���^�t�F�[�X�iS�L�[, �����{�^���j����͂����
            else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) || downMove)
            {
                // ��i���j�։�������
                vPlayerVelo = -1.0f;
                hPlayerVelo = 0f;
            }
            // �C���^�t�F�[�X����͂��Ȃ���
            else
            {
                // ��������
                hPlayerVelo *= 0.5f;
                vPlayerVelo *= 0.5f;
            }

            // �v���C���[�̍��E�̈ړ��ʂ����߂�
            hPlayerMove = hPlayerVelo * playerMoveSpeed / penaSpeed;

            // �v���C���[�̑O��i�㉺�j�̈ړ��ʂ����߂�
            vPlayerMove = vPlayerVelo * playerMoveSpeed / penaSpeed;

            // E�L�[����͂����
            if (Input.GetKeyDown(KeyCode.E))
            {
                // �ړ����[�h���`�F���W����
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
                    // �K�[�h�A�N�V���������s����
                    GuardAct();

                    // �C�͉񕜎��Ԃ�ݒ肷��
                    director.ChargeTime = 3.0f;
                }
                // �C�͉񕜎��ԊO�łȂ����
                else
                {
                    // ��J��Ԃɂ���
                    director.FatigueSwitch = true;

                    // �y�i���e�B��L���ɂ���
                    damage = 2;
                    penaSpeed = 4;

                    // �v���C���[�̐F�𐅐F�ɕω�������
                    playerMat.color = Color.cyan;

                    // ��J��ԉ񐔂𑝂₷
                    director.FatigueCnt += 1;

                    // �C�͉񕜎��Ԃ�ݒ肷��
                    director.ChargeTime = 5.0f;
                }
            }
        }

        // �v���C���[�̊p�x���J�����̊p�x�ɍ��킹��
        rigid.rotation = came.rotation;

        // �v���C���[���]�[������o�Ȃ��悤�ɂ���
        rigid.position = new(
            Mathf.Clamp(rigid.position.x, -13f, 13f), 
            Mathf.Clamp(rigid.position.y, 1.0f, 27f), 
            Mathf.Clamp(rigid.position.z, -13f, 13f)
            );
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
             transform.right * hPlayerMove * Time.fixedDeltaTime +
             transform.up * vPlayerMove * Time.fixedDeltaTime * Y +
             transform.forward * vPlayerMove * Time.fixedDeltaTime * Z);
    }

    private void GuardAct()
    {
        // �v���C���[�������Ȃ���Ԃɂ���
        canMove = false;

        // �v���C���[�I�[����L���ɂ���
        playerAura.SetActive(true);

        // ���G���Ԃ�ݒ肷��
        invTime = 1.0f;

        // �s���s�\���Ԃ�ݒ肷��
        stuckTime = 1.0f;
    }

    private void KnockBack(int X, int Y, int Z)
    {
        rigid.AddForce(X, Y, Z, ForceMode.VelocityChange);

        // �v���C���[�������Ȃ���Ԃɂ���
        canMove = false;

        // ��J��ԂłȂ����
        if (!director.FatigueSwitch)
        {
            // �v���C���[�̐F�����F�ɕω�������
            playerMat.color = Color.magenta;
        }

        // �v���C���[�̗̑͂����炷
        director.NowPlayerLives -= damage;

        // ���G���Ԃ�ݒ肷��
        invTime = 3.0f;

        // �s���s�\���Ԃ�ݒ肷��
        stuckTime = 2.0f;
    }
}
