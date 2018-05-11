using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// プレイヤーのスーパークラス
public class PlayerBase : MonoBehaviour {

    // 操作ボタン
    private KeyCode keyLeft = KeyCode.LeftArrow;
    private KeyCode keyRight = KeyCode.RightArrow;
    protected KeyCode keyAction = KeyCode.Z;
    private KeyCode keyJump = KeyCode.Space;

    // 移動速度
    public float moveSpeed;

    // ジャンプ力
    public float jumpForce;
    protected Rigidbody2D rig2D;

    // 攻撃判定オブジェクト
    public GameObject AttckJudge;

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

    // プレイヤーとアバターのAwakeで呼び出す
    protected void SetUp()
    {
        groundDistance = GetComponent<SpriteRenderer>().bounds.size.y / 2.0f;
        playerWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    protected void Move()
    {
        Vector3 nextPosition = transform.position;
        Vector3 scale = transform.localScale;

        if (Input.GetKey(keyLeft))
        {
            if (!flipMove)
            {
                scale.x = -0.3f;
                nextPosition.x -= Time.deltaTime * moveSpeed;
            }
            else
            {
                scale.x = 0.3f;
                nextPosition.x += Time.deltaTime * moveSpeed;

            }
        }
        else if (Input.GetKey(keyRight))
        {
            if (!flipMove)
            {
                scale.x = 0.3f;
                nextPosition.x += Time.deltaTime * moveSpeed;

            }
            else
            {
                scale.x = -0.3f;
                nextPosition.x -= Time.deltaTime * moveSpeed;
            }
        }
        transform.position = nextPosition;
        transform.localScale = scale;
    }

    protected void Jump()
    {
        // ジャンプ
        if (Input.GetKeyDown(keyJump))
        {
            rig2D.AddForce(Vector2.up * jumpForce);
        }
    }

    // 地面と接地しているかチェックするメソッド
    protected bool checkGround()
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

        //Debug.DrawRay(transform.position, Vector3.down * groundDistance);

        return false;
    }

    protected void Attack()
    {
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
            Instantiate(AttckJudge, new Vector3(pos.x + attackRange, pos.y), Quaternion.identity);
        }
    }

    public void FlipMove()
    {
        flipMove = !flipMove;
    }
}
