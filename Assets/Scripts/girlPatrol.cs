using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class girlPatrol : MonoBehaviour
{
    private Vector2 targetPos;

    private float distance;
    
    public float speed = 1f;

    public Transform bar;

    private float tavlanmaSayisi = 0; // 100 Olunca tavlanıp numarasını verecek

    private float tavlanmaHizi = 5f; //Karaktere göre tavlanma kolaylığı değişecek
    
    void Start()
    {
        targetPos = new Vector2( Random.Range(-8, 9), transform.position.y); //Patroll edilecek noktayı seçiyoruz.

        distance = Math.Abs(transform.position.x - targetPos.x); //Başlangıç pozisyonumuz ile gideceğimiz pozisyon arasındaki farklı alıyoruz.
    }
 
 
    void FixedUpdate()
    {
        distance = Math.Abs(transform.position.x - targetPos.x); //Başlangıç pozisyonumuz ile gideceğimiz pozisyon arasındaki farklı alıyoruz.

        if (distance > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
        else
        {
            targetPos = new Vector2( Random.Range(-8, 9), transform.position.y); //Patroll edilecek noktayı seçiyoruz.
        }

    }
 
    void Flip(){
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            speed = 0;
            tavlanmaSayisi += tavlanmaHizi;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
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

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            speed = 1;
        }
    }

    public void SetSizeBar(float sizeNormalized)
    {
        bar.localScale = new Vector3(sizeNormalized, 1f);
    }
}
