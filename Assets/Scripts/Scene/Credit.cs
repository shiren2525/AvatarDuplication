using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// クレジットのシーンで使う派生クラス
/// </summary>
public class Credit : SceneBase
{
    [SerializeField] PlaySound playSE;

    private void Update()
    {
        Menu();
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown(A) || Input.GetButtonDown(B))
        {
            playSE.PlaySE(0);
            Invoke("LoadScene", 0.5f);
        }
    }
    private void LoadScene()
    {
        SceneManager.LoadScene("Title");
    }
}
