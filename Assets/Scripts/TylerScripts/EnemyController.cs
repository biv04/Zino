using System;
using System.Collections;
using System.Collections.Generic;
using Boo.Lang;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    public float health;
    public float speed;
    public bool beingAttacked;
    public bool freezer = false;
    private Transform currentPos;
    public GameObject player;
    public float currHitTime;
    public float hitTime;

    protected float damage = 10f;

    //This is not how you use this component, how would you access the player's elements if it's new??
    //PlayerInventory p = new PlayerInventory();

    // Start is called before the first frame update
    void Start() {
        health = 100;
        speed = 0.025f;
        hitTime = 1f;
        //player = GameObject.Find("player");
        currentPos = GetComponent<Transform>();
    }

    // Update is called once per frame
    public void Update() {

        if (health <= 0) {
            gameObject.SetActive(false);
            player.GetComponent<PlayerInventory>().balance.setCoins(player.GetComponent<PlayerInventory>().balance.getCoins() + 10);
        }

        if (player.GetComponent<PlayerMovement>().isProtected) {
            Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>(), true);
        }

        else {
            Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>(), false);
        }
        
        
        if (!freezer) {
            handleMovement();
            return;
        }
        
        if (!beingAttacked) {
            GetComponent<SpriteRenderer>().color = Color.white;

            handleMovement();
        }
        else {

            GetComponent<SpriteRenderer>().color = Color.red;
            
            if (Time.time >= currHitTime + hitTime) {
                beingAttacked = false;
                Debug.Log("This shouldnt show!");
                Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), player.GetComponent<BoxCollider2D>(), false);

            }
            
        }
    }

    public virtual void handleMovement() {
        
    } /*{

        Transform playerPos = player.GetComponent<Transform>();
        if (playerPos.position.x < currentPos.position.x) {
            currentPos.position = new Vector2(currentPos.position.x - speed, currentPos.position.y);
        }

        else {
            currentPos.position = new Vector2(currentPos.position.x + speed, currentPos.position.y);
        }

    }*/

    public void takeDamage(int damageReceived, Vector2 damageForce) {
        health -= damageReceived;
        GetComponent<Rigidbody2D>().AddForce(damageForce, ForceMode2D.Impulse);
        currHitTime = Time.time;
        beingAttacked = true;
    }
    
    void OnCollisionEnter2D(Collision2D collision) {
        if (beingAttacked && freezer) {
            return;
        }
        if (collision.gameObject.CompareTag("Player")) {

            if (player.GetComponent<PlayerMovement>().isHit){// || player.GetComponent<PlayerMovement>().isProtected) {
                return;
            }
            Rigidbody2D playerRB = collision.gameObject.GetComponent<Rigidbody2D>();
            
            Vector3 dir = collision.gameObject.transform.position - transform.position;
            
            dir = dir.normalized;
            //Debug.Log(dir.x);
            
            if (Math.Abs(dir.x) < 0.5) {
                if (dir.x < 0) {
                    dir.x = -0.6f;
                }
                else {
                    dir.x = 0.6f;
                }
            }
            
            playerRB.AddForce(new Vector2(200*dir.x, 250));
            
            

            player.GetComponent<PlayerHealth>().health -= (int)(damage);
            player.GetComponent<PlayerMovement>().isHit = true;
            player.GetComponent<PlayerMovement>().currHitTime = Time.time;


        }
    }

   
}
