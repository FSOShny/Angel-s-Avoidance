using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    // �v���C���[�̈ړ��ʌW��
    public float playerMoveSpeed = 10f;

    // �v���C���[�I�[��
    [SerializeField] private GameObject playerAura;

    // �R���|�[�l���g�i���́A�A�j���[�V�����A�f�B���N�^�[�A�I�[�f�B�I�V�X�e���j
    private Rigidbody rigid;
    private Animator anim;
    private GamePlayingDirector director;
    private AudioSystem audioSystem;

    // �v���C���[�̃}�e���A��
    private Material playerMat;

    // �J�����̕ψ�
    private Transform came;

    // ���G����
    private float invTime = 0f;

    // �v���C���[�̔�e�W��
    private int damage = 1;

    // �v���C���[�̈ړ����x�y�i���e�B
    private int penaSpeed = 1;

    // �s���s�\����
    private float stuckTime = 0f;

    // �v���C���[�̍��E�̉����x
    private float hPlayerVelo = 0f;

    // �v���C���[�̑O��i�㉺�j�̉����x
    private float vPlayerVelo = 0f;

    // �v���C���[�̍��E�̈ړ���
    private float hPlayerMove;

    // �v���C���[�̑O��i�㉺�j�̈ړ���
    private float vPlayerMove;

    // �v���C���[���ړ��\���ǂ����̃t���O�ϐ�
    private bool canMove = true;
    public bool CanMove
    {
        get { return canMove; }
        set { canMove = value; }
    }

    // �E���{�^������͂��Ă��邩�ǂ����̃t���O�ϐ�
    private bool rightMove = false;
    public bool RightMove
    {
        get { return rightMove; }
        set { rightMove = value; }
    }

    // �����{�^������͂��Ă��邩�ǂ����̃t���O�ϐ�
    private bool leftMove = false;
    public bool LeftMove
    {
        get { return leftMove; }
        set { leftMove = value; }
    }

    // ����{�^������͂��Ă��邩�ǂ����̃t���O�ϐ�
    private bool upMove = false;
    public bool UpMove
    {
        get { return upMove; }
        set { upMove = value; }
    }

    // �����{�^������͂��Ă��邩�ǂ����̃t���O�ϐ�
    private bool downMove = false;
    public bool DownMove
    {
        get { return downMove; }
        set { downMove = value; }
    }

    // �K�[�h�A�N�V�������s���ǂ����̃t���O�ϐ�
    private bool guard = false;
    public bool Guard
    {
        get { return guard; }
        set { guard = value; }
    }

    private void Start()
    {
        // �e�R���|�[�l���g���擾����
        rigid = GetComponent<Rigidbody>();
        anim = GameObject.Find("MorroMan_Idle_MoCap").GetComponent<Animator>();
        director = GameObject.FindGameObjectWithTag("Director").GetComponent<GamePlayingDirector>();
        playerMat = GameObject.Find("MHuman").GetComponent<Renderer>().material;
        came = GameObject.Find("Camera").transform;
        audioSystem = GameObject.FindGameObjectWithTag("AudioSystem").GetComponent<AudioSystem>();

        // �v���C���[�I�[���𖳌��ɂ��A
        // �v���C���[�̃A�j���[�^�[���~�߂�
        playerAura.SetActive(false);
        anim.speed = 0f;
    }

    private void Update()
    {
        if (invTime > 0f)
        {
            /* ���G���Ԃ̂Ƃ���
               ���̎��Ԃ��o�߂�����   */

            invTime -= Time.deltaTime;
        }
        else if (invTime < 0f)
        {
            /* ���G���Ԃ��I�������Ƃ���
               ���Ԃ�����������i���ʂȏ������Ȃ����߁j */
            invTime = 0f;

            // �i�v���C���[����J��Ԃł���Ƃ��͖����j
            if (!director.FatigueSwitch)
            {
                // �v���C���[�̐F�����ɖ߂�
                playerMat.color = Color.white;
            }

            // �v���C���[�I�[���𖳌��ɂ���
            playerAura.SetActive(false);
        }

        if (director.ChargeTime > 0f)
        {
            /* �C�͉񕜎��Ԃ̂Ƃ���
               ���̎��Ԃ��o�߂�����   */

            director.ChargeTime -= Time.deltaTime;
        }
        else if (director.ChargeTime < 0f)
        {
            /* ���G���Ԃ��I�������Ƃ���
               ���Ԃ�����������i���ʂȏ������Ȃ����߁j */

            director.ChargeTime = 0f;

            // �y�i���e�B�𖳌��ɂ��A
            // �v���C���[��ʏ��Ԃɂ���
            damage = 1;
            penaSpeed = 1;
            director.FatigueSwitch = false;

            // �i���G���Ԃ̂Ƃ��͖����j
            if (invTime == 0)
            {
                // �v���C���[�̐F�����ɖ߂�
                playerMat.color = Color.white;
            }
        }

        if (stuckTime > 0f)
        {
            /* �s���s�\���Ԃ̂Ƃ���
               ���̎��Ԃ��o�߂����� */

            stuckTime -= Time.deltaTime;
        }
        else if (stuckTime < 0f)
        {
            /* �s���s�\���Ԃ��I�������Ƃ���
               ���Ԃ�����������i���ʂȏ������Ȃ����߁j */

            stuckTime = 0f;

            // �v���C���[�ɂ������Ă���͂��[���ɂ��A
            // �v���C���[���ړ��\�ɂ���
            rigid.velocity = Vector3.zero;
            canMove = true;
        }

        // �i�C���^�t�F�[�X���g�p�\�ł���A����
        //   �v���C���[���ړ��\�ł���Ƃ��͗L���j
        if (director.CanUseInterf && canMove)
        {
            if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) || rightMove)
            {
                /* �E�C���^�t�F�[�X�iD�L�[, �E���{�^���j����͂����
                   �E�։�������                                        */

                hPlayerVelo = 1.0f;
                vPlayerVelo = 0f;
            }
            else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) || leftMove)
            {
                /* ���C���^�t�F�[�X�iA�L�[, �����{�^���j����͂����
                   ���։�������                                        */

                hPlayerVelo = -1.0f;
                vPlayerVelo = 0f;
            }
            else if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) || upMove)
            {
                /* �O�i��j�C���^�t�F�[�X�iW�L�[, ����{�^���j����͂����
                   �O�i��j�։�������                                        */

                vPlayerVelo = 1.0f;
                hPlayerVelo = 0f;
            }
            else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) || downMove)
            {
                /* ��i���j�C���^�t�F�[�X�iS�L�[, �����{�^���j����͂����
                   ��i���j�։�������                                        */

                vPlayerVelo = -1.0f;
                hPlayerVelo = 0f;
            }
            else
            {
                /* �C���^�t�F�[�X����͂��Ȃ���
                   ��������                     */
                
                hPlayerVelo *= 0.5f;
                vPlayerVelo *= 0.5f;
            }

            // �v���C���[�̍��E�A�O��i�㉺�j�̈ړ��ʂ�ݒ肷��
            hPlayerMove = hPlayerVelo * playerMoveSpeed / penaSpeed;
            vPlayerMove = vPlayerVelo * playerMoveSpeed / penaSpeed;

            if (Input.GetKeyDown(KeyCode.E))
            {
                /* E�L�[����͂����
                   �ړ����[�h���`�F���W����
                   �i���ʉ��Đ�����j       */

                audioSystem.Music = 0;
                director.ModeChange = !director.ModeChange;
            }

            // �i�v���C���[����J��Ԃł���Ƃ��͖����j
            if (!director.FatigueSwitch)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    /* �X�y�[�X�L�[����͂����
                       �K�[�h�A�N�V�������s���i����������j   */
                    guard = true;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        // �i�v���C���[���ړ��\�ł���Ƃ��͗L���j
        if (canMove)
        {
            if (!director.ModeChange)
            {
                /* �ړ����[�h���u�O��v�ł����
                   �v���C���[��O�㍶�E�Ɉړ������� */

                NormalAct(0, 1);
            }
            else
            {
                /* �ړ����[�h���u�㉺�v�ł����
                   �v���C���[���㉺���E�Ɉړ������� */

                NormalAct(1, 0);
            }

            if (guard)
            {
                /* �K�[�h�A�N�V�������s�����������Ă����
                   �t���O���I�t�ɂ���                     */

                guard = false;

                if (director.ChargeTime == 0f)
                {
                    /* �C�͉񕜎��ԂłȂ��Ƃ���
                       �K�[�h�A�N�V�������s��
                       �i���ʉ��Đ�����j       */

                    audioSystem.Music = 6;
                    GuardAct();

                    // �C�͉񕜎��Ԃ�ݒ肷��
                    director.ChargeTime = 3.0f;
                }
                else
                {
                    /* �C�͉񕜎��Ԃ̂Ƃ���
                       �v���C���[���J��Ԃɂ���
                       �i���ʉ��Đ�����j         */

                    audioSystem.Music = 7;
                    director.FatigueSwitch = true;

                    // �y�i���e�B��L���ɂ��A
                    // �v���C���[�̐F�𐅐F�ɕω�������
                    damage = 2;
                    penaSpeed = 4;
                    playerMat.color = Color.cyan;

                    // ��J��ԉ񐔂𑝂₵�A
                    // �C�͉񕜎��Ԃ�ݒ肷��
                    director.FatigueCnt += 1;
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
        // �i���G���Ԃ̂Ƃ��͖����j
        if (invTime == 0)
        {
            // �]�[����G�l�~�[�ɏՓ˂���ƃv���C���[���m�b�N�o�b�N����
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
        // �v���C���[���ړ�������
        rigid.MovePosition(transform.position +
             transform.right * hPlayerMove * Time.fixedDeltaTime +
             transform.up * vPlayerMove * Time.fixedDeltaTime * Y +
             transform.forward * vPlayerMove * Time.fixedDeltaTime * Z
             );
    }

    private void GuardAct()
    {
        // �v���C���[���ړ��s�ɂ��A
        // �v���C���[�I�[����L���ɂ���
        // �i�v���C���[�I�[�����K�[�h���s���Ă����j
        canMove = false;
        playerAura.SetActive(true);

        // ���G���ԁA�s���s�\���Ԃ�ݒ肷��
        invTime = 1.0f;
        stuckTime = 1.0f;
    }

    private void KnockBack(int X, int Y, int Z)
    {
        // �m�b�N�o�b�N�̗͂������A
        // �v���C���[���ړ��s�ɂ���
        // �i���ʉ��Đ�����j
        audioSystem.Music = 8;
        rigid.AddForce(X, Y, Z, ForceMode.VelocityChange);
        canMove = false;

        // �i��J��Ԃł���Ƃ��͖����j
        if (!director.FatigueSwitch)
        {
            // �v���C���[�̐F�����F�ɕω�������
            playerMat.color = Color.magenta;
        }

        // �v���C���[�̗̑͂����炷
        director.NowPlayerLives -= damage;

        // ���G���ԁA�s���s�\���Ԃ�ݒ肷��
        invTime = 3.0f;
        stuckTime = 2.0f;
    }
}
