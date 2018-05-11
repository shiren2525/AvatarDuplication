using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour {

    // Unity上で速度を選択できるようにするための変数
    public int speedSelect;

    // エネミーの速度を設定    
    const float speedStop = 0;
    const float speedVerySlow = 0.2f;
    const float speedSlow = 0.5f;
    const float speedNormal = 1.0f;
    const float speedFast = 2.0f;



    // 選択した速度を格納するために必要
    protected float moveSpeed;

    // エネミーが向いている方向を管理するために使用
    protected bool enemyControl = true;

    // Unity上でHPを選択
    public int HPSelect;

    // エネミーのHPを管理
    protected int HPEnemey;
    
    // 各EnemyのAwakeで呼び出して、HPとスピードを設定するメソッド
    protected void SetUp()
    {
        HPSelector();
        SpeedSelector();
    }

    protected void HPSelector()
    {
        // インスペクターで設定したHPを代入
        HPEnemey = HPSelect;
    }

    protected void SpeedSelector()
    {
        // インスペクターで選んだスピードを代入
        switch (speedSelect)
        {
            case 1:
                moveSpeed = speedStop;
                break;
            case 2:
                moveSpeed = speedVerySlow;
                break;
            case 3:
                moveSpeed = speedSlow;
                break;
            case 4:
                moveSpeed = speedNormal;
                break;
            case 5:
                moveSpeed = speedFast;
                break;
            default:
                break;
        }
    }

    protected void Move()
    {
        // 移動処理
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

    protected void DamageEnemy()
    {
        HPEnemey--;
        if (IsDeath())
        {
            Destroy(this.gameObject);
        }
    }

    private bool IsDeath()
    {
        return HPEnemey <= 0;
    }

}
