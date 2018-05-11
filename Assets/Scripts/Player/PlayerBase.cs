using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // アバターを発見できるようになる距離
    public float findRange;

    private void Start()
    {
        groundDistance = GetComponent<SpriteRenderer>().bounds.size.y / 2.0f;
        playerWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    protected void Move()
    {
        // 移動処理
        Vector3 nextPosition = transform.position;

        if (Input.GetKey(keyLeft))
        {
            nextPosition.x -= Time.deltaTime * moveSpeed;
        }
        else if (Input.GetKey(keyRight))
        {
            nextPosition.x += Time.deltaTime * moveSpeed;
        }
        transform.position = nextPosition;
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
            Instantiate(AttckJudge, new Vector3(pos.x + 1, pos.y), Quaternion.identity);
        }
    }
}
