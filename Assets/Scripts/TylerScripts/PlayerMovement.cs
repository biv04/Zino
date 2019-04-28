using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    public float attackRadius;
    public int damage = 10;
    public float distToGround;
    public LayerMask GroundLayer;
    public float hitTime;
    public float jumpForce;
    public float currHitTime;
    public bool facingRight;
    public bool jumping;
    public bool onGround;
    public bool secondJump;
   // private int gravityCounter;
    public bool isHit;
    private Weapon currWeapon;


    public bool usingAbility1;
    public bool usingAbility2;

    public Vector2 damageForce = new Vector2(15, 0);
    
    Animator anim;
    private Rigidbody2D rb;
    
    public Transform attackPos;
    public LayerMask whatIsEnemies;

    public PlayerInventory inventory;

    public PlayerAbility ability1;
    public PlayerAbility ability2;

    public bool isProtected;

    private Color tempColor;


    private void Awake() {
        Application.targetFrameRate = 60;
        
        inventory = GetComponent<PlayerInventory>();
        inventory.balance.setCoins(0);
        //sword = inventory.backpack[0];
        //bow = inventory.backpack[1];
        damageForce = new Vector2(2, 0);
        distToGround = 0.25f;
        facingRight = true;
        isHit = false;
        jumping = false;
        onGround = true;
        secondJump = false;
        hitTime = 1f;
        //gravityCounter = 0;
        //health = 100;
        speed = 0.04f;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //myGravity = rb.gravityScale;
        attackRadius = 0.4f;
        jumpForce = 500f;

        isProtected = false;
        gameObject.AddComponent<powerAttack>();
        gameObject.AddComponent<Protect>();
        gameObject.AddComponent<Dash>();

        gameObject.AddComponent<HealthPotion>();
        gameObject.AddComponent<SpeedPotion>();






    }


    void Start() {

        currWeapon = inventory.getCurrWeapon();

        inventory.items.Add(GetComponent<HealthPotion>());
        for (int i = 0; i < 4; i++) {
            inventory.items.Add(GetComponent<SpeedPotion>());
        }

        ability1 = GetComponent<Dash>();
        ability2 = GetComponent<Protect>();



    }

    // Update is called once per frame
    void Update() {
        
        if (IsGrounded()) {
            onGround = true;
            jumping = false;
            secondJump = false;
            //rb.gravityScale = 1;
            //gravityCounter = 0;
            // anim.SetBool("isFalling", false);
            anim.SetBool("IsJump", false);
            //print("Ground");
        }
        
        checkAbilities();
        checkItems();
        
        if (!isHit) {
            if (!isProtected && !usingAbility1 && !usingAbility2) {
                GetComponent<SpriteRenderer>().color = Color.white;
            }

            handleMovement();
            handleAttacking();
            checkForWeaponSwap();
        }
        else {
            
            GetComponent<SpriteRenderer>().color = Color.cyan;

            if (Time.time >= currHitTime + hitTime) {
                isHit = false;
            }
            
        }
    }



    void handleMovement() {

        /* moving left and right */

        if (Input.GetAxisRaw("Horizontal") > 0) {
            if (facingRight) {
                Transform currentpos = GetComponent<Transform>();

                currentpos.position = new Vector2(currentpos.position.x + speed, currentpos.position.y);

                if (onGround) {
                    anim.SetBool("IsRunning", true);
                }
            }

            else {
                Flip();
            }

        }

        else if (Input.GetAxisRaw("Horizontal") < 0) {
            if (!facingRight) {
                Transform currentpos = GetComponent<Transform>();

                currentpos.position = new Vector2(currentpos.position.x - speed, currentpos.position.y);

                if (onGround) {
                    anim.SetBool("IsRunning", true);
                }
            }

            else {
                Flip();
            }
        }

        else {
            anim.SetBool("IsRunning", false);
        }

        /* handling jumping */

        if (Input.GetButtonDown("Jump")) {

            Jump();

        }
    }

    void handleAttacking() {

        if (Input.GetButtonDown("Fire1")) {


            if (currWeapon == inventory.backpack[0]) {
                anim.SetTrigger("IsAttacking");

                Collider2D[] enemiesToDamage =
                    Physics2D.OverlapCircleAll(attackPos.position, attackRadius, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++) {
                   
                    Vector3 dir = enemiesToDamage[i].gameObject.transform.position - attackPos.position;

                    dir = dir.normalized;
                   

                    enemiesToDamage[i].GetComponent<EnemyController>()
                        .takeDamage(damage, new Vector2(damageForce.x * dir.x, damageForce.y));
                    Physics2D.IgnoreCollision(enemiesToDamage[i], GetComponent<BoxCollider2D>(), true);
                    print("Hit!");
                }
            }

            else {
                
                //TODO: Handle stuff for ranged weapon damage, etc.
                
                var playerPos = new Vector2(transform.position.x, transform.position.y);
                
                Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                Vector2 lookPos = Camera.main.ScreenToWorldPoint(mousePos);
                
                var dir = lookPos - new Vector2(transform.position.x, transform.position.y);
                var temp = dir.normalized * 2;
                temp += playerPos;

                
                int layermask = LayerMask.GetMask("Enemy");
 
                var thisRay = Physics2D.Raycast(playerPos, temp - playerPos, Mathf.Infinity, layermask);
                
                if (thisRay){ 

                    Debug.DrawRay(playerPos, (temp-playerPos).normalized * thisRay.distance, Color.red, 10f);
                    
                    thisRay.collider.gameObject.GetComponent<EnemyController>().takeDamage(damage, new Vector2(damageForce.x * dir.normalized.x, damageForce.y));  

                   
                }

                else {
                }


            }



        }
        
    }
    
 

    public void Flip()
    {
        
        facingRight = !facingRight;
        
        //print(anim.GetBool("FacingRight"));
        // Multiply the player's x local scale by -1
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void Jump() {

        if (onGround) {
            jumping = true;
            onGround = false;
            secondJump = false;
            rb.AddForce(new Vector2(0, jumpForce));
            anim.SetBool("IsJump", true);
            return;
        }
        if (!secondJump) {
            secondJump = true;
            rb.AddForce(new Vector2(0, jumpForce/1.5f));
        }
        


    }
    
    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distToGround, GroundLayer);
        if (hit.collider)
        {
            return true;
        }
        return false;

    }

    public void checkForWeaponSwap() {

        if (Input.GetAxis("Mouse ScrollWheel") < 0f && currWeapon != inventory.backpack[0]) {
            currWeapon = inventory.SwitchActiveWeapon(); //switch to sword
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0f && currWeapon != inventory.backpack[1]) {
            currWeapon = inventory.SwitchActiveWeapon(); //switch to bow
        }
        
        //work on mousewheel stuff
        
        
        
        
    }


    public void checkAbilities() {


        if (!ability1.isHoldAbility && usingAbility1 && !ability1.isUsing) {
            usingAbility1 = false;
        }
        
        if (!ability2.isHoldAbility && usingAbility2 && !ability2.isUsing) {
            usingAbility2 = false;
        }
        
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKey(KeyCode.Q) && !ability1.isCountingDown) {
            
            ability1.useAbility(KeyCode.Q);
            usingAbility1 = true;
        }

        if (ability1.isHoldAbility && usingAbility1 && Input.GetKeyUp(KeyCode.Q)) {
            ability1.finishAbility();
            usingAbility1 = false;
        }
        
        
        if (Input.GetKeyDown(KeyCode.E) && ability2.isCountingDown == false) {
            ability2.useAbility(KeyCode.E);
            usingAbility2 = true;
        }

    }


    public void checkItems() {

        var countOfItems = inventory.items.Count;
        
        if (!(countOfItems > 0)) {
            return;
        }

        if (countOfItems > 0) {
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                inventory.items[0].activate();
                inventory.items.RemoveAt(0);
            }
        }
        
        if (countOfItems > 1) {
            if (Input.GetKeyDown(KeyCode.Alpha2)) {
                inventory.items[1].activate();
                inventory.items.RemoveAt(1);
            }
        }
        
        if (countOfItems > 2) {
            if (Input.GetKeyDown(KeyCode.Alpha3)) {
                inventory.items[2].activate();
                inventory.items.RemoveAt(2);
            }
        }
        
        
        
    }

    //Here are Unity helper functions


    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
            jumping = false;
            secondJump = false;
            //rb.gravityScale = 1;
            //gravityCounter = 0;
           // anim.SetBool("isFalling", false);
            anim.SetBool("IsJump", false);
            print("Ground");
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = false;
            print("Off ground");
        }
    }
 
    

    
    
    
}

