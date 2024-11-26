using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPoint : MonoBehaviour
{
    public bool isTrigger=false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("tp touch!!");
        isTrigger = true;
    }
}
