using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// メイン画面で使用するクリアとオーバーで遷移させる派生クラス
/// </summary>
public class ClearOver : SceneBase {

    // クリア時とオーバー時に使用
    [SerializeField] Text ClearOverText;

    void Update () {
        Menu();	
	}

    /// <summary>
    /// クリア判定をとる
    /// </summary>
    /// <param name="collision">プレイヤーのコライダー</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ClearOverText.GetComponent<Text>().text = "Clear";
            Invoke("CallTitleScene", 5.0f);
        }
    }

    /// <summary>
    /// オーバー時に呼び出す
    /// </summary>
    public void GameOver()
    {
        ClearOverText.GetComponent<Text>().text = "Game Over";
        Invoke("CallTitleScene", 5.0f);
    }

    /// <summary>
    /// タイトルシーンを時間差で呼び出すため
    /// </summary>
    public void CallTitleScene()
    {
        SceneManager.LoadScene("Title");
    }
}
