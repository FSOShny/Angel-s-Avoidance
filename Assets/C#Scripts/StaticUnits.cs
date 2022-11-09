using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticUnits
{
    private static bool startup = true; // �Q�[���̋N�������ǂ���

    public static bool Startup
    {
        get { return startup; }
        set { startup = value; }
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

    private static float maxPlayerLife = 5.0f; // �v���C���[�̗͍̑ő�l

    public static float MaxPlayerLife
    {
        get { return maxPlayerLife; }
        set { maxPlayerLife = value; }
    }

    private static float gameTimeLim = 45f; // �Q�[���̐�������

    public static float GameTimeLim
    {
        get { return gameTimeLim; }
        set { gameTimeLim = value; }
    }
}
