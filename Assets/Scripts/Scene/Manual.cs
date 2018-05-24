using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// マニュアルのシーンで使用する派生クラス
/// </summary>
public class Manual : SceneBase {

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Title");
        }
    }
}
