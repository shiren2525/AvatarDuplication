using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// アバターの派生クラス
/// </summary>
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

    // 画面外に出た時にメソッドを動かすため
    private bool isVisible;

    // 36fで1カウントを実装するための変数
    private int count;

    // 画面に表示する変数
    private int deathCount = 10;

    // カウントの初期値
    const int INIT_DEATH_COUNT = 10;

    // 画面外に出た時のカウントを表示する
    [SerializeField] Text DeathCountText;

    // オフ状態の時に近づかれたときに出すため
    [SerializeField] GameObject iconAction;

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
        if (!stateAvatar)
        {
            // オフ状態の時のみ動かす
            if (isVisible)
            {
                // 画面外にいるアバターに起動させられないようにするため
                FindAvatar();
            }
            return;
        }
        if (!isVisible)
        {
            // オン状態
            CountUp();
        }
        if (stateLock)
        {
            return;
        }
        // オン状態・ロックされていない時に動かす
        Move();
        Attack();
        if (CheckGround())
            Jump();        
    }

    /// <summary>
    /// 敵との接触判定をするメソッド
    /// </summary>
    /// <param name="collision">相手のコライダー</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!stateAvatar)
        {  
            // 起動していなければリターン
            return;
        }
        // 敵と接触した場合にダメージ
        if (collision.gameObject.tag == "Enemy")
        {
            Death();
        }

        if (collision.gameObject.tag == "EnemyLock")
        {
            // EnemyLockと接触したらロックされる
            stateLock = true;
        }
    }

    /// <summary>
    /// ロックの解除の判定
    /// </summary>
    /// <param name="collision">相手のコライダー</param>
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyLock")
        {
            // EnemyLockが倒れたらロックが解除される
            stateLock = false;
        }
    }

    /// <summary>
    /// 自分をオフ状態からアバター扱いしてもらうメソッド
    /// </summary>
    private void FindAvatar()
    {
        Vector3 offset = Vector3.zero;
        offset.x = avatarWidth / 2;
        Debug.DrawRay(transform.position - offset, Vector2.left * findRange);
        // 前にプレイヤー又はアバターがあった場合、起動させられる。
        if (Physics2D.Raycast(transform.position - offset, Vector2.left, findRange, layerMask))
        {
            iconAction.SetActive(true);
            if (Input.GetKeyDown(keyAction))
            {
                // オフ状態からアバター扱いにする
                this.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                PlayerStatus.GetComponent<PlayerStatus>().AddNumberAvatar();
                this.gameObject.layer = LayerMask.NameToLayer("Avatar");
                Destroy(iconAction);
                stateAvatar = true;
            }
        }
    }

    /// <summary>
    /// やられているか確認
    /// </summary>
    /// <returns>やられているかどうか</returns>
    private bool IsDeath()
    {
        return HP <= 0;
    }

    /// <summary>
    /// 画面外に出た時の処理を始める
    /// </summary>
    private void OnBecameInvisible()
    {
        isVisible = false;
    }

    /// <summary>
    /// 画面内に戻ってきたら処理を終わる
    /// </summary>
    private void OnBecameVisible()
    {
        isVisible = true;
        count = 0;
        deathCount = INIT_DEATH_COUNT;
        DeathCountText.GetComponent<Text>().text = "";
    }

    /// <summary>
    /// カウントをする
    /// </summary>
    private void CountUp()
    {
        DeathCountText.GetComponent<Text>().text = deathCount.ToString() + " / " + INIT_DEATH_COUNT.ToString();
        count++;
        if (count == 36)
        {
            deathCount--;
            count = 0;
        }       
        if (deathCount == 0)
        {
            Death();   
        }
    }

    /// <summary>
    /// やられた時の処理
    /// </summary>
    private void Death()
    {
        HP--;
        DeathCountText.GetComponent<Text>().text = "";
        PlayerStatus.GetComponent<PlayerStatus>().AddDamageAvatar(1);
        if (IsDeath())
        {
            Destroy(this.gameObject);
        }
    }
}
