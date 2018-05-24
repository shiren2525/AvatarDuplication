using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EnemyParasolの派生クラス
/// </summary>
public class EnemyParasol : EnemyBase {

    // パラソルの状態変化に使用
    private bool stateParasol = true;

    // パラソルが左右に揺れる速度
    public float swaySpeed;

    // Walkerに切り替えるために必要
    public Sprite EnemyWakerSprite;

    // パラソルの揺れを制御するために必要
    private bool swayCourse;

    // パラソルの左右移動を切り替える角度を設定
    public float parasolAngle;

    private void Awake()
    {
        SetUp();
    }

    private void FixedUpdate()
    {
        if (stateParasol)
        {
            ParasolMove();
        }
        else if (!stateParasol)
        {
            Move();
        }
    }

    /// <summary>
    /// 接触判定
    /// </summary>
    /// <param name="collision">相手のコライダー</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (stateParasol)
        {
            if (collision.gameObject.tag == "Ground")
            {
                // パラソル状態である時に、地面に当たったらWalkerに変わる
                this.gameObject.GetComponent<SpriteRenderer>().sprite = EnemyWakerSprite;
                stateParasol = !stateParasol;
            }
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
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Sowrd")
        {
            Damage();
        }
    }

    // パラソルの挙動を制御
    private void ParasolMove()
    {
        if (swayCourse)
        {
            GetComponent<Rigidbody2D>().velocity = transform.right.normalized * swaySpeed;
            transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime);
        }
        else if (!swayCourse)
        {
            GetComponent<Rigidbody2D>().velocity = transform.right.normalized * -swaySpeed;
            transform.Rotate(new Vector3(0, 0, -45) * Time.deltaTime);
        }

        if (this.gameObject.transform.localEulerAngles.z >= parasolAngle && this.gameObject.transform.localEulerAngles.z <= parasolAngle + 10.0f)
        {
            // 右方向に60度回転したら、切り替える
            swayCourse = !swayCourse;
        }
        else if (this.gameObject.transform.localEulerAngles.z >= 350.0f - parasolAngle && this.gameObject.transform.localEulerAngles.z <= 360.0f - parasolAngle)
        {
            swayCourse = !swayCourse;
        }        
    }
}
