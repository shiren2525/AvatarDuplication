using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ワープのクラス
/// </summary>
public class Warp : MonoBehaviour {
    
    // ワープ先の座標を取る
    [SerializeField] GameObject WarpZone;

    [SerializeField] PlaySound playSE;
    
    /// <summary>
    /// プレイヤーかアバターが接触した場合にワープさせる
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 pos = WarpZone.transform.position;
        Vector3 offset = new Vector3(0, 1.3f, 0);

        if (collision.gameObject.tag == ("Player"))
        {
            collision.gameObject.transform.position = pos - offset;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            playSE.PlaySE(8);
        }
        if (collision.gameObject.tag == "Avatar")
        {
            collision.gameObject.transform.position = pos - offset;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            playSE.PlaySE(8);
        }
    }
}
