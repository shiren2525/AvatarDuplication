using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// タイトル画面で使用する派生クラス
/// </summary>
public class Title : SceneBase {

    private KeyCode keyLeft = KeyCode.LeftArrow;
    private KeyCode keyRight = KeyCode.RightArrow;
    private KeyCode keyAction = KeyCode.Space;

    // カーソルの位置を制御するための番号
    private int selectNum = 1;

    // ボタンの座標を所得するため
    [SerializeField] GameObject CursorObj;
    [SerializeField] GameObject StageSelectObj;
    [SerializeField] GameObject ManualObj;
    [SerializeField] GameObject CreditObj;
    [SerializeField] GameObject QuitObj;

    [SerializeField] PlaySound playSE;

    private void Update()
    {
        Menu();
        Selected();
    }

    // 番号によってシーン遷移する
    private void LoadScene()
    {
        switch (selectNum)
        {
            case 1:
                SceneManager.LoadScene("StageSelect");
                break;
            case 2:
                SceneManager.LoadScene("Manual");
                break;
            case 3:
                SceneManager.LoadScene("Credit");
                break;
            case 4:
                Application.Quit();
                break;
        } 
    }

    // キー入力によって番号を変更する
    private void Selected()
    {
        if (Input.GetKeyDown(keyRight))
        {
            selectNum++;
            Cursor();
            playSE.PlaySE(0);
        }
        else if (Input.GetKeyDown(keyLeft))
        {
            selectNum--;
            Cursor();
            playSE.PlaySE(0);
        }
        else if (Input.GetKeyDown(keyAction))
        {
            playSE.PlaySE(1);
            Invoke("LoadScene", 1.0f);
        }
    }

    // カーソルの位置を制御する
    private void Cursor()
    {
        Vector3 cursorPosition = CursorObj.transform.position;
        Vector3 stageSelectPotion = StageSelectObj.transform.position;
        Vector3 manualPosition = ManualObj.transform.position;
        Vector3 creditPosition = CreditObj.transform.position;
        Vector3 quitPosition = QuitObj.transform.position;

        switch (selectNum)
        {
            case 0:
                cursorPosition = quitPosition;
                selectNum = 4;
                break;
            case 1:
                cursorPosition = stageSelectPotion;
                break;
            case 2:
                cursorPosition = manualPosition;
                break;
            case 3:
                cursorPosition = creditPosition;
                break;
            case 4:
                cursorPosition = quitPosition;
                break;
            case 5:
                cursorPosition = stageSelectPotion;
                selectNum = 1;
                break;
        }
        Debug.Log("Title 選ばれている番号: " + selectNum);
        CursorObj.transform.position = cursorPosition;
    } 
}
