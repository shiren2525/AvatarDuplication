using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 押されたことをマネージャーに報告するクラス
/// </summary>
public class MultiSwitch : MonoBehaviour
{
    // 各Switchの状態を管理するために、各Switchに番号を割り当てる。
    public int switchNum;

    // MultiSwitchManagerに報告するために必要
    [SerializeField] MultiSwichManager multiSwitchManager;

    [SerializeField] Animator Anim;
    
    /// <summary>
    /// 押されたらtrue
    /// </summary>
    /// <param name="collision">相手のコライダー</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            multiSwitchManager.AddPush(switchNum, true);
            Anim.SetBool("Pushed", true);
        }
        if (collision.gameObject.tag == "Avatar")
        {
            multiSwitchManager.AddPush(switchNum, true);
            Anim.SetBool("Pushed", true);
        }
    }

    /// <summary>
    /// 離れたらfalse
    /// </summary>
    /// <param name="collision">相手のコライダー</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            multiSwitchManager.AddPush(switchNum, false);
            Anim.SetBool("Pushed", false);
        }
        if (collision.gameObject.tag == "Avatar")
        {
            multiSwitchManager.AddPush(switchNum, false);
            Anim.SetBool("Pushed", false);
        }
    }
}
