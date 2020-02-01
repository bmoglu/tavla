using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public bool isGrounded;
    public bool isTaked;

    // Start is called before the first frame update
    void Start()
    {
        isGrounded = false;
        isTaked = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Floor"))
        {
            isGrounded = true;
        }
        
        if (other.transform.CompareTag("Player"))
        {
            isGrounded = false;
            isTaked = true;
        }
    }
    
    
}
