using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GirlFriendPatrol : MonoBehaviour
{
    private Vector2 targetPos;

    private float distance;
    
    public float speed = 1f;

    public Transform bar;

    private bool talkToPlayer = false;

    private Animator _animator;

    public GameObject player; 
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        
        targetPos = new Vector2( Random.Range(player.transform.position.x + 3, player.transform.position.x - 3), transform.position.y); //Patroll edilecek noktayı seçiyoruz.

        distance = Math.Abs(transform.position.x - targetPos.x); //Başlangıç pozisyonumuz ile gideceğimiz pozisyon arasındaki farklı alıyoruz.
    }

    private void Update()
    {

        if (UIController.isGameStart && !UIController.isGamePasue)
        {
            _animator.enabled = true;
            if (_animator.GetBool("isAnger"))
            {
                transform.position = new Vector3(transform.position.x, -2f, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, -3.18f, transform.position.z); 
            }
            
            if (speed == 1)
            {
                _animator.SetBool("isMoving", true);
            }
            else
            {
                _animator.SetBool("isMoving", false);
            }
        }
        else
        {
            _animator.enabled = false;
        }
    }

    void FixedUpdate()
    {
        if (UIController.isGameStart && !UIController.isGamePasue)
        {
            distance = Math.Abs(transform.position.x - targetPos.x); //Başlangıç pozisyonumuz ile gideceğimiz pozisyon arasındaki farklı alıyoruz.

        
            if (distance > 0.1f)
            {
                if (transform.position.x < targetPos.x)
                {
                    Flip(transform, 1);
                }
                else
                {
                    Flip(transform, -1);
                }

                transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            }
            else
            {
                targetPos = new Vector2(Random.Range(player.transform.position.x + 3, player.transform.position.x - 3),
                    transform.position.y); //Patroll edilecek noktayı seçiyoruz.
            }
        }

    }
 
    void Flip(Transform transform, float direction){
        Vector3 theScale = transform.localScale;
        theScale.x = -direction / 2;
        transform.localScale = theScale;
    }


    public void SetSizeBar(float sizeNormalized)
    {
        bar.localScale = new Vector3(sizeNormalized, 1f);
    }
}