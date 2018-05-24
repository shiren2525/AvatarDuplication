using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 押されたことをマネージャーに報告するクラス
/// </summary>
public class MultiSwitch : MonoBehaviour {

    // MultiSwitchManagerに報告するために必要
    [SerializeField] MultiSwichManager multiSwitchManager;

    [SerializeField] Animator Anim;

    /// <summary>
    /// 押されたら増やす
    /// </summary>
    /// <param name="collision">相手のコライダー</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            multiSwitchManager.AddPush(1);
            Anim.SetBool("Pushed", true);
        }
        if (collision.gameObject.tag == "Avatar")
        {
            multiSwitchManager.AddPush(1);
            Anim.SetBool("Pushed", true);
        }
    }

    /// <summary>
    /// 離れたら減らす
    /// </summary>
    /// <param name="collision">相手のコライダー</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            multiSwitchManager.AddPush(-1);
            Anim.SetBool("Pushed", false);
        }
        if (collision.gameObject.tag == "Avatar")
        {
            multiSwitchManager.AddPush(-1);
            Anim.SetBool("Pushed", false);
        }
    }
}
