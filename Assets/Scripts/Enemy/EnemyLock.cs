using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLock : EnemyBase {

    // プレイヤーかアバターを発見できる距離
    public float lockRange;

    // プレイヤーとアバターを識別
    public LayerMask layerMask;

    // EnemyLockがプレイヤーに触れて停止させるために必要
    private bool stateLock=false;

    private void Awake()
    {
        EnemySetUp();
    }

    private void FixedUpdate()
    {
        Move();

        Debug.DrawRay(transform.position, Vector3.left * lockRange);
        FindAvatar();
    }

    private void FindAvatar()
    {
        if (!stateLock)
        {
            // 前にプレイヤー又はアバターがあった場合、起動させられる。
            if (Physics2D.Raycast(transform.position, Vector2.left, lockRange, layerMask))
            {
                speedSelect = 5;
                SpeedSelector();
            }
        }
    }

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Sowrd")
        {
            DamageEnemy();
        }
    }

}
