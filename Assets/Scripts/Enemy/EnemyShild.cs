using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EnemyShildの派生クラス
/// </summary>
public class EnemyShild : EnemyBase {
    
    // プレイヤーとアバターを認識するために必要
    public LayerMask layerMask;

    // 画像を反転させるときにスケールが変わらないようにするために必要
    private Vector3 scale;

    // プレイヤーとアバターにシールドを向けるようになる距離
    public float shildRange;

    private void Awake()
    {
        SetUp();
        scale = transform.localScale;
    }

    private void FixedUpdate()
    {
        Shild();
    }

    /// <summary>
    /// プレイヤーとアバターのいる方向によって、画像をフリップさせる
    /// </summary>
    private void Shild()
    {
        Vector3 pos = transform.position;
        Vector3 nextScale = transform.localScale;
        Debug.DrawRay(transform.position, Vector2.left * shildRange);
        Debug.DrawRay(transform.position, Vector2.right * shildRange);
        
        if (Physics2D.Raycast(pos, Vector2.left, shildRange, layerMask))
        {
            nextScale.x = -scale.x;
        }
        else if(Physics2D.Raycast(pos, Vector2.right, shildRange, layerMask))
        {
            nextScale.x = scale.x;
        }
        transform.localScale = nextScale;
    }

    /// <summary>
    /// 左右両方にある場合のみダメージを受ける
    /// </summary>
    /// <param name="collision">相手のコライダー</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Sowrd")
        {
            if (Physics2D.Raycast(transform.position, Vector2.left, shildRange, layerMask) && Physics2D.Raycast(transform.position, Vector2.right, shildRange, layerMask))
            {
                Damage();
            }
        }
    }

}
