using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// マルチSwitchが全て押されているか判定するクラス
/// </summary>
public class MultiSwichManager : MonoBehaviour
{

    // 同時押しするスイッチの数
    public static int switches = 3;

    // 押されているスイッチの数を管理
    private int pushedSwitches;

    public int motionSelect;

    // すべて押されたときに削除するオブジェクト
    [SerializeField] GameObject DestroyObj;

    [SerializeField] GameObject CreateObj;

    [SerializeField] PlaySound playSE;
    /// <summary>
    /// 押されている数が、同時押しする数と同じか判定
    /// </summary>
    /// <param name="push">押されている数</param>
    public void AddPush(int push)
    {
        pushedSwitches += push;
        playSE.PlaySE(4);
        Debug.Log("押されているスイッチの数: " + pushedSwitches);
        if (switches == pushedSwitches)
        {
            Debug.Log("Clear");
            if (motionSelect == 0)
            {
                Destroy();
            }
            else if (motionSelect == 1)
            {
                Create();
            }
        }
    }

    private void Destroy()
    {
        Destroy(DestroyObj);
    }
    private void Create()
    {
        CreateObj.SetActive(true);
    }
}
