using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの派生クラス
/// </summary>
public class Player : PlayerBase {

    // ステータス管理に使用
    [SerializeField] private GameObject PlayerStatus;

    // EnemyLockにロックされているか管理
    private bool stateLock;

    // ゲーム開始時にのみ表示するアイコン
    [SerializeField] GameObject iconPlayer;
    private float destroyTime = 10.0f;

    private void Awake()
    {
        SetUp();
        rig2D = GetComponent<Rigidbody2D>();
        HP = 3;
        Destroy(iconPlayer, destroyTime);
    }

    private void FixedUpdate ()
    {
        //Debug.DrawRay(transform.position, Vector3.right * findRange);
        if (!stateLock)
        {
            Move();
            if (CheckGround())
            {
                Jump();
            }
            Attack();
        }
    }   
    
    /// <summary>
    /// 敵との接触判定
    /// </summary>
    /// <param name="collision">相手のコライダー</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 敵と接触した場合にダメージ
        if (collision.gameObject.tag == "Enemy")
        {
            HP--;
            PlayerStatus.GetComponent<PlayerStatus>().AddDamagePlayer(1);
            if (IsDeath())
            {
                Destroy(this.gameObject);
            }
        }

        if (collision.gameObject.tag == "EnemyLock")
        {
            // EnemyLockと接触したらロックされる
            stateLock = !stateLock;
        }
    }

    /// <summary>
    /// ロックを解除する判定
    /// </summary>
    /// <param name="collision">相手のコライダー</param>
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyLock")
        {
            // EnemyLockが倒れたら解除される
            stateLock = !stateLock;
        }    
    }
    
    /// <summary>
    /// やられているか確認
    /// </summary>
    /// <returns>やられているかどうか</returns>
    private bool IsDeath()
    {
        return HP <= 0;
    }
}
