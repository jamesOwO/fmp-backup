using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.LightAnchor;

public class CameraFollow : MonoBehaviour
{
    public int scene;
    public PlayerController playerController;
    public GameController gameController;

    public GameObject player;

    public float targetX;
    public float targetY;

    private float differenceX;
    private float differenceY;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( scene == 1)
        {
            if (gameController.finalAttack)
            {


                differenceX = targetX - this.transform.position.x;
                differenceY = targetY - this.transform.position.y;

                rb.velocity = new Vector2(differenceX, rb.velocity.y);

            }
            else
            {
                this.transform.position = new Vector2(player.transform.position.x, 1.5f);

            }

        }
        else if(scene == 2)
        {
            this.transform.position = new Vector2(player.transform.position.x, 1.5f);
        }
    }
}
