using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// クレジットのシーンで使う派生クラス
/// </summary>
public class Credit : SceneBase {

    private void Update()
    {
        Menu();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Title");
        }
    }
}
