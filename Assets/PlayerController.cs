using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 moveVelocity;
    public static bool isTalking = false;
    
    [SerializeField] private float speed=10f;
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"),0);
       moveVelocity = moveInput * speed;
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position+moveVelocity*Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Girl"))
        {
            isTalking = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Girl"))
        {
            isTalking = false;   
        }
        
    }
}
