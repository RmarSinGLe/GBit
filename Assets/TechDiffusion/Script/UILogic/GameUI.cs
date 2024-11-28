using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public Scrollbar volumeScrollbar;
    public Toggle muteToggle;
    public AudioSource audioSource;
    private bool isPaused = false; // ��Ϸ�Ƿ���ͣ��־

    public Image[] muteImage;
    private float previousVolume; // ���ڱ���֮ǰ������ֵ
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

        previousVolume = PlayerPrefs.GetFloat("VolumeLevel", 1f); // ��ʼ��previousVolume
        volumeScrollbar.value = previousVolume; // ��ʼ��������λ��
        audioSource.volume = previousVolume; // ��ʼ������
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
        settingPanel.SetActive(true);
    }

    void UpdateHealthBar()
    {
        currentSan = player.san;
        sanBarSlider.value = currentSan; 
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenuPanel.SetActive(false); // ������ͣ�˵�
        Time.timeScale = 1f; // �ָ���Ϸʱ��
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseMenuPanel.SetActive(true); // ��ʾ��ͣ�˵�
        Time.timeScale = 0f; // ��ͣ��Ϸʱ��
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
        // �˳���Ϸ
        Application.Quit();
        // ����ڱ༭���²��ԣ�Ҳ��Ҫֹͣ����
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void OnMuteToggleChange(bool isMuted)
    {
        if (isMuted)
        {
            previousVolume = volumeScrollbar.value; // ���浱ǰ����
            audioSource.volume = 0;
            volumeScrollbar.value = 0; // ���û���Ϊ0
        }
        else
        {
            audioSource.volume = previousVolume; // �ָ���֮ǰ������
            volumeScrollbar.value = previousVolume; // �ָ�������λ��
            audioSource.Play(); // ������Ƶ
        }
    }

    private void OnVolumeChange(float value)
    {
        if (value == 0)
        {
            // �������ֵΪ0�������л�Ϊѡ��״̬
            muteToggle.isOn = true;
            audioSource.volume = 0; // ����������Ϊ0
        }
        else
        {
            // �������ֵ��Ϊ0��ȡ���л�
            muteToggle.isOn = false;
            audioSource.volume = value; // ��������
        }

        // ������������
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
}
