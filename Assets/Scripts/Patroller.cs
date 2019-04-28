using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroller : EnemyController {

    public Transform pointA;
    public Transform pointB;
    public bool isRight = true;
    private Vector3 pointAPosition;
    private Vector3 pointBPosition;

    private Animator anim;
    private GameObject bloodEffect;

    public AudioClip hurt;
    public AudioClip death;

    private void Start() {
        anim = GetComponent<Animator>();
        anim.SetBool("isRunning", true);
        health = 20;
        speed = 0.01f;
        hitTime = 1f;
        freezer = true;
        GetComponent<Rigidbody2D>().drag = 1;

        pointAPosition = new Vector3(pointA.position.x, 0, 0);
        pointBPosition = new Vector3(pointB.position.x, 0, 0);
    }


    public override void handleMovement() {
        pointAPosition = new Vector3(pointAPosition.x, transform.position.y, transform.position.z);
        pointBPosition = new Vector3(pointBPosition.x, transform.position.y, transform.position.z);

        if (isRight) {
            transform.position = Vector3.MoveTowards(transform.position, pointBPosition, speed);
            if (transform.position.Equals(pointBPosition)) {
                //Debug.Log ("Position b");
                isRight = false;
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        else {
            transform.position = Vector3.MoveTowards(transform.position, pointAPosition, speed);
            if (transform.position.Equals(pointAPosition)) {
                //Debug.Log ("Position a");
                isRight = true;
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }
}

