using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public int pow = 5; // �͂̑傫��

    private Rigidbody rigid;
    private bool firstBound = true;

    private void Start()
    {
        // ���W�b�h�{�f�B�[�R���|�[�l���g���擾����
        rigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (firstBound) // �]�[���ɏՓ˂���܂ł�
        {
            // �����I�ɓ���
            rigid.AddForce(pow, pow, pow);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �]�[���ɏՓ˂���ƃ_�[�N�{�[�����o�E���h����
        if (collision.gameObject.name == "Bottom")
        {
            Bounding(0, pow, 0);
        }
        else if (collision.gameObject.name == "Front")
        {
            Bounding(0, 0, -pow);
        }
        else if (collision.gameObject.name == "Left")
        {
            Bounding(pow, 0, 0);
        }
        else if (collision.gameObject.name == "Back")
        {
            Bounding(0, 0, pow);
        }
        else if (collision.gameObject.name == "Right")
        {
            Bounding(-pow, 0, 0);
        }
        else if (collision.gameObject.name == "Top")
        {
            Bounding(0, -pow, 0);
        }
    }

    public void Bounding(int X, int Y, int Z)
    {
        rigid.AddForce(X, Y, Z, ForceMode.Impulse);

        if (firstBound)
        {
            firstBound = false;
        }
    }
}
