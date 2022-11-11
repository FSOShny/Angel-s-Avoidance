using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticUnits
{
    private static bool startup = true; // ゲームの起動時かどうか
                                        // （プラットフォームの強制確認を1回だけにするための処理）

    public static bool Startup
    {
        get { return startup; }
        set { startup = value; }
    }

    private static bool smartPhone = false; // 現在のプラットフォームがスマホかどうか

    public static bool SmartPhone
    {
        get { return smartPhone; }
        set { smartPhone = value; }
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

    private static int maxPlayerLife = 5; // プレイヤーの体力最大値

    public static int MaxPlayerLife
    {
        get { return maxPlayerLife; }
        set { maxPlayerLife = value; }
    }
}
