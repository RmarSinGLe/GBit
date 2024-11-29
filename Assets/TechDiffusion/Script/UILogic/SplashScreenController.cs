using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenController : MonoBehaviour
{
    public float splashDisplayTime = 3f; // �����������ʱ��

    private void Start()
    {
        StartCoroutine(DisplaySplashScreen());
    }

    private IEnumerator DisplaySplashScreen()
    {
        // ��ʾ��������
        yield return new WaitForSeconds(splashDisplayTime);
        // ����������
        SceneManager.LoadScene("StartScene");
    }
}
