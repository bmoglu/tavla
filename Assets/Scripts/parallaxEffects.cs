using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallaxEffects : MonoBehaviour
{
    public GameObject player;

    public float speed = 1f; //En arkadaki 1f
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Math.Sign(player.GetComponent<PlayerController>().moveVelocity.x) < 0)
        {
            Move(-speed / 8);
        }
        else if (Math.Sign(player.GetComponent<PlayerController>().moveVelocity.x) > 0)
        {
            Move(speed / 8);
        }
    }

    private void Move(float _Speed)
    {
        Vector3 newPos = transform.localPosition;
        
        newPos.x -= Time.deltaTime * _Speed;
        
        transform.localPosition = newPos;
    }
}
