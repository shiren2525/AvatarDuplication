using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの基底クラス
/// </summary>
public class PlayerBase : MonoBehaviour {

    // 操作ボタン
    private KeyCode keyLeft = KeyCode.LeftArrow;
    private KeyCode keyRight = KeyCode.RightArrow;
    protected KeyCode keyAction = KeyCode.Z;
    private KeyCode keyJump = KeyCode.Space;
    
    public float moveSpeed;

    public float jumpForce;
    protected Rigidbody2D rig2D;

    // 攻撃判定オブジェクト
    public GameObject AttckJudge;

    // 攻撃の後隙
    private int interval;

    // 攻撃の後隙
    const int ATTACK_INTERVAL = 20;

    // 自分のHP (Player と Avatarで同じものを使用)
    protected int HP;

    // 地面と接触しているかどうかの判定に使用
    private float playerWidth;
    private float groundDistance;
    [SerializeField] protected LayerMask groundMask;

    // 前にオフ状態のアバターがあるかどうかを判別するために使用
    public LayerMask layerMaskSowrd;

    // アバターを発見できるようになる距離(この距離にある時にアタックもできなくなる)
    protected float findRange = 0.1f;

    // 攻撃を出す距離
    public float attackRange;

    // FlipZoneで使用
    private bool flipMove;

    // プレイヤーの向いている方向を管理
    private bool rightDirection;

    // プレイヤーとアバターの初期スケールを格納するために必要
    private　Vector3 scale;

    /// <summary>
    /// プレイヤーとアバターのAwakeで呼び出す
    /// </summary>
    protected void SetUp()
    {
        groundDistance = GetComponent<SpriteRenderer>().bounds.size.y / 2.0f;
        playerWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        scale = transform.localScale;
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    protected void Move()
    {
        Vector3 nextPosition = transform.position;
        Vector3 nextScale = transform.localScale;

        if (Input.GetKey(keyLeft))
        {
            if (!flipMove)
            {
                nextScale.x = -scale.x;
                rightDirection = false;
                nextPosition.x -= Time.deltaTime * moveSpeed;
            }
            else
            {
                nextScale.x = scale.x;
                rightDirection = true;
                nextPosition.x += Time.deltaTime * moveSpeed;

            }
        }
        else if (Input.GetKey(keyRight))
        {
            if (!flipMove)
            {
                nextScale.x = scale.x;
                rightDirection = true;
                nextPosition.x += Time.deltaTime * moveSpeed;

            }
            else
            {
                nextScale.x = -scale.x;
                rightDirection = false;
                nextPosition.x -= Time.deltaTime * moveSpeed;
            }
        }
        transform.position = nextPosition;
        transform.localScale = nextScale;
    }

    /// <summary>
    /// ジャンプ
    /// </summary>
    protected void Jump()
    {
        if (Input.GetKeyDown(keyJump))
        {
            rig2D.AddForce(Vector2.up * jumpForce);
        }
    }

    /// <summary>
    /// 地面と接触しているか確認
    /// </summary>
    /// <returns>ジャンプできるかどうか</returns>
    protected bool CheckGround()
    {
        Vector3 offset = Vector3.zero;
        offset.x = playerWidth / 2.0f; // 画像の両端にも判定を持たせるために使用
        float footDistance = 0.05f; // 画像から下に判定を設けるために必要

        if (Physics2D.Raycast(transform.position, Vector2.down, groundDistance + footDistance, groundMask))
            return true;
        else if (Physics2D.Raycast(transform.position + offset, Vector2.down, groundDistance + footDistance, groundMask))
            return true;
        else if (Physics2D.Raycast(transform.position - offset, Vector2.down, groundDistance + footDistance, groundMask))
            return true;
        
        return false;
    }

    /// <summary>
    /// 攻撃
    /// </summary>
    protected void Attack()
    {
        interval++;
        if (interval < ATTACK_INTERVAL)
        {
            // 攻撃に間隔を設けるため
            return;
        }
        Vector3 pos = transform.position;
        // 攻撃判定を出す
        Vector3 offset = Vector3.zero;
        offset.x = playerWidth / 2;

        if (Physics2D.Raycast(transform.position + offset, Vector2.left, -findRange, layerMaskSowrd))
        {
            // 前にオフ状態のアバターがあった場合は、アタックしない。
            return;
        }
        else if (Input.GetKeyDown(keyAction))
        {
            if (rightDirection)
            {
                Instantiate(AttckJudge, new Vector3(pos.x + attackRange, pos.y), Quaternion.identity);
            }
            else
            {
                Instantiate(AttckJudge, new Vector3(pos.x - attackRange, pos.y), Quaternion.identity);
            }
            interval = 0;
        }
    }

    /// <summary>
    /// FlipZoneに接触したときの操作反転
    /// </summary>
    public void FlipMove()
    {
        flipMove = !flipMove;
    }
}
