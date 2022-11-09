using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticUnits
{
    private static bool startup = true; // ゲームの起動時かどうか

    public static bool Startup
    {
        get { return startup; }
        set { startup = value; }
    }

    private static int direction = 1; // カメラの回転方向係数

    public static int Direction
    {
        get { return direction; }
        set { direction = value; }
    }

    private static int enemyMoveSpeed = 8; // エネミーの移動速度係数

    public static int EnemyMoveSpeed
    {
        get { return enemyMoveSpeed;}
        set { enemyMoveSpeed = value; }
    }

    private static float maxPlayerLife = 5.0f; // プレイヤーの体力最大値

    public static float MaxPlayerLife
    {
        get { return maxPlayerLife; }
        set { maxPlayerLife = value; }
    }

    private static float gameTimeLim = 45f; // ゲームの制限時間

    public static float GameTimeLim
    {
        get { return gameTimeLim; }
        set { gameTimeLim = value; }
    }
}
