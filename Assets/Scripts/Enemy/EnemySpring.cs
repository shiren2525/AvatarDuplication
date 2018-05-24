using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EnemySpringの派生クラス
/// </summary>
public class EnemySpring : EnemyBase {

    public float jumpForce;

    // ジャンプさせるために必要
    private Rigidbody2D rig2D;

    private void Awake()
    {
        rig2D = GetComponent<Rigidbody2D>();
        SetUp();
    }

    private void FixedUpdate()
    {
       Move();
    }

    /// <summary>
    /// 接触判定
    /// </summary>
    /// <param name="collision">相手のコライダー</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            // 地面に当たるたびに跳ねる
           rig2D.AddForce(Vector2.up * jumpForce);            
        }
        if (collision.gameObject.tag == "Player")
        {
            Damage();
        }
        if (collision.gameObject.tag == "Avatar")
        {
            Damage();
        }
    }

    /// <summary>
    /// ソードとの接触判定
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
