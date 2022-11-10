using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticUnits
{
    private static bool startup = true; // �Q�[���̋N�������ǂ���
                                        // �i�v���b�g�t�H�[���̋����m�F��1�񂾂��ɂ��邽�߂̏����j

    public static bool Startup
    {
        get { return startup; }
        set { startup = value; }
    }

    private static bool smartPhone = false; // ���݂̃v���b�g�t�H�[�����X�}�z���ǂ���

    public static bool SmartPhone
    {
        get { return smartPhone; }
        set { smartPhone = value; }
    }

    private static int direction = 1; // �J�����̉�]�����W��

    public static int Direction
    {
        get { return direction; }
        set { direction = value; }
    }

    private static int enemyMoveSpeed = 8; // �G�l�~�[�̈ړ����x�W��

    public static int EnemyMoveSpeed
    {
        get { return enemyMoveSpeed;}
        set { enemyMoveSpeed = value; }
    }

    private static int maxPlayerLife = 5; // �v���C���[�̗͍̑ő�l

    public static int MaxPlayerLife
    {
        get { return maxPlayerLife; }
        set { maxPlayerLife = value; }
    }
}
