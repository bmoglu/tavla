using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slowly : MonoBehaviour
{
    private float speed = 0.5f;

    private float alpha;
    
    // Start is called before the first frame update
    void Start()
    {
        alpha = 0.001f;
    }

    // Update is called once per frame
    void Update()
    {
        if (alpha < 255)
        {
            alpha += alpha * speed * Time.deltaTime;
            transform.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, alpha);
        }
        
    }
}
