using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 画面上のステータスメニューを管理するクラス
/// </summary>
public class PlayerStatus : MonoBehaviour {

    // 初期HP確定に必要
    const int INITIAL_HP_PLAYER = 3;
    const int INITIAL_HP_AVATAR = 6;

    // HP管理に使用
    public int HPPlayer = INITIAL_HP_PLAYER;
    public int HPAvatar = INITIAL_HP_AVATAR;

    // HPアイコンの非表示に使用
    [SerializeField] private GameObject[] iconHPPlayer = new GameObject[INITIAL_HP_PLAYER];
    [SerializeField] private GameObject[] iconHPAvatar = new GameObject[INITIAL_HP_AVATAR];

    // アバターの最大数確定に必要
    const int INITIAL_TOTAL_AVATAR = 10;

    // 現在引き連れているアバターの数
    public int numberAvatar = 0;

    // 現在のステージにいるすべてのアバターの数
    public int totalAvatar = INITIAL_TOTAL_AVATAR;

    // 左下に出すアバターの数のテキスト
    [SerializeField] Text TextTotalAvatar;

    // クリアとオーバーを知らせるため
    [SerializeField] ClearOver clearOver;
    
    /// <summary>
    /// プレイヤーにダメージが入った場合にアイコンを減らす
    /// </summary>
    /// <param name="damage">受けたダメージ</param>
    public void AddDamagePlayer(int damage)
    {
        iconHPPlayer[HPPlayer - 1].SetActive(false);
        HPPlayer -= damage;
        if (HPPlayer <= 0)
        {
            clearOver.GameOver();
        }
    }

    /// <summary>
    /// アバターにダメージが入った場合にアイコンを減らす
    /// </summary>
    /// <param name="damage">やられたアバターの数</param>
    public void AddDamageAvatar(int damage)
    {
        if (HPAvatar > 0)
        {
            numberAvatar--;
            totalAvatar--;
            HPAvatar -= damage;
            iconHPAvatar[HPAvatar].SetActive(false);
            TextTotalAvatar.GetComponent<Text>().text = numberAvatar.ToString() + " / " + totalAvatar.ToString();
        }
        if (HPAvatar <= 0)
        {
            clearOver.GameOver();
        }
    }

    /// <summary>
    /// アバターが増えたときにアイコンを増やすメソッド
    /// </summary>
    public void AddNumberAvatar()
    {
        numberAvatar++;
        TextTotalAvatar.GetComponent<Text>().text = numberAvatar.ToString() + " / " + totalAvatar.ToString();
    }
}
