using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// シールドのサブクラス
public class EnemyShild : EnemyBase {
    
    // プレイヤーとアバターを認識するために必要
    public LayerMask layerMask;

    // プレイヤーとアバターにシールドを向けるようになる距離
    public float shildRange;

    private void Awake()
    {
        SetUp();
    }

    private void FixedUpdate()
    {
        Shild();
    }

    private void Shild()
    {
        Vector3 pos = transform.position;
        Vector3 scale = transform.localScale;
        Debug.DrawRay(transform.position, Vector2.left * shildRange);
        Debug.DrawRay(transform.position, Vector2.right * shildRange);

        // プレイヤーとアバターのいる方向によって、画像をフリップさせる
        if (Physics2D.Raycast(pos, Vector2.left, shildRange, layerMask))
        {
            scale.x = -0.5f;
        }
        else if(Physics2D.Raycast(pos, Vector2.right, shildRange, layerMask))
        {
            scale.x = 0.5f;
        }
        transform.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Sowrd")
        {
            if (Physics2D.Raycast(transform.position, Vector2.left, shildRange, layerMask) && Physics2D.Raycast(transform.position, Vector2.right, shildRange, layerMask))
            {
                // 左右両方にある場合のみダメージを受ける
                DamageEnemy();
            }
        }
    }

}
