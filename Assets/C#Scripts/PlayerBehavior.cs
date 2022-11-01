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
    public float invTime;

    // Start is called before the first frame update
    private void Start()
    {
        // �J�����̊p�x���擾
        camera = Camera.main;

        // ���W�b�h�{�f�B�[�R���|�[�l���g���擾����
        rigid = GetComponent<Rigidbody>();
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

        if (invTime > 0)
        {
            invTime -= Time.deltaTime;
            Debug.Log(invTime);
        }
        else if (invTime < 0)
        {
            rigid.velocity = Vector3.zero;
            moveCan = true;
        }
    }

    private void FixedUpdate()
    {
        if (moveCan)
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
        if (collision.gameObject.name == "Front")
        {
            rigid.AddForce(Vector3.back, ForceMode.Impulse);
        }
        else if (collision.gameObject.name == "Left")
        {
            rigid.AddForce(Vector3.right, ForceMode.Impulse);
        }
        else if (collision.gameObject.name == "Back")
        {
            rigid.AddForce(Vector3.forward, ForceMode.Impulse);
        }
        else if (collision.gameObject.name == "Right")
        {
            rigid.AddForce(Vector3.left, ForceMode.Impulse);
        }
        else if (collision.gameObject.name == "Top")
        {
            rigid.AddForce(Vector3.down, ForceMode.Impulse);
        }

        invTime = 3;
        moveCan = false;
    }
}