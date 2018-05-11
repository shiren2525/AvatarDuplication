using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// プレイヤーのサブクラス
public class Player : PlayerBase {

    // ステータス管理に使用
    [SerializeField] private GameObject PlayerStatus;

    // EnemyLockにロックされているか管理
    private bool stateLock;
    
    private void Awake()
    {
        SetUp();
        rig2D = GetComponent<Rigidbody2D>();
        HP = 3;
    }

    private void FixedUpdate ()
    {

        //Debug.DrawRay(transform.position, Vector3.right * findRange);
        if (!stateLock)
        {
            Move();
            if (checkGround())
            {
                Jump();
            }
            Attack();
        }
    }   
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 敵と接触した場合にダメージ
        if (collision.gameObject.tag == "Enemy")
        {
            HP--;
            PlayerStatus.GetComponent<PlayerStatus>().AddDamagePlayer(1);
            if (HP <= 0)
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
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyLock")
        {
            // EnemyLockが倒れたら解除される
            stateLock = !stateLock;
        }    
    }
}
