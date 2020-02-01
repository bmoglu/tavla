using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using Random = UnityEngine.Random;

public class girlPatrol : MonoBehaviour
{
    public GameObject girlFriendGonulAlinmaBari, tavlanmaBar;
    public GameObject[] contact;

    private bool isGirlGoOut = false;
    private Vector2 targetPos;

    private float distance;
    
    public float speed = 1f;

    public Transform bar;

    private float tavlanmaSayisi = 0; // 100 Olunca tavlanıp numarasını verecek

    private float tavlanmaHizi = .5f; //Karaktere göre tavlanma kolaylığı değişecek

    private bool talkToPlayer = false;

    public GameObject player;

    private PlayerController pc;
    
    private void Awake()
    {
       pc=GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    void Start()
    {
        targetPos = new Vector2( Random.Range(-22, 23), transform.position.y); //Patroll edilecek noktayı seçiyoruz.

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
            var rnd=Random.Range(0,3); 
            tavlanmaBar.SetActive(false);
            contact[rnd].SetActive(true);
            PlayerController.isTalking = false;
            isGirlGoOut = true;
            gameObject.GetComponent<Collider2D>().enabled = false;
            PlayerController.GirlsCount++;
            
            pc.BuketClose();

            
            
        }
    }

    void FixedUpdate()
    {
        distance = Math.Abs(transform.position.x - targetPos.x); //Başlangıç pozisyonumuz ile gideceğimiz pozisyon arasındaki farklı alıyoruz.

       
            
        if (distance > 0.1f)
        {
            if (transform.position.x < targetPos.x)
            {
                Flip(transform,1);
            }
            else
            {
                Flip(transform,-1);
            }

            if(!isGirlGoOut)
                transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            else
            {
                if (0 < transform.position.x)
                {
                    targetPos = new Vector2(100, transform.position.y);
                }
                else
                {
                    targetPos = new Vector2(-100, transform.position.y);
                }
                transform.position = Vector2.MoveTowards(transform.position,targetPos , 2.5f*speed * Time.deltaTime);
            }
                
        }    
        else
        {
            if (!isGirlGoOut)
            {
                targetPos = new Vector2(Random.Range(-22, 23),
                    transform.position.y); //Patroll edilecek noktayı seçiyoruz.
            }
            
        }
        

    }
 
    void Flip(Transform transform, float direction){
        Vector3 theScale = transform.localScale;
        theScale.x = -direction / 2;
        transform.localScale = theScale;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player") && !girlFriendGonulAlinmaBari.activeSelf)
        {
            Flip(other.transform,Math.Sign(transform.position.x - other.transform.position.x));
            Flip(transform, -Math.Sign(transform.position.x - other.transform.position.x));
            
            talkToPlayer = true;
            speed = 0;
            player.GetComponent<Animator>().SetBool("isTalking", true);
        }
    }

    private void TALK()
    {
        if (tavlanmaSayisi <= 100)
        {
            if (PowerUp.isTaked) // power up alındı
            {
                tavlanmaSayisi += tavlanmaHizi*5;
                
            }
            else
            {
                tavlanmaSayisi += tavlanmaHizi;
            }
            
            SetSizeBar(tavlanmaSayisi / 100);
        }
        else
        {
            
        }
    }
    
   

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            talkToPlayer = false;
            speed = 1;
            player.GetComponent<Animator>().SetBool("isTalking", false);
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
        bar.localScale = new Vector3(sizeNormalized, 1f);
    }
}