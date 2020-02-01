using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private bool isTouch = false;

    public GameObject buket;
    public Text GirlsCountText;
    private Rigidbody2D _rb;
    public Vector2 moveVelocity;
    public static bool isTalking = false;

    public static int GirlsCount = 0;
    public Animator _animator;
    
    [SerializeField] private float speed=10f;
    void Start()
    {
        _animator = GetComponent<Animator>();
        
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        GirlsCountText.text = GirlsCount.ToString();

        if (UIController.isGameStart && !UIController.isGamePasue)
        {
            _animator.enabled = true;

            BuketOpen();

            #region Controller

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

            if (horizontalInput < 0 && transform.position.x < -22)
            {
                horizontalInput = 0;
            }
            else if (horizontalInput > 0 && transform.position.x > 22)
            {
                horizontalInput = 0;
            }

            Vector2 moveInput = new Vector2(horizontalInput, 0);

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
                     Vector2 moveMobileInput;
                     
                     if ( transform.position.x > 22)
                     {
                         moveMobileInput = new Vector2(0,0);  
                     }
                     else
                     {
                         moveMobileInput = new Vector2(1,0);
                     }

                     moveVelocity = moveMobileInput * speed * Time.deltaTime;
            
                     Flip(Math.Sign(1));
                        
                     _animator.SetBool("isMoving", true);
                 }
                 else if (Screen.width / 2 > touch.position.x)
                 {
                     Flip(Math.Sign(-1));
                        
                     _animator.SetBool("isMoving", true);
            
                     Vector2 moveMobileInput;
                     
                     if ( transform.position.x < -22)
                     {
                         moveMobileInput = new Vector2(0,0);  
                     }
                     else
                     {
                         moveMobileInput = new Vector2(-1,0);
                     }
            
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

            #endregion

        }
        else
        {
            _animator.enabled = false;
        }

    }

    private void FixedUpdate()
    {
        if (UIController.isGameStart && !UIController.isGamePasue)
        {
            
            _rb.MovePosition(_rb.position + moveVelocity);
        }

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
    
    private void BuketOpen()
    {
        if (PowerUp.isTaked)
        {
            CancelInvoke(nameof(BuketClose));
            buket.SetActive(true);
            Invoke(nameof(BuketClose),5);
        }
    }

    public void BuketClose()
    {
        buket.SetActive(false);
        PowerUp.isTaked = false;
    }

    private void GameOver()
    {
        
    }
}
