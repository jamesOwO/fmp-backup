using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public PlayerController playerController;

    private bool startAttack = false;
    private float distanceFromMan = 0;
    public float enemySpeed;
    private bool enemyRunning = false;
    private bool enemyStartRun = true;
    public bool wakeEnemy = false;

    public GameObject player;
    public GameObject enemyChase;
    public GameObject enemyCutscene;
    private Rigidbody2D enemyrb;
    public Animator enemyAnimator, enemyArm;


    // Start is called before the first frame update
    void Start()
    {
        enemyrb = enemyChase.GetComponent<Rigidbody2D>();
        enemyAnimator = enemyCutscene.GetComponent<Animator>();
        enemyAnimator.speed = 0f;
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

            Debug.Log(player.transform.position.x - enemyChase.transform.position.x);
            if (distanceFromMan < 20 && startAttack == false)
            {
                enemySpeed = 2.5f;
                startAttack = true;
            }
            if (distanceFromMan >= 40)
            {
                enemySpeed = 7f;
                startAttack = false;
            }
            if (startAttack == true)
            { 
                enemyArm.SetTrigger("attack");

            }
        }
    }
}