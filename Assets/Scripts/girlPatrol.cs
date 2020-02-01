using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class girlPatrol : MonoBehaviour
{
    public GameObject girlFriendGonulAlinmaBari,tavlanmaBar,phone;

    private bool isGirlGoOut = false;
    private Vector2 targetPos;

    private float distance;
    
    public float speed = 1f;

    public Transform bar;

    private float tavlanmaSayisi = 0; // 100 Olunca tavlanıp numarasını verecek

    private float tavlanmaHizi = .5f; //Karaktere göre tavlanma kolaylığı değişecek

    private bool talkToPlayer = false;

    public GameObject player;
    
    void Start()
    {
        targetPos = new Vector2( Random.Range(-69, 70), transform.position.y); //Patroll edilecek noktayı seçiyoruz.

        distance = Math.Abs(transform.position.x - targetPos.x); //Başlangıç pozisyonumuz ile gideceğimiz pozisyon arasındaki farklı alıyoruz.
    }

    private void Update()
    {
        if (girlFriendGonulAlinmaBari.activeSelf) //EĞER KIZ ARKADAŞIN KALBİ KIRIKSA TAVLANAMAZ
        {
            talkToPlayer = false;
            speed = 1;
            tavlanmaSayisi = 0;
            SetSizeBar(0);
        }
        
        if (talkToPlayer)
        {
            TALK();
        }
        
        if (tavlanmaSayisi >= 100 && !isGirlGoOut)
        {
            tavlanmaBar.SetActive(false);
            phone.SetActive(true);
            PlayerController.isTalking = false;
            isGirlGoOut = true;
            gameObject.GetComponent<Collider2D>().enabled = false;
            PlayerController.GirlsCount++;
        }
    }

    void FixedUpdate()
    {
        distance = Math.Abs(transform.position.x - targetPos.x); //Başlangıç pozisyonumuz ile gideceğimiz pozisyon arasındaki farklı alıyoruz.

       
            
        if (distance > 0.1f)
        {
            if (transform.position.x < targetPos.x)
            {
                Flip(1);
            }
            else
            {
                Flip(-1);
            }

            if(!isGirlGoOut)
                transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            else
            {
                targetPos = new Vector2(-100, transform.position.y);
                transform.position = Vector2.MoveTowards(transform.position,targetPos , 2.5f*speed * Time.deltaTime);  
            }
                
        }    
        else
        {
            if (!isGirlGoOut)
            {
                targetPos = new Vector2(Random.Range(-69, 70),
                    transform.position.y); //Patroll edilecek noktayı seçiyoruz.
            }
            
        }
        

    }
 
    void Flip(float direction){
        Vector3 theScale = transform.localScale;
        theScale.x = -direction / 2;
        transform.localScale = theScale;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player") && !girlFriendGonulAlinmaBari.activeSelf)
        {
            talkToPlayer = true;
            speed = 0;
            player.GetComponent<Animator>().SetBool("isTalking", true);
        }
    }

    private void TALK()
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
    
   

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            talkToPlayer = false;
            speed = 1;
            player.GetComponent<Animator>().SetBool("isTalking", false);
        }
    }

    public void SetSizeBar(float sizeNormalized)
    {
        bar.localScale = new Vector3(sizeNormalized, 1f);
    }

    private void GirlGoOut()
    {
        
    }
}