using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public PlayerController playerController;

    public bool treeFall = false;
    
    private bool startAttack = false;
    private float distanceFromMan = 0;
    public float enemySpeed;
    private bool enemyRunning = false;
    private bool enemyStartRun = true, sceneTransitionStart;
    public bool wakeEnemy = false;
    public bool finalAttack = false;
    private float attackCooldown = 0, sceneTransitionCooldown;
    private bool loadNextScene;
     
    public GameObject player;
    public GameObject enemyChase;
    public GameObject enemyCutscene;
    private Rigidbody2D enemyrb;
    public Animator enemyAnimator, enemyArm, treeTrunk, sceneTransition;


    // Start is called before the first frame update
    void Start()
    {
        enemyrb = enemyChase.GetComponent<Rigidbody2D>();
        enemyAnimator = enemyCutscene.GetComponent<Animator>();
        enemyAnimator.speed = 0f;
        treeTrunk.speed = 0f;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (enemyRunning == true)
        {
            enemyrb.velocity = new Vector2(1 * enemySpeed, enemyrb.velocity.y);
        }
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.L))
        {
            enemyChase.transform.position = new Vector2(20, enemyChase.transform.position.y);
        }

        if (wakeEnemy)
        {
            enemyAnimator.speed = 1.5f;
            wakeEnemy = false;
        }
        if (playerController.startChase)
        {
            distanceFromMan = player.transform.position.x - enemyChase.transform.position.x;

            if (enemyStartRun == true)
            {
                enemyChase.SetActive(true);
                enemyChase.transform.position = new Vector2(-15, enemyChase.transform.position.y);
                //enemyAnimator.SetBool("StartRun", true);
                enemyRunning = true;
                enemyStartRun = false;
            }

            if (enemyChase.transform.position.x <= 265)
            {
                if (distanceFromMan < 21 && startAttack == false)
                {
                    enemySpeed = 2f;
                    startAttack = true;
                }
                if (distanceFromMan >= 25)
                {
                    enemySpeed = 4.5f;
                }
                if (distanceFromMan >= 30)
                {
                    enemySpeed = 6f;
                }
            }
            else if (enemyChase.transform.position.x <= 274)
            {
                if (distanceFromMan < 22)
                {
                    enemySpeed = 3;
                }
            }
            else if (enemyChase.transform.position.x >= 275 && finalAttack == false)
            {
                enemySpeed = 1f;
                finalAttack = true;
                attackCooldown = Time.time + 2.5f;
                enemyArm.SetBool("FinalAttack", true);
                enemyAnimator.SetBool("FinalAttack", true);
            }

            if (treeFall == true)
            {
                treeTrunk.speed = 0.65f;
                loadNextScene = true;
            }
            /*
            if (finalAttack && attackCooldown + 0.7  <= Time.time)
            {
                treeTrunk.speed = 0.75f;
                loadNextScene = true;

            }
            if (finalAttack && attackCooldown + 0.7  <= Time.time)
            {
                loadNextScene = true;
            }
            */
            Debug.Log(treeFall);
            if (startAttack == true)
            {
                enemyArm.Play("Attack");
                startAttack = false;
            }
            if (loadNextScene)
            {
                if (finalAttack && sceneTransitionStart == false)
                {
                    sceneTransition.SetBool("Fade", true);
                    sceneTransitionCooldown = Time.time + 2;
                    sceneTransitionStart = true;
                }
                if (finalAttack && sceneTransitionCooldown <= Time.time)
                {
                    SceneManager.LoadScene(2);
                }

            }
        }
    }
}