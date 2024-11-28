using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    float destroyTime = 0.2f;
    private SpriteRenderer spr;
    private CircleCollider2D cld;

    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        cld = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Player") 
        {
            Destroy(gameObject,destroyTime);
        }
    }
}
