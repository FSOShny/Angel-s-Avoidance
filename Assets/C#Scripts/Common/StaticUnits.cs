using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticUnits
{
    // �Q�[���̋N�������ǂ����i�v���b�g�t�H�[���̋����m�F��1�񂾂��ɂ��邽�߂̏����j
    private static bool startup = true;

    public static bool Startup
    {
        get { return startup; }
        set { startup = value; }
    }

    // ���݂̃v���b�g�t�H�[�����X�}�z���ǂ���
    private static bool smartPhone = false;

    public static bool SmartPhone
    {
        get { return smartPhone; }
        set { smartPhone = value; }
    }

    // �Q�[���̐�������
    private static float gameTimeLim = 45f;

    public static float GameTimeLim
    {
        get { return gameTimeLim; }
        set { gameTimeLim = value; }
    }

    // �G�l�~�[�̈ړ��ʌW��
    private static int enemyMoveSpeed = 8;

    public static int EnemyMoveSpeed
    {
        get { return enemyMoveSpeed; }
        set { enemyMoveSpeed = value; }
    }

    // �v���C���[�̗͍̑ő�l
    private static int maxPlayerLives = 5;

    public static int MaxPlayerLives
    {
        get { return maxPlayerLives; }
        set { maxPlayerLives = value; }
    }

    // �J�����̔��]�W��
    private static int reverse = 1;

    public static int Reverse
    {
        get { return reverse; }
        set { reverse = value; }
    }
}
