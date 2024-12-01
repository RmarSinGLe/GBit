using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Owari : MonoBehaviour
{
    public Transform victory;
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Victory! Switching to victory scene.");
            victory.gameObject.SetActive(true);
            // ÇÐ»»µ½Ê¤Àû³¡¾°
            // SceneManager.LoadScene("VictoryScene"); 
        }
    }

}
