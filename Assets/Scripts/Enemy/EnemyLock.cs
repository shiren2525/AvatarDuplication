using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EnemyLockの派生クラス
/// </summary>
public class EnemyLock : EnemyBase {

    // プレイヤーかアバターを発見できる距離
    public float lockRange;

    // プレイヤーとアバターを識別するために必要
    public LayerMask layerMask;

    // EnemyLockがプレイヤーに触れて停止させるために必要
    private bool stateLock = false;

    private void Awake()
    {
        SetUp();
    }

    private void FixedUpdate()
    {
        Move();
        FindAvatar();
        Debug.DrawRay(transform.position, Vector3.left * lockRange);
    }

    /// <summary>
    /// 前にプレイヤーかアバターがある場合に速度を速くする
    /// </summary>
    private void FindAvatar()
    {
        if (!stateLock)
        {
            if (Physics2D.Raycast(transform.position, Vector2.left, lockRange, layerMask))
            {
                speedSelect = 5;
                SpeedSelector();
            }
        }
    }

    /// <summary>
    /// プレイヤーかアバターに接触した場合に、ロック状態になって停止する
    /// </summary>
    /// <param name="collision">相手のコライダー</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            speedSelect = 1;
            SpeedSelector();
            stateLock = true;
        }
        if (collision.gameObject.tag == "Avatar")
        {
            speedSelect = 1;
            SpeedSelector();
            stateLock = true;
        }
    }

    /// <summary>
    /// ソードと接触判定
    /// </summary>
    /// <param name="collision">相手のコライダー</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Sowrd")
        {
            Damage();
        }
    }

}
