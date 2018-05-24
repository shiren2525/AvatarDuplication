using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カメラの位置をプレイヤーに合わせるクラス
/// </summary>
public class CameraController : MonoBehaviour {

    // プレイヤーの座標を所得するため
    [SerializeField] GameObject player;

    // プレイヤーのオフセット距離を所得するために必要
    private Vector3 offset;
    
    void Start()
    {
        offset = transform.position - player.transform.position;
    }
    
    void LateUpdate()
    {
        // プレイヤーが移動した後に、位置を合わせる
        transform.position = player.transform.position + offset;
    }
}
