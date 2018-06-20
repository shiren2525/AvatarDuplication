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

    // ステージによって、スイッチを押したときの処理が違うため、番号で分ける。
    public int motionSelect;

    // 各スイッチが押されているかどうかを管理
    private bool push1;
    private bool push2;
    private bool push3;

    // すべて押されたときに削除するオブジェクト
    [SerializeField] GameObject DestroyObj;

    // 全て押されたときに出現させるオブジェクト
    [SerializeField] GameObject CreateObj;

    [SerializeField] PlaySound playSE;

    /// <summary>
    /// スイッチが押されたときに呼び出して、状態を管理する。
    /// </summary>
    /// <param name="num">押されたスイッチの番号</param>
    /// <param name="push">押されているかどうか</param>
    public void AddPush(int num, bool push)
    {
        switch (num)
        {
            case 1:
                push1 = push;
                Debug.Log("Switch1の状態: " + push1);
                break;
            case 2:
                push2 = push;
                Debug.Log("Switch2の状態: " + push2);
                break;
            case 3:
                push3 = push;
                Debug.Log("Switch3の状態: " + push3);
                break;
            default:
                Debug.Log("Switchの番号が違います。入っている数値=" + num);
                break;
        }

        playSE.PlaySE(4);
        SwitchClear();
    }

    /// <summary>
    /// すべてのスイッチが押されたときの処理
    /// </summary>
    private void SwitchClear()
    {
        if (push1 && push2 && push3)
        {
            if (motionSelect == 0)
            {
                Destroy();
            }
            else if (motionSelect == 1)
            {
                Create();
            }

            Debug.Log("Clear");
            playSE.PlaySE(5);
            Destroy(this.gameObject);
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
