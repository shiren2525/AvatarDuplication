using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// マルチSwitchが全て押されているか判定するクラス
/// </summary>
public class MultiSwichManager : MonoBehaviour {

    // 同時押しするスイッチの数
    public static int Switches = 3;

    // 押されているスイッチの数を管理
    private int PushedSwitches;
    
    /// <summary>
    /// 押されている数が、同時押しする数と同じか判定
    /// </summary>
    /// <param name="push">押されている数</param>
    public void AddPush(int push)
    {
        PushedSwitches += push;
        Debug.Log("押されているスイッチの数: " + PushedSwitches);
        if (Switches == PushedSwitches)
        {
            Debug.Log("Clear");
            // TODO:ここに処理を入れたい
        }
    }
}
