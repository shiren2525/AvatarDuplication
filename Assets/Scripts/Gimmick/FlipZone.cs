using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// フリップゾーンのクラス
/// </summary>
public class FlipZone : MonoBehaviour {

    [SerializeField] PlaySound playSE;

    /// <summary>
    /// プレイヤーかアバターの操作を反転させる
    /// </summary>
    /// <param name="collision">相手のコライダー</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            collision.gameObject.GetComponent<PlayerBase>().FlipMove();
            playSE.PlaySE(0);
        }
        if (collision.gameObject.tag == "Avatar")
        {
            collision.gameObject.GetComponent<PlayerBase>().FlipMove();
            playSE.PlaySE(0);
        }
    }
}
