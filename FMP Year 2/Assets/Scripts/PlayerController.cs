using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;
using Cinemachine;


public class PlayerController : MonoBehaviour
{
    public GameController gameController;

    private bool crateBroken = false;
    private bool canPaus = true;
    public bool menuActive = false;
    public bool startChase = false;
    private double gameStart = 0;
    private bool startGame = false;

    
    private bool Jumpable = false, jumpDelay = false;
    private bool moving = false;
    public float moveSpeed;
    private float movehorizontal;
    public float jumpForceUp, jumpForceRight;
    private bool isjumping;
    private float jumpCooldown = 0;
    private bool isRunning = false;
    private bool grounded = false;

    public GameObject camera;
    private Component cinemachine;
    public GameObject cage;
    public GameObject pauseMenu;
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    [SerializeField] private LayerMask jumpableGround;
    public SpriteRenderer playerSprite;
    public Animator cageAnimator;
    public Animator animator; 

    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //moveSpeed = 7f;
        coll = GetComponent<BoxCollider2D>();
        SetTransformCoord(cage.transform.position.x - 3.3f, -0f);
    }

    void FixedUpdate()
    {
        if (menuActive == false)
        {
            if (moving == true)
            {
                movehorizontal = Input.GetAxisRaw("Horizontal");

                rb.velocity = new Vector2(movehorizontal * moveSpeed, rb.velocity.y);

                if (movehorizontal > 0.1)
                {
                    isRunning = true;
                    GetComponent<SpriteRenderer>().flipX = false;
                }
                else if (movehorizontal < -0.1)
                {
                    isRunning = true;
                    GetComponent<SpriteRenderer>().flipX = true;
                }
                else
                {
                    isRunning = false;
                }
            }
            if (isjumping == true)
            {
                rb.velocity = new Vector2(5f, rb.velocity.y);

            }
        }
    }
    private void Update()
    {
        Debug.Log(grounded + " Grounded");
        /*
                if (isjumping == true && jumpCooldown - 2 <= Time.time)
                {
                    isjumping = false;

                    animator.SetBool("Jumping", false);
                }
        */
        /*
        if (jumpDelay == true && jumpCooldown <= Time.time)
        {
            rb.velocity = new Vector2(jumpForceRight, jumpForceUp);
            isjumping = true;
            jumpDelay = false;
        }

        */
        if (menuActive == false)
        {
            animator.speed = 1f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (startGame == true && grounded == true && Jumpable == true)
                {
                    rb.velocity = new Vector2(20f, jumpForceUp);
                    Debug.Log("Jump");
                    animator.SetBool("Jumping", true);
                    jumpCooldown = Time.time + 1f;
                    jumpDelay = true;
                    isjumping = true;
                }
                if (startGame == false)
                {
                    gameController.wakeEnemy = true;
                    startGame = true;
                    cageAnimator.SetBool("Fall", true);
                    Console.WriteLine("Spacebar down");
                    gameStart = Time.time + 1;
                }
            }
            if (startGame == true && Time.time > gameStart && playerSprite.enabled == false)
            {
                playerSprite.enabled = true;
            }
            else if (startGame == true && Time.time > gameStart + 4 && moving == false)
            {
                CinemachineShake.Instance.shakeCamera(4f, 0.5f);
                CinemachineShake.Instance.shakeCamera(1f, 10f);
                Jumpable = true;
                moving = true;
            }

            
            if (startChase == true)
            {
                
            }
        }

        if (menuActive == true)
        {
            animator.speed = 0f;
        }

        if (Input.GetKeyDown(KeyCode.P) && canPaus == true)
        {
            if (menuActive == false)
            {
                menuActive = true;
                pauseMenu.SetActive(true);
            }
        }
       

        animator.SetBool("Running", isRunning);

    }
    public void SetTransformCoord(float x, float y)
    {
        transform.position = new Vector2(x, y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "StartChase" && startChase == false)
        {
            startChase = true;
        }
        grounded = true;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            if (isjumping == true && jumpCooldown < Time.time)
            {
                animator.SetBool("Jumping", false);
                isjumping = false;
            }
            
            
            grounded = true;
            

        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        grounded = false;

    }

}
