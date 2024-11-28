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
        dialogPanel.SetActive(true); // ��ʾ�Ի���
    }

    void HideDialog()
    {
        dialogPanel.SetActive(false); // ���ضԻ���
    }

    void QuitApplication()
    {
        // �˳���Ϸ
        Application.Quit();
        // ����ڱ༭���²��ԣ�Ҳ��Ҫֹͣ����
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }
}
