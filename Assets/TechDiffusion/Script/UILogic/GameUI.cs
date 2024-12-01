using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Slider sanBarSlider; 
    public float maxSan = 100f;
    private float currentSan;    

    public Player player;

    public GameObject pauseMenuPanel; 
    public GameObject settingPanel;
    public Button resumeButton; 
    public Button pauseButton; 
    public Button exitButton;
    public Button settingButton;
    public Button restartButton;
    public Scrollbar volumeScrollbar;
    public Toggle muteToggle;
    public AudioSource audioSource;
    private bool isPaused = false; // 游戏是否暂停标志

    public Image[] muteImage;
    private float previousVolume; // 用于保存之前的音量值

    void Start()
    {
        currentSan = maxSan;
        UpdateHealthBar();

        pauseMenuPanel.SetActive(false); 
        settingPanel.SetActive(false); 

        resumeButton.onClick.AddListener(ResumeGame);
        exitButton.onClick.AddListener(QuitApplication);
        pauseButton.onClick.AddListener(PauseGame);
        settingButton.onClick.AddListener(OpenSettings);
        restartButton.onClick.AddListener(ReStart);

        previousVolume = PlayerPrefs.GetFloat("VolumeLevel", 1f); // 初始化previousVolume
        volumeScrollbar.value = previousVolume; // 初始化滑动条位置
        audioSource.volume = previousVolume; // 初始化音量
        volumeScrollbar.onValueChanged.AddListener(OnVolumeChange);
        muteToggle.onValueChanged.AddListener(OnMuteToggleChange);


    }
    private void Update()
    {
        UpdateHealthBar();
        EscListener();
        showMuteImage();
        Debug.Log(volumeScrollbar.value + "||" + audioSource.volume);
    }

    private void OpenSettings()
    {
        if (settingPanel.activeSelf)
        {
            settingPanel.SetActive(false);
        }
        else
        {
            settingPanel.SetActive(true);
        }
    }

    void UpdateHealthBar()
    {
        currentSan = player.san;
        sanBarSlider.value = currentSan; 
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenuPanel.SetActive(false); // 隐藏暂停菜单
        Time.timeScale = 1f; // 恢复游戏时间
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseMenuPanel.SetActive(true); // 显示暂停菜单
        Time.timeScale = 0f; // 暂停游戏时间
    }

    public void EscListener()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void QuitApplication()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void OnMuteToggleChange(bool isMuted)
    {
        if (isMuted)
        {
            previousVolume = volumeScrollbar.value; // 保存当前音量
            audioSource.volume = 0;
            volumeScrollbar.value = 0; // 设置滑条为0
        }
        /*else
        {
            audioSource.volume = previousVolume; // 恢复到之前的音量
            volumeScrollbar.value = previousVolume; // 恢复滑条的位置
            audioSource.Play(); // 播放音频
        }*/
    }

    private void OnVolumeChange(float value)
    {
        if (value <= 0)
        {
           // muteToggle.isOn = true; // 滑条音量为0时设置静音按钮为选中状态
            audioSource.volume = 0; // 将音量设置为0
        }
        else
        {
            muteToggle.isOn = false; // 否则取消静音按钮的选中状态
            audioSource.volume = value; // 更新音量
        }

        PlayerPrefs.SetFloat("VolumeLevel", value);
    }

    private void OnDestroy()
    {
        volumeScrollbar.onValueChanged.RemoveListener(OnVolumeChange);
        muteToggle.onValueChanged.RemoveListener(OnMuteToggleChange);

        resumeButton.onClick.RemoveListener(ResumeGame);
        exitButton.onClick.RemoveListener(QuitApplication);
        pauseButton.onClick.RemoveListener(PauseGame);
        settingButton.onClick.RemoveListener(OpenSettings);
        restartButton.onClick.RemoveListener(ReStart);
    }

    public void showMuteImage()
    {
        if(volumeScrollbar.value==0)
        {
            foreach (var img in muteImage)
            {
                img.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach(var img in muteImage)
            {
                img.gameObject.SetActive(false);
            }
        }
    }

    public void ReStart()
    {
        SceneManager.LoadScene(1);
    }
}
