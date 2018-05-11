using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// アバターのサブクラス
public class PlayerAvatar : PlayerBase {

    // アバターの状態を管理
    private bool stateAvatar = false;

    // アバターを起動させるために使用
    public LayerMask layerMask;

    // ステータス管理に使用
    [SerializeField] private GameObject PlayerStatus;

    // アバターの前後に発見の判定を設けるために必要
    private float avatarWidth;

    // EnemyLockにロックされているかどうかを管理
    private bool stateLock;
    
    private void Awake()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        SetUp();
        rig2D = GetComponent<Rigidbody2D>();
        avatarWidth = GetComponent<SpriteRenderer>().bounds.size.x;

        HP = 1;
    }

    private void FixedUpdate()
    {
        // オフ状態の時のみ動かす
        if (!stateAvatar)
        {
            FindAvatar();
        }
        
        if (stateLock || !stateAvatar)
        {
            // ロックされているか、オフ状態の場合リターン
            return;
        }

        Move();
        Attack();
        if (checkGround())
        {
            Jump();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!stateAvatar)
        {  
            // 起動さている場合のみダメージを受ける
            return;
        }
        // 敵と接触した場合にダメージ
        if (collision.gameObject.tag == "Enemy")
        {
            HP--;
            PlayerStatus.GetComponent<PlayerStatus>().AddDamageAvatar(1);
            if (HP <= 0)
            {
                Destroy(this.gameObject);
            }
        }
        if (collision.gameObject.tag == "EnemyLock")
        {
            // EnemyLockと接触したらロックされる
            stateLock = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyLock")
        {
            // EnemyLockが倒れたらロックが解除される
            stateLock = false;
        }
    }

    private void FindAvatar()
    {
        Vector3 offset = Vector3.zero;
        offset.x = avatarWidth / 2;
        Debug.DrawRay(transform.position - offset, Vector2.left * findRange);
        // 前にプレイヤー又はアバターがあった場合、起動させられる。
        if (Physics2D.Raycast(transform.position - offset, Vector2.left, findRange, layerMask))
        {
            if (Input.GetKeyDown(keyAction))
            {
                // オフ状態からアバター扱いにする
                this.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                PlayerStatus.GetComponent<PlayerStatus>().AddNumberAvatar();
                this.gameObject.layer = LayerMask.NameToLayer("Avatar");
                stateAvatar = true;
            }
        }
    }
}
