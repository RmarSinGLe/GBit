using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnEffect : MonoBehaviour
{
    public Image buttonImage; // ���а�ť��Image���
    public Sprite normalImage; // ����״̬��ͼƬ
    public Sprite hoverImage;  // ����״̬��ͼƬ
    public Sprite clickImage;  // ���״̬��ͼƬ

    private void Start()
    {
        // ��ʼ����ťͼ��Ϊ����״̬
        buttonImage.sprite = normalImage;

        // ��Ӱ�ť������¼�
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    // �����¼�
    public void OnMouseEnter()
    {
        buttonImage.sprite = hoverImage; 
    }

    // ���������¼�
    public void OnMouseExit()
    {
        buttonImage.sprite = normalImage; 
    }

    // ����¼�
    public void OnClick()
    {
        buttonImage.sprite = clickImage; 
        Invoke("ResetImage", 0.1f); 
    }

    // �ָ�ͼ��Ϊ����״̬
    private void ResetImage()
    {
        buttonImage.sprite = normalImage;
    }
}
