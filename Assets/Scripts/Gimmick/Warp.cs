using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ワープのクラス
public class Warp : MonoBehaviour {
    
    // ワープ先の座標を取る
    [SerializeField] GameObject WarpZone;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 pos = WarpZone.transform.position;
        Vector3 offset = new Vector3(0, 5.2f, 0);
        // ToDoワープ先の座標の調整
        if (collision.gameObject.tag == ("Player"))
        {
            collision.gameObject.transform.position = pos - offset;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        if (collision.gameObject.tag == "Avatar")
        {
            collision.gameObject.transform.position = pos - offset;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
