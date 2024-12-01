using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Owari : MonoBehaviour
{
    
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Victory! Switching to victory scene.");
            SceneManager.LoadScene("VictoryScene"); 
        }
    }

}
