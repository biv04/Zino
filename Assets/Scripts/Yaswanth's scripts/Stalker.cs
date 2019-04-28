using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalker : EnemyController
{
    
    public bool isRight = true;
    private Vector3 pointAPosition;
    private Vector3 pointBPosition;

    private Animator anim;
    private GameObject bloodEffect;
    private Transform target;
    public Transform target2;
    public int triggerDistance;
    public bool following;

    public AudioClip hurt;
    public AudioClip death;

    private void Start()
    {
        anim = GetComponent<Animator>();

        following = false;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<Rigidbody2D>().drag = 5;
        
        triggerDistance = 2;
        target = player.GetComponent<Transform>();
        anim.SetBool("isRunning", true);
        health = 20;
        speed = 0.75f;
        hitTime = 1f;
        freezer = true;

    }


    public override void handleMovement()
    {

        if(Math.Abs(target.position.x - target2.position.x) < triggerDistance && Math.Abs(target.position.y - target2.position.y) < triggerDistance) {
            following = true;
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, target2.position, speed * Time.deltaTime);
        }

    }
}


