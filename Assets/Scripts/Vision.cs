using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    private bool isTalking = false;

    private float tavlanmaSayisi = 0; // 100 kalbini onarmış olacağız.

    private float tavlanmaHizi = 2f; //Karaktere göre tavlanma kolaylığı değişecek

    
    private GirlFriendPatrol gfPatrolScript;
    
    public GameObject Heart, BreakHeart,GirlFriend;

    private PlayerController pc;

    private void Awake()
    {
        pc=GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    void Start()
    {
        gfPatrolScript = GetComponentInParent<GirlFriendPatrol>();
    }

    // Update is called once per frame
    void Update()
    {
        if (UIController.isGameStart && !UIController.isGamePasue)
        {


            if (isTalking)
            {
                if (PowerUp.isTaked && PowerUp.isBuket)
                {
                    tavlanmaHizi = 2.5f;
                }
                else
                {
                    tavlanmaHizi = 0.5f;
                }

                GetComponentInParent<GirlFriendPatrol>().player.GetComponent<Animator>().SetBool("isTalking", true);
                gfPatrolScript.speed = 0;

                if (PlayerController.isTalking && !BreakHeart.activeSelf)
                {
                    tavlanmaSayisi = 0;

                    gfPatrolScript.bar.parent.gameObject.SetActive(true);

                    Heart.SetActive(false);
                    BreakHeart.SetActive(true);
                    GetComponentInParent<Animator>().GetComponent<Animator>().SetBool("isAnger", true);

                }
                else
                {
                    if (tavlanmaSayisi <= 100 - tavlanmaHizi)
                    {
                        
                        tavlanmaSayisi += tavlanmaHizi;
                        SetSizeBar(tavlanmaSayisi / 100);
                        if (PowerUp.isGift && tavlanmaSayisi >= 100)
                        {
                            tavlanmaSayisi = 150;
                            pc.GiftClose();
                        }
                            
                    }
                    else
                    {

                    }
                }
            }


            if (tavlanmaSayisi >= 100)
            {
                tavlanmaSayisi = 0;
                gfPatrolScript.bar.parent.gameObject.SetActive(false);
                gfPatrolScript.speed = 1;
                Heart.SetActive(true);
                BreakHeart.SetActive(false);
                pc.BuketClose();
                GetComponentInParent<Animator>().GetComponent<Animator>().SetBool("isAnger", false);

            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Flip(other.transform, Math.Sign(transform.position.x - other.transform.position.x));

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
            StartCoroutine(BarAzalt() );
        }
    }

    IEnumerator BarAzalt()
    {
        while (tavlanmaSayisi >= 0) // Bu coroutine, durdurulmadığı sürece sürekli çalışmaya devam eder
        {
            tavlanmaSayisi -= 1;
            SetSizeBar(tavlanmaSayisi / 100);
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void SetSizeBar(float sizeNormalized)
    {
        gfPatrolScript.bar.localScale = new Vector3(sizeNormalized, 1f);
    }
    
    void Flip(Transform transform, float direction){
        Vector3 theScale = transform.localScale;
        theScale.x = -direction / 2;
        transform.localScale = theScale;
    }
}
