using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの基底クラス
/// </summary>
public class PlayerBase : MonoBehaviour
{

    // 操作ボタン
    private KeyCode keyLeft = KeyCode.LeftArrow;
    private KeyCode keyRight = KeyCode.RightArrow;
    protected KeyCode keyAction = KeyCode.Z;
    protected KeyCode keyJump = KeyCode.Space;

    private string Horizontal = "Horizontal";
    protected string A = "A";
    protected string B = "B";

    public float moveSpeed;

    public float jumpForce;
    protected Rigidbody2D rig2D;
    protected SpriteRenderer spriteRenderer;

    public GameObject AttckJudge;   // 攻撃判定オブジェクト
    private int interval;           // 攻撃の後隙を管理するための変数
    const int ATTACK_INTERVAL = 20; // 攻撃の後隙の値

    // 自分のHP (Player と Avatarで同じものを使用)
    protected int HP;

    // 地面と接触しているかどうかの判定に使用
    private float playerWidth;
    private float groundDistance;
    [SerializeField] protected LayerMask groundMask;

    // 前にオフ状態のアバターがあるかどうかを判別するために使用
    public LayerMask layerMaskSword;

    // アバターを発見できるようになる距離(この距離にある時にアタックもできなくなる)
    protected float findRange = 0.1f;

    // 攻撃を出す距離
    public float attackRange;

    // FlipZoneで使用
    private bool flipMove;


    /// <summary>
    /// プレイヤーとアバターのAwakeで呼び出す
    /// </summary>
    protected void SetUp()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        groundDistance = spriteRenderer.bounds.size.y / 2.0f;
        playerWidth = spriteRenderer.bounds.size.x;
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    protected void Move()
    {
        Vector3 nextPosition = transform.position;

        if (Input.GetKey(keyLeft) || Input.GetAxis(Horizontal) < 0)
        {
            if (!flipMove)
            {
                spriteRenderer.flipX = true;
                nextPosition.x -= Time.deltaTime * moveSpeed;
            }
            else
            {
                spriteRenderer.flipX = false;
                nextPosition.x += Time.deltaTime * moveSpeed;
            }
        }
        else if (Input.GetKey(keyRight) || Input.GetAxis(Horizontal) > 0)
        {
            if (!flipMove)
            {
                spriteRenderer.flipX = false;
                nextPosition.x += Time.deltaTime * moveSpeed;
            }
            else
            {
                spriteRenderer.flipX = true;
                nextPosition.x -= Time.deltaTime * moveSpeed;
            }
        }
        transform.position = nextPosition;
    }

    /// <summary>
    /// ジャンプ
    /// </summary>
    protected virtual void Jump()
    {
        if (Input.GetKeyDown(keyJump) || Input.GetButtonDown(A))
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
        Debug.DrawRay(transform.position + offset, Vector2.right *findRange);
        if (Physics2D.Raycast(transform.position + offset, Vector2.right , findRange, layerMaskSword))
        {
            // 前にオフ状態のアバターがあった場合は、アタックしない。
            Debug.Log("前にアバターオフがあります。");
            return;
        }
        else if (Input.GetKeyDown(keyAction) || Input.GetButtonDown(B))
        {       
            AttackMain(pos);
        }
    }

    /// <summary>
    /// プレイヤーだけにSEを鳴らせたいのでオーバーライドする
    /// </summary>
    /// <param name="pos">自分の座標</param>
    protected virtual void AttackMain(Vector3 pos)
    {
        if (!spriteRenderer.flipX)
        {
            Instantiate(AttckJudge, new Vector3(pos.x + attackRange, pos.y), Quaternion.identity);
        }
        else
        {
            Instantiate(AttckJudge, new Vector3(pos.x - attackRange, pos.y), Quaternion.identity);
        }
        interval = 0;
    }

    /// <summary>
    /// FlipZoneに接触したときの操作反転
    /// </summary>
    public void FlipMove()
    {
        flipMove = !flipMove;
    }
}
