using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemyの基底クラス
/// </summary>
public class EnemyBase : MonoBehaviour {

    // Unity上で速度を選択できるようにするための変数
    public int speedSelect;

    // エネミーの速度を設定    
    const float SPEED_STOP = 0;
    const float SPEED_VERYSLOW = 0.2f;
    const float SPEED_SLOW = 0.5f;
    const float SPEED_NORMAL = 1.0f;
    const float SPEED_FAST = 2.0f;
    
    // 選択した速度を格納するために必要
    protected float moveSpeed;

    // エネミーが向いている方向を管理するために使用
    protected bool enemyControl = true;

    // Unity上でHPを選択
    public int HPSelect;

    // エネミーのHPを管理
    protected int HPEnemey;
    
    /// <summary>
    /// 各EnemyのAwakeで呼び出して、HPとスピードを設定するため
    /// </summary>
    protected void SetUp()
    {
        HPSelector();
        SpeedSelector();
    }

    /// <summary>
    /// インスペクターで設定したHPを代入
    /// </summary>
    protected void HPSelector()
    {
        HPEnemey = HPSelect;
    }

    /// <summary>
    /// インスペクターで選んだスピードを代入
    /// </summary>
    protected void SpeedSelector()
    {
        switch (speedSelect)
        {
            case 1:
                moveSpeed = SPEED_STOP;
                break;
            case 2:
                moveSpeed = SPEED_VERYSLOW;
                break;
            case 3:
                moveSpeed = SPEED_SLOW;
                break;
            case 4:
                moveSpeed = SPEED_NORMAL;
                break;
            case 5:
                moveSpeed = SPEED_FAST;
                break;
        }
    }

    /// <summary>
    /// 移動
    /// </summary>
    protected void Move()
    {
        Vector3 nextPosition = transform.position;
        if (enemyControl)
        {
            nextPosition.x -= Time.deltaTime * moveSpeed;
        }
        else if (!enemyControl)
        {
            nextPosition.x += Time.deltaTime * moveSpeed;
        }
        transform.position = nextPosition;
    }

    /// <summary>
    /// ダメージを受けた時にHPを減らす
    /// </summary>
    protected void Damage()
    {
        HPEnemey--;
        if (IsDeath())
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// やられているか判定
    /// </summary>
    /// <returns>やられているかどうか</returns>
    private bool IsDeath()
    {
        return HPEnemey <= 0;
    }

}
