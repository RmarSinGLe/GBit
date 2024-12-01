using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public bool isVisited=false;
    void Start()
    {
        isVisited = false;
    }
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            StartCoroutine(HandleCheckpoint());
        }
    }

    private IEnumerator HandleCheckpoint()
    {
        yield return new WaitForSeconds(0.1f);
        isVisited = true;
    }
}
