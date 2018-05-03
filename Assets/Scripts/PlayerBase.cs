using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour {

    // 操作ボタン
    private KeyCode keyLeft = KeyCode.LeftArrow;
    private KeyCode keyRight = KeyCode.RightArrow;
    private KeyCode keyAttack = KeyCode.Z;
    private KeyCode keyJump = KeyCode.Space;

    // 移動速度
    private float speed;

    public void Move()
    {
        if (Input.GetKeyDown(keyLeft))
        {
            speed++;
        }
    }
}
