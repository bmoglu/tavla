﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    private bool isTalking = false;

    private float tavlanmaSayisi = 0; // 100 kalbini onarmış olacağız.

    private float tavlanmaHizi = .5f; //Karaktere göre tavlanma kolaylığı değişecek

    
    private GirlFriendPatrol gfPatrolScript;
    
    public GameObject Heart, BreakHeart,GirlFriend;
    void Start()
    {
        gfPatrolScript = GetComponentInParent<GirlFriendPatrol>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTalking)
        {
            GetComponentInParent<GirlFriendPatrol>().player.GetComponent<Animator>().SetBool("isTalking", true);
            gfPatrolScript.speed = 0;
            
                        if (PlayerController.isTalking && !BreakHeart.activeSelf)
                        {
                            tavlanmaSayisi = 0;
            
                            gfPatrolScript.bar.parent.gameObject.SetActive(true);
                        
                            Heart.SetActive(false);
                            BreakHeart.SetActive(true);
                        } else
                        {
                            if (tavlanmaSayisi <= 100)
                            {
                                tavlanmaSayisi += tavlanmaHizi;
                                SetSizeBar(tavlanmaSayisi / 100);
                            }
                            else
                            {
                                //Tavlandı
                            }
                        }
        }
        
        VisionLook();
        
        if (tavlanmaSayisi >= 100)
        {
            tavlanmaSayisi = 0;
            gfPatrolScript.bar.parent.gameObject.SetActive(false);
            gfPatrolScript.speed = 1;
            Heart.SetActive(true);
            BreakHeart.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            isTalking = true;


        }
        
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            isTalking = false;
            gfPatrolScript.speed = 1;
            GetComponentInParent<GirlFriendPatrol>().player.GetComponent<Animator>().SetBool("isTalking", false);

        }
    }

    private void VisionLook()
    {
        
    }
    
    public void SetSizeBar(float sizeNormalized)
    {
        gfPatrolScript.bar.localScale = new Vector3(sizeNormalized, 1f);
    }
}