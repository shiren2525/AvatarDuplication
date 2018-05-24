using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーとアバターが使う攻撃判定のクラス
/// </summary>
public class Attack : MonoBehaviour {

    //攻撃判定の持続時間
    public float activeFrame;

    void Start ()
    {
        Destroy(this.gameObject, activeFrame);		
	}
}
