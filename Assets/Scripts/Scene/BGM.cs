using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BGMを管理するクラス
/// </summary>
public class BGM : MonoBehaviour {

    [SerializeField] AudioSource AS;
    [SerializeField] AudioClip[] sources;

    // BGMがあるかどうか判定するために必要
    private static BGM instance;

    private void Awake()
    {
        if (instance != null)
        {
            // 既に存在した場合は、削除する。
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }	

    public static BGM GetInstance()
    {
        return instance;
    }

    public void PlayBGM(int num)
    {
        AS.clip = sources[num];
        AS.Play();
    }
    // 使い方：BGM.GetInstance().PlayBGM();
}
