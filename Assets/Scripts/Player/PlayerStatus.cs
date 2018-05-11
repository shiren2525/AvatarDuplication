using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour {

    // 初期HP確定に必要
    const int initialHPPlayer = 3;
    const int initialHPAvatar = 6;

    // HP管理に使用
    public int HPPlayer = initialHPPlayer;
    public int HPAvatar = initialHPAvatar;

    // HPアイコンの非表示に使用
    [SerializeField] private GameObject[] iconHPPlayer = new GameObject[initialHPPlayer];
    [SerializeField] private GameObject[] iconHPAvatar = new GameObject[initialHPAvatar];

    // アバターの最大数確定に必要
    const int initialTotalAvatar = 10;

    public int numberAvatar = 0;
    public int totalAvatar = initialTotalAvatar;

    [SerializeField] Text TextTotalAvatar;

    private void Update()
    {
        TextTotalAvatar.GetComponent<Text>().text = numberAvatar.ToString() + " / " + totalAvatar.ToString();
    }

    public void AddDamagePlayer(int damage)
    {
        iconHPPlayer[HPPlayer - 1].SetActive(false);
        HPPlayer -= damage;
    }

    public void AddDamageAvatar(int damage)
    {
        iconHPAvatar[HPAvatar - 1].SetActive(false);
        numberAvatar--;
        totalAvatar--;
        HPAvatar -= damage;
    }

    public void AddNumberAvatar()
    {
        numberAvatar++;
    }

}
