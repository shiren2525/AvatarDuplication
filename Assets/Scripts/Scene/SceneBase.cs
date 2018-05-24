using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーンを制御する基底クラス
/// </summary>
public class SceneBase : MonoBehaviour {

    // すべてのシーンで受け付けるシーン遷移
    protected void Menu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Title");
        }
    }
}
