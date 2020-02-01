using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    public float interpVelocity;
    public float minDistance;
    public float followDistance;
    public GameObject target;
    public Vector3 offset;
    Vector3 targetPos;
    // Use this for initialization

    private Vector2 maxXpos ;
    
    void Start ()
    {

        
        maxXpos = new Vector2(-14f, 14f);
        
        targetPos = transform.position;
    }
     
    // Update is called once per frame
    void FixedUpdate () {
        if (target)
        {
            if (transform.position.x < maxXpos.x)
            {
                transform.position = new Vector3(-14f, transform.position.y,-10f);
            } else if (transform.position.x > maxXpos.y)
            {
                 transform.position = new Vector3(14f, transform.position.y, -10f);
            }
            
            Vector3 posNoZ = transform.position;
            posNoZ.z = target.transform.position.z;
  
            Vector3 targetDirection = (target.transform.position - posNoZ);
  
            interpVelocity = targetDirection.magnitude * 5f;
  
            targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime); 
  
            transform.position = Vector3.Lerp( transform.position, targetPos + offset, 0.25f);
  
        }
    }

   
}
