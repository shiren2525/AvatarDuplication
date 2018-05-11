using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParasol : EnemyBase {

    // パラソルの状態変化に使用
    private bool stateParasol = true;

    // パラソルが左右に揺れる速度
    public float swaySpeed;

    // Walkerに切り替えるために必要
    public Sprite EnemyWakerSprite;

    // パラソルの動きを制御するために必要
    private int parasolMode = 0;

    // パラソルの左右移動を切り替える角度を設定
    public float parasolAngle;

    private void Awake()
    {
        EnemySetUp();
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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (stateParasol)
        {
            // パラソル状態である時に、地面に当たったらWalkerに変わる
            if (collision.gameObject.tag == "Ground")
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = EnemyWakerSprite;
                stateParasol = !stateParasol;
            }
        }
        if (collision.gameObject.tag == "Player")
        {
            DamageEnemy();
        }
        if (collision.gameObject.tag == "Avatar")
        {
            DamageEnemy();
        }
    }

    private void ParasolMove()
    {
        switch (parasolMode)
        {
            case 0:
                GetComponent<Rigidbody2D>().velocity = transform.right.normalized * swaySpeed;
                transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime);
                break;
            case 1:
                GetComponent<Rigidbody2D>().velocity = transform.right.normalized * -swaySpeed;
                transform.Rotate(new Vector3(0, 0, -45) * Time.deltaTime);
                break;
            case 2:
                GetComponent<Rigidbody2D>().velocity = transform.right.normalized * swaySpeed;
                transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime);
                break;
            default:
                break;
        }

        if (this.gameObject.transform.localEulerAngles.z >= parasolAngle && this.gameObject.transform.localEulerAngles.z <= parasolAngle + 10.0f)
        {
            // 右方向に60度回転したら、切り替える
            parasolMode = 1;
        }
        else if (this.gameObject.transform.localEulerAngles.z >= 350.0f - parasolAngle && this.gameObject.transform.localEulerAngles.z <= 360.0f - parasolAngle)
        {
            parasolMode = 2;
        }        
    }
}
