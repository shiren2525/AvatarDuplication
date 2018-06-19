using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーンを制御する基底クラス
/// </summary>
public class SceneBase : MonoBehaviour {

    protected string Horizontal = "Horizontal";
    protected string Vertical = "Vertical";
    protected string A = "A";
    protected string B = "B";

    protected bool hori;

    // すべてのシーンで受け付けるシーン遷移
    protected void Menu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        else if (Input.GetKeyDown(KeyCode.R)||Input.GetButtonDown("Start"))
        {
            SceneManager.LoadScene("Title");
        }
    }
}
