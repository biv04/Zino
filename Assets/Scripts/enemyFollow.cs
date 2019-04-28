using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyFollow : MonoBehaviour {
    public float speed;
    private Transform target;
    public int dist;
    public int health;

    private float dazedTime;
    public float startDazedTime;

    private GameObject bloodEffect;
    private Animator anim;

    public AudioClip fly1;
    public AudioClip fly2;
    public AudioClip death;
    public AudioClip hurt;

 

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim.SetBool("isRunning", true);
        SoundManager.instance.RandomizeSfx(fly1, fly2);
    }
	
	// Update is called once per frame
	void Update () {
		if(Vector2.Distance(transform.position, target.position)< dist)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

        if (dazedTime <= 0)
        {
            speed = 1;
        }
        else
        {
            speed = 0;
            dazedTime -= Time.deltaTime;
        }

        if (health <= 0)
        {
            SoundManager.instance.PlaySingle(death);

            anim.SetBool("isDead", true);
            Destroy(gameObject, (float)0.3);
        }

    }


   
    public void TakeDamage(int damage)
    {
        dazedTime = startDazedTime;
       // Instantiate(bloodEffect, transform.position, Quaternion.identity);
        health -= damage;
        SoundManager.instance.PlaySingle(hurt);
        //Debug.Log("damage taken");
    }
}
