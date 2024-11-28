using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject dialogPanel;

    public Button CloseButtonOfDialogPanel;
    public Button showDialogButton; // in fact, is't "Exit" Button.
    public Button startButton;
    public Button quitButtonOK;    //the true exit game button which on the dialogPanel
    void Start()
    {
        CloseButtonOfDialogPanel.onClick.AddListener(HideDialog);
        showDialogButton.onClick.AddListener(ShowDialog);
        dialogPanel.SetActive(false);
        startButton.onClick.AddListener(StartGame);
        quitButtonOK.onClick.AddListener(QuitApplication);
    }

    void Update()
    {
        
    }
    private void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void ShowDialog()
    {
        dialogPanel.SetActive(true); // 显示对话框
    }

    void HideDialog()
    {
        dialogPanel.SetActive(false); // 隐藏对话框
    }

    void QuitApplication()
    {
        // 退出游戏
        Application.Quit();
        // 如果在编辑器下测试，也需要停止播放
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }
}
