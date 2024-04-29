using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameController : MonoBehaviour
{

    PlayerController playerController;

    private bool enemyStartRun = false;
    public bool wakeEnemy = false;

    public GameObject enemy;

    public Animator enemyAnimator;

    // Start is called before the first frame update
    void Start()
    {
        enemyAnimator.speed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (wakeEnemy)
        {
            Debug.Log("EnemyAwake");
            enemyAnimator.speed = 1.5f;
        }
        if (playerController.startChase)
        {
            if (enemyStartRun == true)
            {
                enemy.transform.position = new Vector2(30, 10);
            }
        }
    }

}
