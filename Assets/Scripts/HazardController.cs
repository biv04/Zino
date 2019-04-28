using System;
using System.Collections;
using System.Collections.Generic;
using Boo.Lang;
using UnityEngine;

public class HazardController : MonoBehaviour
{


    private Transform currentPos;
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        currentPos = GetComponent<Transform>();
    }

    // Update is called once per frame
    public void Update()
    {

    }

   

  
    void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.CompareTag("Player"))
        {

            if (player.GetComponent<PlayerMovement>().isHit)
            {
                return;
            }
            Rigidbody2D playerRB = collision.gameObject.GetComponent<Rigidbody2D>();

            Vector3 dir = collision.gameObject.transform.position - transform.position;

            dir = dir.normalized;
            //Debug.Log(dir.x);

            if (Math.Abs(dir.x) < 0.5)
            {
                if (dir.x < 0)
                {
                    dir.x = -0.6f;
                }
                else
                {
                    dir.x = 0.6f;
                }
            }

            playerRB.AddForce(new Vector2(200 * dir.x, 250));



            player.GetComponent<PlayerHealth>().health -= 10;
            player.GetComponent<PlayerMovement>().isHit = true;
            player.GetComponent<PlayerMovement>().currHitTime = Time.time;


        }
    }


}
