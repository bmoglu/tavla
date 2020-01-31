using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    public GameObject Heart, BreakHeart,GirlFriend;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        VisionLook();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Heart.SetActive(false);
            BreakHeart.SetActive(true);
        }
       
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Heart.SetActive(true);
            BreakHeart.SetActive(false);
        }
    }

    private void VisionLook()
    {
        
    }
}
