using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EnemyWalker,Runner,Bigに使う派生クラス
/// </summary>
public class Enemy : EnemyBase {

    private void Awake()
    {
        SetUp();
    }

    private void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// プレイヤーとアバターとの接触判定
    /// </summary>
    /// <param name="collision">相手のコライダー</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
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
