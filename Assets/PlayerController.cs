using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isTouch = false;
    
    private Rigidbody2D _rb;
    private Vector2 moveVelocity;
    public static bool isTalking = false;

    public Animator _animator;
    
    [SerializeField] private float speed=10f;
    void Start()
    {
        _animator = GetComponent<Animator>();
        
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

#if UNITY_EDITOR
        float horizontalInput = Input.GetAxis("Horizontal");
        
        if (horizontalInput == 0)
        {
            _animator.SetBool("isMoving", false);
        }
        else
        {
            Flip(Math.Sign(horizontalInput));
            
            _animator.SetBool("isMoving", true);
        }

        Vector2 moveInput = new Vector2(horizontalInput,0);

        moveVelocity = moveInput * speed * Time.deltaTime;
#endif
       
#if UNITY_ANDROID

        if (Input.touchCount > 0)
        {
             Touch touch = Input.GetTouch(0);
             
             if (touch.phase == TouchPhase.Began)
             {
                 isTouch = true;

             } else if (touch.phase == TouchPhase.Ended)
             {
                 isTouch = false;
             }
             
             if (isTouch)
             {
                 if (Screen.width / 2 < touch.position.x)
                 {
                     Vector2 moveMobileInput = new Vector2(1,0);
            
                     moveVelocity = moveMobileInput * speed * Time.deltaTime;
            
                     Flip(Math.Sign(1));
                        
                     _animator.SetBool("isMoving", true);
                 }
                 else
                 {
                     Flip(Math.Sign(-1));
                        
                     _animator.SetBool("isMoving", true);
            
                     Vector2 moveMobileInput = new Vector2(-1,0);
            
                     moveVelocity = moveMobileInput * speed * Time.deltaTime;
                 }
             }
             else
             {
                 Vector2 moveMobileInput = new Vector2(0,0);
            
                 moveVelocity = moveMobileInput * speed * Time.deltaTime;
                 
                 _animator.SetBool("isMoving", false);
             }
            
             }

        
                
               
#endif 
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position+moveVelocity);
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Girl"))
        {
            isTalking = true;
        }
    }

    void Flip(float direction){
        Vector3 theScale = transform.localScale;
        theScale.x = -direction / 2;
        transform.localScale = theScale;
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Girl"))
        {
            isTalking = false;   
        }
        
    }
}
