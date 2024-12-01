using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 后处理控制 : MonoBehaviour
{
    public 后处理  post;
    public Material m2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnEnable()
    {
        post.material = m2;
    }

}
