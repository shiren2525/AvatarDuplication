using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    // 出現させるエネミーを格納する
    [SerializeField] GameObject Enemy;

    // 出現させるエネミーの座標を管理するために必要
    private Vector3 pos;

    // 出現せせるエネミーのざひょうをこのオブジェクトとの差を管理する
    public float spawnPosX;
    public float spawnPosY;

    // 出現させる回数
    public int spawnCount;

    void Start()
    {
        pos = transform.position;
    }

    /// <summary>
    /// プレイヤーかアバターが接触した場合に出現させる
    /// </summary>
    /// <param name="collision">相手のコライダー</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Spawn();
        }
        else if (collision.gameObject.tag == "Avatar")
        {
            Spawn();
        }
    }

    /// <summary>
    /// エネミーをスポーンさせる
    /// </summary>
    private void Spawn()
    {
        Instantiate(Enemy, new Vector3(pos.x + spawnPosX, pos.y + spawnPosY, pos.z), Quaternion.identity);
        spawnCount--;

        if (spawnCount <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
