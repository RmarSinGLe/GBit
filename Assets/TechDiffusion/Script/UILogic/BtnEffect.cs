using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnEffect : MonoBehaviour
{
    public Image buttonImage; // 持有按钮的Image组件
    public Sprite normalImage; // 正常状态的图片
    public Sprite hoverImage;  // 悬浮状态的图片
    public Sprite clickImage;  // 点击状态的图片

    private void Start()
    {
        // 初始化按钮图像为正常状态
        buttonImage.sprite = normalImage;

        // 添加按钮的鼠标事件
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    // 悬浮事件
    public void OnMouseEnter()
    {
        buttonImage.sprite = hoverImage; 
    }

    // 撤销悬浮事件
    public void OnMouseExit()
    {
        buttonImage.sprite = normalImage; 
    }

    // 点击事件
    public void OnClick()
    {
        buttonImage.sprite = clickImage; 
        Invoke("ResetImage", 0.1f); 
    }

    // 恢复图像为正常状态
    private void ResetImage()
    {
        buttonImage.sprite = normalImage;
    }
}
