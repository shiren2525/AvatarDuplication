using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SEを再生するクラス
/// </summary>
public class PlaySound : MonoBehaviour {

    // SEを格納する
    [SerializeField] AudioClip[] sources;
    private AudioSource AS;

    void Start()
    {
        AS = gameObject.GetComponent<AudioSource>();
    }

    // 他のクラスで呼び出してSEを再生するために使う
    public void PlaySE(int num)
    {
        AS.PlayOneShot(sources[num]);
    }
}
