using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private bool isGrounded;
    public  static bool isTaked;
    public  static bool isBuket;
    public  static bool isBabyBear;
    public  static bool isGift;

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
            isTaked = false;
        }
        
        if (other.transform.CompareTag("Player") && gameObject.CompareTag("Buket"))
        {
            isGrounded = false;
            isTaked = true;
            isBuket = true;
            
            Destroy(gameObject);
        }
        
        if (other.transform.CompareTag("Player") && gameObject.CompareTag("BabyBear"))
        {
            isGrounded = false;
            isTaked = true;
            isBabyBear = true;
            
            Destroy(gameObject);
        }
        
        if (other.transform.CompareTag("Player") && gameObject.CompareTag("Gift"))
        {
            isGrounded = false;
            isTaked = true;
            isGift = true;
            
            Destroy(gameObject);
        }
    }
    
    
}
