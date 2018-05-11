using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        rig2D = GetComponent<Rigidbody2D>();
        avatarWidth = GetComponent<SpriteRenderer>().bounds.size.x;

        HP = 1;
    }

    private void Update()
    {
        if (stateLock)
            return;

        if (!stateAvatar)
        {
            // 起動している場合のみ動作させる
            FindAvatar();
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
            stateLock = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyLock")
        {
            stateLock = false;
        }
    }

    private void FindAvatar()
    {
        Vector3 offset = Vector3.zero;
        offset.x = avatarWidth / 2;
        // 前にプレイヤー又はアバターがあった場合、起動させられる。
        if (Physics2D.Raycast(transform.position - offset, Vector2.left, 0.1f, layerMask))
        {
            if (Input.GetKeyDown(keyAction))
            {
                PlayerStatus.GetComponent<PlayerStatus>().AddNumberAvatar();
                this.gameObject.layer = LayerMask.NameToLayer("Avatar");
                stateAvatar = true;
            }
        }
    }
}
