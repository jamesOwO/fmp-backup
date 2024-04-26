using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool beginChase = false;
    public bool wakeEnemy = false;

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
    }

}
