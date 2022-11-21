using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticUnits
{
    // ゲームの起動時かどうか（プラットフォームの強制確認を1回だけにするための処理）
    private static bool startup = true;

    public static bool Startup
    {
        get { return startup; }
        set { startup = value; }
    }

    // 現在のプラットフォームがスマホかどうか
    private static bool smartPhone = false;

    public static bool SmartPhone
    {
        get { return smartPhone; }
        set { smartPhone = value; }
    }

    // ゲームの制限時間
    private static float gameTimeLim = 45f;

    public static float GameTimeLim
    {
        get { return gameTimeLim; }
        set { gameTimeLim = value; }
    }

    // エネミーの移動量係数
    private static int enemyMoveSpeed = 8;

    public static int EnemyMoveSpeed
    {
        get { return enemyMoveSpeed; }
        set { enemyMoveSpeed = value; }
    }

    // プレイヤーの体力最大値
    private static int maxPlayerLives = 5;

    public static int MaxPlayerLives
    {
        get { return maxPlayerLives; }
        set { maxPlayerLives = value; }
    }

    // カメラの反転係数
    private static int reverse = 1;

    public static int Reverse
    {
        get { return reverse; }
        set { reverse = value; }
    }
}
