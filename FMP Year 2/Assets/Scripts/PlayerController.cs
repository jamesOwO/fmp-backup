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
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameController gameController;

    private bool crateBroken = false;
    private bool canPaus = false;
    public bool menuActive = false;
    public bool startChase = false;
    private double gameStart = 0;
    private bool startGame = false;

    private float playerAcceleration = 0, changedDirection, movehorizontal, jumpCooldown = 0, sceneTransitionCooldown, jumpDirection;
    private bool Jumpable = false, jumpDelay = false, moving = false, isjumping, grounded = false, isRunning = false, playerDead = false, sceneTransitionStart;

    public float moveSpeed, jumpForceUp, jumpForceRight;
    

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
    public Animator sceneTransition;

    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //moveSpeed = 7f;
        coll = GetComponent<BoxCollider2D>();
        SetTransformCoord(cage.transform.position.x - 3.3f, -0f);
    }

    void FixedUpdate()
    {
        Debug.Log("Movement - " + playerAcceleration);
        if (menuActive == false)
        {
            if (moving == true)
            {
                movehorizontal = Input.GetAxisRaw("Horizontal");


                if (playerAcceleration <= moveSpeed)
                {
                    playerAcceleration = playerAcceleration + 0.125f;
                }


                rb.velocity = new Vector2(movehorizontal * playerAcceleration, rb.velocity.y);

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
                if (movehorizontal != changedDirection)
                {
                    playerAcceleration = 0;
                }
                changedDirection = movehorizontal;

            }
            if (isjumping == true)
            {
                if(jumpDirection == 0)
                {
                    rb.velocity = new Vector2(playerAcceleration * movehorizontal, rb.velocity.y);

                }
                else
                {
                    rb.velocity = new Vector2(5f * jumpDirection, rb.velocity.y);
                    playerAcceleration = 0;
                }
            }

        }
    }
    private void Update()
    {
        Debug.Log(grounded + " Grounded");

        if (menuActive == false)
        {
            animator.speed = 1f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (startGame == true && grounded == true && Jumpable == true)
                {
                    playerAcceleration = 0;
                    jumpDirection = movehorizontal;
                    rb.velocity = new Vector2(rb.velocity.x, jumpForceUp);
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
                    gameStart = Time.time + 2.5;
                }
            }
            if (startGame == true && Time.time > gameStart && playerSprite.enabled == false)
            {
                playerSprite.enabled = true;
            }
            else if (startGame == true && Time.time > gameStart + 3.5 && moving == false)
            {
                CinemachineShake.Instance.shakeCamera(4f, 0.5f);
                CinemachineShake.Instance.shakeCamera(1f, 10f);
                Jumpable = true;
                moving = true;
            }

            
            if (startChase == true)
            {
                canPaus = true;   
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
                animator.speed = 0f;
            }
        }
        if (Input.GetKeyDown(KeyCode.R) && playerDead == false)
        {
            Debug.Log("restart");
            playerDead = true;
        }
       

        if (playerDead == true)
        {
            Debug.Log("restarting");
            moving= false;
            animator.speed = 0f;
            cageAnimator.speed = 0f;
            gameController.enemyAnimator.speed = 0f;
            gameController.enemyArm.speed = 0f;


            if (sceneTransitionStart == false)
            {
                sceneTransition.SetBool("Fade", true);
                sceneTransitionCooldown = Time.time + 2;
                sceneTransitionStart = true;
            }
            else if (sceneTransitionCooldown <= Time.time)
            {
                SceneManager.LoadScene(1);
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
        if (collision.tag == "EnemyCane")
        {
            playerDead = true;
        }
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
