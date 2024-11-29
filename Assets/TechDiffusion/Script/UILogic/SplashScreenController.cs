using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenController : MonoBehaviour
{
    public float splashDisplayTime = 3f; // 启动画面持续时间

    private void Start()
    {
        StartCoroutine(DisplaySplashScreen());
    }

    private IEnumerator DisplaySplashScreen()
    {
        // 显示启动动画
        yield return new WaitForSeconds(splashDisplayTime);
        // 加载主场景
        SceneManager.LoadScene("StartScene");
    }
}
